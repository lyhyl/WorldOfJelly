#define _DIS_RANGE_

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnionPreColl;

namespace UnionPreColl
{
    public static class JellyCollision
    {
        public static void RestrictPolygon(JellyVertex[] poly, ref List<Vector> inters, ref List<int> discons)
        {
            for (int c = 0; c < inters.Count; c += 2)
            {
                Vector segb = inters[c];
                Vector sege = inters[c + 1];

                int indexa = discons[c];
                int indexb = discons[c + 1];

                if (indexa < indexb)
                    for (int vi = indexa; vi < indexb; ++vi)
                        poly[vi].Position = JMath.ClosestPointOnSegment(segb, sege, poly[vi].Position);
                else
                    for (int vi = indexb; vi < indexa + poly.Length; ++vi)
                        poly[vi % poly.Length].Position = JMath.ClosestPointOnSegment(segb, sege, poly[vi % poly.Length].Position);
            }
        }

        public static void ProcessCollisionB(JellyVertex[] polya, JellyVertex[] polyb, ref List<bool> io_list, ref List<int> discon_list, ref List<Vector> inter_list)
        {
            io_list.Clear();
            //opp_io_list.Clear();

            GetInOutState(polya, polyb, ref io_list);
            //GetInOutState(polyb, polya, ref opp_io_list);

            discon_list.Clear();
            //opp_discon_list.Clear();

            FindDiscontinuity(ref io_list, ref discon_list);
            //FindDiscontinuity(ref opp_io_list, ref opp_discon_list);

            //!!
            if ((discon_list.Count & 1) != 0)// || (opp_discon_list.Count & 1) != 0)
                throw new Exception("assert failed.");

            inter_list.Clear();
            //opp_inter_list.Clear();

            GetIntersetions(polya, polyb, ref discon_list, ref inter_list);
            //GetIntersetions(polyb, polya, ref opp_discon_list, ref opp_inter_list);

            //!!
            if ((inter_list.Count & 1) != 0)// || (opp_inter_list.Count & 1) != 0)
                throw new Exception("assert failed.");
        }

        private static void GetIntersetions(JellyVertex[] pa, JellyVertex[] pb, ref List<int> discons, ref List<Vector> inters)
        {
            Vector inter;
            foreach (int id in discons)
                if (JMath.SegmentPolygonTest(
                    pa[(id + pa.Length - 1) % pa.Length].Position,
                    pa[id].Position,
                    pb,
                    out inter))
                    inters.Add(inter);
                else
                {
                    inters.Add((pa[(id + pa.Length - 1) % pa.Length].Position + pa[id].Position) * 0.5);
                    //throw new Exception("assert failed.");
                }
        }

        private static void FindDiscontinuity(ref List<bool> io_states, ref List<int> discons)
        {
            int c = 0;
            if (io_states[0])
            {
                if (io_states[io_states.Count - 1])//cycle
                {
                    //notice here ,
                    //if polygonA is fully inside the polygonB ,
                    //it will get ArgumentOutOfRangeException ,
                    //because all the vertices' io state are true

                    //while (io_states[c]) ++c;

                    foreach (bool s in io_states)
                        if (s) ++c; else break;

                    int tmp = c;

                    while (c < io_states.Count - 1)
                        if (io_states[c] != io_states[++c])
                            discons.Add(c);

                    discons.Add(tmp);
                }
                else
                {
                    discons.Add(0);

                    while (c < io_states.Count - 1)
                        if (io_states[c] != io_states[++c])
                            discons.Add(c);
                }
            }
            else
            {
                while (c < io_states.Count - 1)
                    if (io_states[c] != io_states[++c])
                        discons.Add(c);

                if (io_states[c])
                    discons.Add(0);
            }
        }

        private static void GetInOutState(JellyVertex[] pa, JellyVertex[] pb, ref List<bool> io_states)
        {
            foreach (JellyVertex v in pa)
                io_states.Add(JMath.PointInPolygon(pb, v.Position, new Vector(0, -500)));
        }

