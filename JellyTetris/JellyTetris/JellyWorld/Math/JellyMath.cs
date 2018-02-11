using System;
using JellyTetris.JellyWorld;
using JellyTetris.JellyWorld.Math;

namespace JellyTetris.JellyWorld.Math
{
    public static class JellyMath
    {
        public static float Cross(JellyVector2 va, JellyVector2 vb) { return va.X * vb.Y - va.Y * vb.X; }
        public static float SignedTriangleArea(JellyVector2 a, JellyVector2 b, JellyVector2 c)
        {
            return (a.X - c.X) * (b.Y - c.Y) - (a.Y - c.Y) * (b.X - c.X);
        }
        public static JellyVector2 ClosestPointOnSegment(JellyVector2 a, JellyVector2 b, JellyVector2 c)
        {
            JellyVector2 ab = b - a;
            float t = JellyVector2.Dot(c - a, ab) / JellyVector2.Dot(ab, ab);
            if (t < 0) return a;
            if (t > 1) return b;
            return a + ab * t;
        }
        public static bool SegmentTest(JellyVector2 a, JellyVector2 b, JellyVector2 c, JellyVector2 d, out JellyVector2 i)
        {
            float triaa = SignedTriangleArea(a, b, d);
            float triab = SignedTriangleArea(a, b, c);
            float triac = 0;
            float triad = 0;
            if (triaa * triab < 0.0)
            {
                triac = SignedTriangleArea(c, d, a);
                triad = triac + triab - triaa;
                if (triac * triad < 0.0)
                {
                    float t = triac / (triac - triad);
                    i = a + (b - a) * t;
                    return true;
                }
            }
            if (triaa + triab + triac + triad == 0.0)
            {
                JellyVector2 ab = b - a;
                float abdab = JellyVector2.Dot(ab, ab);
                JellyVector2 ac = c - a;
                float abdac = JellyVector2.Dot(ab, ac);
                JellyVector2 ad = d - a;
                float abdad = JellyVector2.Dot(ab, ad);

                float min = System.Math.Min(abdac, abdad);
                float max = System.Math.Max(abdac, abdad);
                if (min < abdab && max > 0)
                {
                    i = (a + b + c + d) * 0.25f;
                    return true;
                }
            }
            i = JellyVector2.Zero;//it should be null,change in XNA
            return false;
        }
        public static bool SegmentTest(JellyVector2 a, JellyVector2 b, JellyVector2 c, JellyVector2 d)
        {
            float triaa = SignedTriangleArea(a, b, d);
            float triab = SignedTriangleArea(a, b, c);
            float triac = 0;
            float triad = 0;
            if (triaa * triab < 0.0)
            {
                triac = SignedTriangleArea(c, d, a);
                triad = triac + triab - triaa;
                if (triac * triad < 0.0)
                    return true;
            }
            if (triaa + triab + triac + triad == 0.0)
            {
                JellyVector2 ab = b - a;
                float abdab = JellyVector2.Dot(ab, ab);
                JellyVector2 ac = c - a;
                float abdac = JellyVector2.Dot(ab, ac);
                JellyVector2 ad = d - a;
                float abdad = JellyVector2.Dot(ab, ad);

                float min = System.Math.Min(abdac, abdad);
                float max = System.Math.Max(abdac, abdad);
                return (min < abdab && max > 0);
            }
            return false;
        }
        public static bool SegmentPolygonTest(JellyVector2 a, JellyVector2 b, JellyVertex[] poly, out int id, out JellyVector2 ins)
        {
            int c;
            for (c = 0; c < poly.Length - 1; ++c)
                if (SegmentTest(a, b, poly[c].Position, poly[c + 1].Position, out ins))
                {
                    id = c;
                    return true;
                }
            if (SegmentTest(a, b, poly[c].Position, poly[0].Position, out ins))
            {
                id = c;
                return true;
            }
            ins = JellyVector2.Zero;//it should be null,change in XNA
            id = int.MaxValue;
            return false;
        }
        public static bool SegmentPolygonTest(JellyVector2 a, JellyVector2 b, JellyVertex[] poly, out int id)
        {
            id = 0;
            while (id < poly.Length - 1)
                if (SegmentTest(a, b, poly[id].Position, poly[++id].Position))
                    return true;
            if (SegmentTest(a, b, poly[id].Position, poly[0].Position))
                return true;
            id = int.MaxValue;
            return false;
        }
        public static bool SegmentPolygonTest(JellyVector2 a, JellyVector2 b, JellyVertex[] poly, out JellyVector2 ins)
        {
            int c = 0;
            while (c < poly.Length - 1)
                if (SegmentTest(a, b, poly[c].Position, poly[++c].Position, out ins))
                    return true;
            if (SegmentTest(a, b, poly[c].Position, poly[0].Position, out ins))
                return true;
            ins = JellyVector2.Zero;//it should be null,change in XNA
            return false;
        }
        public static bool PointInPolygon(JellyVertex[] vs, JellyVector2 pt, JellyVector2 infinity)
        {
            uint count = 0;
            uint i = 0;
            while (i < vs.Length - 1)
            {
                if (SegmentTest(pt, infinity, vs[i].Position, vs[i + 1].Position))
                    ++count;
                ++i;
            }
            if (SegmentTest(pt, infinity, vs[i].Position, vs[0].Position))
                ++count;
            return (count & 1) == 1;
        }
        public static void Barycentric(JellyVector2 a, JellyVector2 b, JellyVector2 c, JellyVector2 p, out float u, out float v, out float w)
        {
            JellyVector2 v0 = b - a, v1 = c - a, v2 = p - a;
            float d00 = JellyVector2.Dot(v0, v0);
            float d01 = JellyVector2.Dot(v0, v1);
            float d11 = JellyVector2.Dot(v1, v1);
            float d20 = JellyVector2.Dot(v2, v0);
            float d21 = JellyVector2.Dot(v2, v1);
            float denom = d00 * d11 - d01 * d01;
            v = (d11 * d20 - d01 * d21) / denom;
            w = (d00 * d21 - d01 * d20) / denom;
            u = 1 - v - w;
        }
        public static bool PointTriangle(JellyVector2 p, JellyVector2 a, JellyVector2 b, JellyVector2 c)
        {
            float u, v, w;
            Barycentric(a, b, c, p, out u, out v, out w);
            return v >= 0.0 && w >= 0.0 && (v + w) <= 1.0;
        }
        public static bool PointTriangle(JellyVector2 p, JellyVector2 a, JellyVector2 b, JellyVector2 c, out float u, out float v, out float w)
        {
            Barycentric(a, b, c, p, out u, out v, out w);
            return v >= 0.0 && w >= 0.0 && (v + w) <= 1.0;
        }
        public static void ComputeBezierPoint(JellyVector2 begv, JellyVector2 midv, JellyVector2 endv, ref JellyVector2[] ptlist, ref int sind, int maxiter)
        {
            JellyVector2 vab = midv - begv;
            JellyVector2 vbc = endv - midv;

            begv = begv + vab * 0.5f;
            endv = midv + vbc * 0.5f;

            vab = midv - begv;
            vbc = endv - midv;

            int iter = 0;
            float factor = 1.0f / maxiter;
            float delta = 0;

            while (iter < maxiter)
            {
                JellyVector2 bab = begv + vab * delta;
                JellyVector2 bbc = midv + vbc * delta;
                JellyVector2 vbb = bbc - bab;
                JellyVector2 be = bab + vbb * delta;
                ptlist[sind + iter] = be;
                ++iter;
                delta = factor * iter;
            }

            sind += iter;
        }
    }
    public class Pair<T>
    {
        public Pair() { }
        public Pair(Pair<T> p) { this.A = p.A; this.B = p.B; }
        public Pair(T a, T b) { this.A = a; this.B = b; }
        public Pair(ref T a, ref T b) { this.A = a; this.B = b; }
        public T A { get; set; }
        public T B { get; set; }
        public override string ToString()
        {
            return "<" + this.A.ToString() + "," + this.B.ToString() + ">";
        }
    }
    public class Pair<T, K>
    {
        public Pair() { }
        public Pair(Pair<T, K> p) { this.A = p.A; this.B = p.B; }
        public Pair(T a, K b) { this.A = a; this.B = b; }
        public Pair(ref T a, ref K b) { this.A = a; this.B = b; }
        public T A { get; set; }
        public K B { get; set; }
        public override string ToString()
        {
            return "<" + this.A.ToString() + "," + this.B.ToString() + ">";
        }
    }
    public class Triple<T>
    {
        public Triple() { }
        public Triple(Triple<T> p) { this.A = p.A; this.B = p.B; this.C = p.C; }
        public Triple(T a, T b, T c) { this.A = a; this.B = b; this.C = c; }
        public Triple(ref T a, ref T b, ref T c) { this.A = a; this.B = b; this.C = c; }
        public T A { get; set; }
        public T B { get; set; }
        public T C { get; set; }
        public override string ToString()
        {
            return "<" + this.A.ToString() + "," + this.B.ToString() + "'" + this.C.ToString() + ">";
        }
    }
}
