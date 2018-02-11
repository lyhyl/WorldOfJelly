using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiteRecover
{
    public static class JellyCollision
    {
        public static void RestrictPolygon(Vertex[] poly, ref List<Vector> inters, ref List<int> discons)
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

        public static void ProcessCollisionB(Vertex[] polya, Vertex[] polyb, ref List<bool> io_list, ref List<int> discon_list, ref List<Vector> inter_list)
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

        private static void GetIntersetions(Vertex[] pa, Vertex[] pb, ref List<int> discons, ref List<Vector> inters)
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
                    throw new Exception("assert failed.");
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

                    while (io_states[c]) ++c;

                    //foreach (bool s in io_states)
                    //    if (s) ++c; else break;

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

        private static void GetInOutState(Vertex[] pa, Vertex[] pb, ref List<bool> io_states)
        {
            foreach (Vertex v in pa)
                io_states.Add(JMath.PointInPolygon(pb, v.Position, new Vector(0, -500)));
        }

        public static void ProcessCollision(Vertex[] polya, Vertex[] polyb, ref List<Pair<int>> idpairs, ref List<Pair<Vector>> ptpairs)
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
                if (insa == null || insb == null)
                    return;
                // !!! TEMPORARY !!! Unsolved
                idpairs.Add(Pair.CreatePair((id + 1) % polya.Length, idbuf));
                ptpairs.Add(Pair.CreatePair(insa, insb));
            }
        }

        private static void FindInPairs(Vertex[] polya, Vertex[] polyb, ref bool usefirst, int indexa, int indexb, ref int id, ref Vector insa, ref Vector insb, ref List<Pair<int>> idpairs, ref List<Pair<Vector>> ptpairs)
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

        public static void Restrict(Vertex[] polygon, ref List<Pair<int>> InsIDPairs, ref List<Pair<Vector>> InsPtPairs, ref List<Vector> restrictpt)
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
        public static void ApplyRestrict(Vertex[] polygon, ref List<Pair<int>> InsIDPairs, ref List<Vector> restrictpt)
        {
            //apply
            for (int c = 0; c < InsIDPairs.Count; ++c)
            {
                int ass = 0;
                for (int i = InsIDPairs[c].A; i <= InsIDPairs[c].B; ++i, ++ass)
                    polygon[i].Position = restrictpt[ass];
            }
        }
    }
}