        public static void ProcessCollision(JellyVertex[] polya, JellyVertex[] polyb, ref List<Pair<int>> idpairs, ref List<Pair<Vector>> ptpairs)
        {
            idpairs.Clear();
            ptpairs.Clear();

            bool startin = JMath.PointInPolygon(polyb, polya[0].Position, new Vector(0, -500));
            int beginiter = 0;

            bool usefirst = true;
            Vector insa = null, insb = null;
            int id = int.MaxValue;

            Vector insbuf = null;
            int idbuf = int.MaxValue;

            if (startin)
            {
                for (; beginiter < polya.Length - 1; ++beginiter)
                {
                    if (JMath.SegmentPolygonTest(polya[beginiter].Position, polya[beginiter + 1].Position, polyb, out insbuf))
                    {
                        idbuf = beginiter;
                        break;
                    }
                }
                /*if (insbuf == null)
                {
                    startin = false;
                    insbuf = polya[0].Position;
                    idbuf = 0;
                    //return;
                }*/
                ++beginiter;
            }

            int counter;
            for (counter = beginiter; counter < polya.Length - 1; ++counter)
                FindInPairs(polya, polyb, ref usefirst, counter, counter + 1, ref id, ref insa, ref insb, ref idpairs, ref ptpairs);
            /*if (counter != polya.Length)*/
            if (counter == polya.Length - 1) FindInPairs(polya, polyb, ref usefirst, counter, 0, ref id, ref insa, ref insb, ref idpairs, ref ptpairs);
            /*while (beginiter < polya.Length)
                FindInPairs(polya, polyb, ref usefirst, beginiter, ++beginiter % polya.Length, ref id, ref insa, ref insb, ref idpairs, ref ptpairs);*/
            
            if (startin)
            {
                insb = insbuf;
                // !!! TEMPORARY !!! Here Are Some Problems
                // !!! TEMPORARY !!! Sometimes insa Or insb will be null
                // !!! TEMPORARY !!! double point number...
                if (insa == null || insb == null)
                    return;
                // !!! TEMPORARY !!! Unsolved
                idpairs.Add(Pair.CreatePair((id + 1) % polya.Length, idbuf));
                ptpairs.Add(Pair.CreatePair(insa, insb));
            }
        }

        private static void FindInPairs(JellyVertex[] polya, JellyVertex[] polyb, ref bool usefirst, int indexa, int indexb, ref int id, ref Vector insa, ref Vector insb, ref List<Pair<int>> idpairs, ref List<Pair<Vector>> ptpairs)
        {
            if (usefirst)
            {
                if (JMath.SegmentPolygonTest(polya[indexa].Position, polya[indexb].Position, polyb, out insa))
                {
                    usefirst = false;
                    id = indexa;
                }
            }
            else
            {
                if (JMath.SegmentPolygonTest(polya[indexa].Position, polya[indexb].Position, polyb, out insb))
                {
                    usefirst = true;
                    idpairs.Add(Pair.CreatePair(id + 1, indexa));
                    ptpairs.Add(Pair.CreatePair(insa, insb));
                }
            }
        }

        public static void Restrict(JellyVertex[] polygon, ref List<Pair<int>> InsIDPairs, ref List<Pair<Vector>> InsPtPairs, ref List<Vector> restrictpt)
        {
            restrictpt.Clear();

            //compute
            for (int c = 0; c < InsIDPairs.Count; ++c)
            {
                Vector segs = InsPtPairs[c].A;
                Vector sege = InsPtPairs[c].B;
                int itermax = InsIDPairs[c].B < InsIDPairs[c].A ? InsIDPairs[c].B + polygon.Length : InsIDPairs[c].B;
                for (int iter = InsIDPairs[c].A; iter <= itermax; ++iter)
                {
                    Vector pt = polygon[iter % polygon.Length].Position;
                    restrictpt.Add(JMath.ClosestPointOnSegment(segs, sege, pt));
                }
            }
        }

        public static void ApplyRestrict(JellyVertex[] polygon, ref List<Pair<int>> InsIDPairs, ref List<Vector> restrictpt)
        {
            //apply
            for (int c = 0; c < InsIDPairs.Count; ++c)
            {
                int ass = 0;
                for (int i = InsIDPairs[c].A; i <= InsIDPairs[c].B; ++i, ++ass)
                {
                    polygon[i].Position = restrictpt[ass];
                    polygon[i].Velocity *= 0.5;
                }
            }
        }

