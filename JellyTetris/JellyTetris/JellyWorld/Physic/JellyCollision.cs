#define _DIS_RANGE_

using JellyTetris.JellyWorld.Math;

namespace JellyTetris.JellyWorld.Physic
{
    public static class JellyCollision
    {
        public static void ProcessEdgeForce(JellyVertex[] polygon, JellyVertex jv)
        {
            if (JellyMath.PointInPolygon(polygon, jv.Position, new JellyVector2(-10000, -10000)))
            {
                jv.Velocity.X = -jv.Velocity.X * JellyWorld.Friciton;
                jv.Velocity.Y = -jv.Velocity.Y * JellyWorld.Friciton;
                jv.FX = jv.FY = 0;
                jv.Position = ClosestPoint(jv.Position, polygon);
            }
            else
            {
#if _DIS_RANGE_
                const float threshold = 44.7213595499958f;
#else 
                const float threshold = 3162.27766016838f;
#endif
                const float forcek = 100000;
                int i = 0;
                JellyVector2 cpt;
                bool inVertex, inThreshold, inBack;
                while (i < polygon.Length - 1)
                {
                    FindClosestPointThreshold(jv.Position, polygon[i].Position, polygon[i + 1].Position, threshold * threshold,
                        out cpt, out inVertex, out inThreshold, out inBack);
                    ++i;
                    if (!inBack && inThreshold)
                    {
                        JellyVector2 diff = jv.Position - cpt;
                        float dissq = diff.LengthSquared();
                        dissq = dissq == 0 ? 0.001f : dissq;
#if _DIS_RANGE_
                        float norsize = forcek / dissq - 50;
#else
                        float norsize = forcek / dissq;
#endif
                        norsize = norsize < 0 ? 0 : norsize;
                        if (inVertex)
                            norsize *= 0.5f;
                        diff.Normalize();
                        diff *= norsize;
                        jv.Force += diff;
                    }
                }
                FindClosestPointThreshold(jv.Position, polygon[i].Position, polygon[0].Position, threshold * threshold,
                    out cpt, out inVertex, out inThreshold, out inBack);
                if (!inBack && inThreshold)
                {
                    JellyVector2 diff = jv.Position - cpt;
                    float dissq = diff.LengthSquared();
                    dissq = dissq == 0 ? 0.001f : dissq;
#if _DIS_RANGE_
                    float norsize = forcek / dissq - 50;
#else
                    float norsize = forcek / dissq;
#endif
                    norsize = norsize < 0 ? 0 : norsize;
                    if (inVertex)
                        norsize *= 0.5f;
                    diff.Normalize();
                    diff *= norsize;
                    jv.Force += diff;
                }
            }
        }

        private static void FindClosestPointThreshold(JellyVector2 c, JellyVector2 a, JellyVector2 b, double threshold, out JellyVector2 cpt, out bool inVex, out bool inTrd, out bool inBack)
        {
            float lensq;
            JellyVector2 ab = b - a, ac = c - a, bc = c - b;

            float sarea = ac.X * bc.Y - ac.Y * bc.X;
            inBack = sarea < 0;

            float dacab = JellyVector2.Dot(ac, ab);

            if (dacab <= 0)
            {
                lensq = JellyVector2.Dot(ac, ac);
                inTrd = lensq < threshold;
                cpt = new JellyVector2(a);
                inVex = true;
                return;
            }
            float dabab = JellyVector2.Dot(ab, ab);
            if (dacab >= dabab)
            {
                lensq = JellyVector2.Dot(bc, bc);
                cpt = new JellyVector2(b);
                inVex = true;
                inTrd = lensq < threshold;
                return;
            }
            float t = dacab / dabab;
            lensq = JellyVector2.Dot(ac, ac) - dacab * t;
            cpt = a + ab * t;
            inTrd = lensq < threshold;
            inVex = false;
        }

        private static JellyVector2 ClosestPoint(JellyVector2 v, JellyVertex[] poly)
        {
            float minlen = float.MaxValue;
            int i = 0;
            JellyVector2 cpt = null, tcpt;
            float len;
            while (i < poly.Length - 1)
            {
                tcpt = JellyMath.ClosestPointOnSegment(poly[i].Position, poly[i + 1].Position, v);
                ++i;
                len = (v - tcpt).Length();
                if (len < minlen)
                {
                    minlen = len;
                    cpt = tcpt;
                }
            }
            tcpt = JellyMath.ClosestPointOnSegment(poly[i].Position, poly[0].Position, v);
            len = (v - tcpt).Length();
            if (len < minlen)
            {
                minlen = len;
                cpt = tcpt;
            }
            return cpt;
        }
    }
}

