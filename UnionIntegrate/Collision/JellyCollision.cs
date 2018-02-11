#define _DIS_RANGE_

namespace UnionIntegrate
{
    public static class JellyCollision
    {
        //private static Random rand = new Random();
        public static void ProcessEdgeForce(JellyVertex[] polygon, JellyVertex jv)
        {
            if (JMath.PointInPolygon(polygon, jv.Position, new Vector(-10000, -10000)))
            {
                jv.Velocity.X = -jv.Velocity.X * JellyWorld.Friciton;
                jv.Velocity.Y = -jv.Velocity.Y * JellyWorld.Friciton;
                jv.NxtFX = jv.NxtFY = 0;
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
                        jv.NxtForce += diff;
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
                    jv.NxtForce += diff;
                }
            }
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