        private static Random rand = new Random();
        public static void ProcessEdgeForce(JellyVertex[] polygon, JellyVertex jv)
        {
            if (JMath.PointInPolygon(polygon, jv.Position, new Vector(-10000, -10000)))
            {
                jv.Velocity.X = -jv.Velocity.X * JellyWorld.Friciton;
                jv.Velocity.Y = -jv.Velocity.Y * JellyWorld.Friciton;
                jv.Force.X = jv.Force.Y = 0;
                //return;//for union other restrist method
                jv.Position = ClosestPoint(jv.Position, polygon);// +new Vector(rand.NextDouble() - 0.5, rand.NextDouble() - 0.5);//here inher
            }
            else
            {

#if _DIS_RANGE_
                const double threshold = 44.7213595499958;
#else 
                const double threshold = 3162.27766016838;
#endif
                const double forcek = 100000;
                int i = 0;
                Vector cpt;
                bool inVertex, inThreshold, inBack;
                while (i < polygon.Length - 1)
                {
                    FindClosestPointThreshold(jv.Position, polygon[i].Position, polygon[i + 1].Position, threshold * threshold,
                        out cpt, out inVertex, out inThreshold, out inBack);
                    ++i;
                    if (!inBack && inThreshold)
                    {
                        Vector diff = jv.Position - cpt;
#if _DIS_RANGE_
                        double norsize = forcek / diff.LengthSq - 50;
#else
                        double norsize = forcek / diff.LengthSq;
#endif
                        norsize = norsize < 0 ? 0 : norsize;
                        if (inVertex)
                            norsize *= 0.5;
                        diff.Normalize(norsize);
                        jv.Force += diff;
                    }
                }
                FindClosestPointThreshold(jv.Position, polygon[i].Position, polygon[0].Position, threshold * threshold,
                    out cpt, out inVertex, out inThreshold, out inBack);
                if (!inBack && inThreshold)
                {
                    Vector diff = jv.Position - cpt;
#if _DIS_RANGE_
                    double norsize = forcek / diff.LengthSq - 50;
#else
                        double norsize = forcek / diff.LengthSq;
#endif
                    norsize = norsize < 0 ? 0 : norsize;
                    if (inVertex)
                        norsize *= 0.5;
                    diff.Normalize(norsize);
                    jv.Force += diff;
                }
            }
        }

        private static bool InBackFace(Vector c, Vector a, Vector b)
        {
            Vector ab = b - a;
            Vector ac = c - a;
            Vector abnor = new Vector(-ab.Y, ab.X);//left
            return JMath.Dot(ac, abnor) < 0;
        }

        private static void FindClosestPointThreshold(Vector c, Vector a, Vector b, double threshold, out Vector cpt, out bool inVex, out bool inTrd, out bool inBack)
        {
            double lensq;
            Vector ab = b - a, ac = c - a, bc = c - b;

            double sarea = ac.X * bc.Y - ac.Y * bc.X;
            inBack = sarea < 0;

            double dacab = JMath.Dot(ac, ab);

            if (dacab <= 0)
            {
                lensq = JMath.Dot(ac, ac);
                //cpt = lensq < threshold ? new Vector(a) : null;
                inTrd = lensq < threshold;
                cpt = new Vector(a);
                inVex = true;
                return;
            }
            double dabab = JMath.Dot(ab, ab);
            if (dacab >= dabab)
            {
                lensq = JMath.Dot(bc, bc);
                //cpt = lensq < threshold ? new Vector(b) : null;
                cpt = new Vector(b);
                inVex = true;
                inTrd = lensq < threshold;
                return;
            }
            double t = dacab / dabab;
            lensq = JMath.Dot(ac, ac) - dacab * t;
            //cpt = lensq < threshold ? a + ab * t : null;
            cpt = a + ab * t;
            inTrd = lensq < threshold;
            inVex = false;
        }

        public static void ProcessClosestForce(JellyVertex[] polygona, JellyVertex[] polygonb)
        {
            List<bool> ptsio = new List<bool>();
            List<Vector> closestpts = new List<Vector>();

            /*ptsio.Clear();
            closestpts.Clear();*/

            for (int i = 0; i < polygonb.Length; ++i)
            {
                ptsio.Add(JMath.PointInPolygon(polygona, polygonb[i].Position, new Vector(-10000, -10000)));
                closestpts.Add(ClosestPoint(polygonb[i].Position, polygona));
            }

            for (int i = 0; i < polygonb.Length; ++i)
            {
                if (ptsio[i])
                {
                    polygonb[i].Position = closestpts[i];
                    polygonb[i].Velocity.X = polygonb[i].Velocity.Y = 0;
                    polygonb[i].Force.X = polygonb[i].Force.Y = 0;
                }
                else
                {
                    Vector diff = polygonb[i].Position - closestpts[i];
                    double force = 10000.0 / diff.LengthSq;// -50.0;
                    Vector vforce = diff.Normalize(force < 0 ? 0 : force);
                    polygonb[i].Force += vforce;
                }
            }
        }

        private static Vector ClosestPoint(Vector v, JellyVertex[] poly)
        {
            double minlen = double.MaxValue;
            int i = 0;
            Vector cpt = null, tcpt;
            double len;
            while (i < poly.Length - 1)
            {
                tcpt = JMath.ClosestPointOnSegment(poly[i].Position, poly[i+1].Position, v);
                ++i;
                len = JMath.LengthOf(v - tcpt);
                if (len < minlen)
                {
                    minlen = len;
                    cpt = tcpt;
                }
            }
            tcpt = JMath.ClosestPointOnSegment(poly[i].Position, poly[0].Position, v);
            len = JMath.LengthOf(v - tcpt);
            if (len < minlen)
            {
                minlen = len;
                cpt = tcpt;
            }
            return cpt;
        }
    }
}
