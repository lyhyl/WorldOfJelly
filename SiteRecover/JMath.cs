using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SiteRecover
{
    public static class JMath
    {
        public static void Swap<T>(ref T a, ref T b) { T tmp = a; a = b; b = tmp; }
        public static double LengthOf(Vector v) { return Math.Sqrt((double)(v.X * v.X + v.Y * v.Y)); }
        public static double Dot(Vector va, Vector vb) { return va.X * vb.X + va.Y * vb.Y; }
        public static double Cross(Vector va, Vector vb) { return va.X * vb.Y - va.Y * vb.X; }
        /*
         * a*b=|a|*|b|*cos angle
         * cos angle=a*b/(|a|*|b|)
         * angle = arccos (a*b/(|a|*|b|))
         */
        public static double AngleRad(Vector va, Vector vb)
        {
            return Math.Acos(JMath.Dot(va, vb) / (va.Length + vb.Length));
        }
        public static double AngleDeg(Vector va, Vector vb)
        {
            return JMath.AngleRad(va, vb) / Math.PI * 180.0;
        }
        public static double SignedTriangleArea(Vector a, Vector b, Vector c)
        {
            return (a.X - c.X) * (b.Y - c.Y) - (a.Y - c.Y) * (b.X - c.X);
        }
        public static bool SegmentTest(Vector a, Vector b, Vector c, Vector d, out Vector i)
        {
            double triaa = JMath.SignedTriangleArea(a, b, d);
            double triab = JMath.SignedTriangleArea(a, b, c);
            double triac = 0;
            double triad = 0;
            if (triaa * triab < 0.0)
            {
                triac = JMath.SignedTriangleArea(c, d, a);
                triad = triac + triab - triaa;
                if (triac * triad < 0.0)
                {
                    double t = triac / (triac - triad);
                    i = a + (b - a) * t;
                    return true;
                }
            }
            if (triaa + triab + triac + triad == 0.0)
            {
                Vector ab = b - a;
                double abdab = Dot(ab, ab);
                Vector ac = c - a;
                double abdac = Dot(ab, ac);
                Vector ad = d - a;
                double abdad = Dot(ab, ad);

                double min = Math.Min(abdac, abdad);
                double max = Math.Max(abdac, abdad);
                if (min < abdab && max > 0)
                {
                    i = (a + b + c + d) * 0.25f;
                    return true;
                }
            }
            i = null;
            return false;
        }
        public static bool SegmentTest(Vector a, Vector b, Vector c, Vector d)
        {
            double triaa = JMath.SignedTriangleArea(a, b, d);
            double triab = JMath.SignedTriangleArea(a, b, c);
            if (triaa * triab < 0.0)
            {
                double triac = JMath.SignedTriangleArea(c, d, a);
                double triad = triac + triab - triaa;
                if (triac * triad < 0.0)
                    return true;
            }
            return false;
        }
        public static bool SegmentPolygonTest(Vector a, Vector b, Vertex[] poly, out int id, out Vector ins)
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
            ins = null;
            id = int.MaxValue;
            return false;
        }
        public static bool SegmentPolygonTest(Vector a, Vector b, Vertex[] poly, out int id)
        {
            int c;
            for (c = 0; c < poly.Length - 1; )
                if (SegmentTest(a, b, poly[c].Position, poly[++c].Position))
                {
                    id = c;
                    return true;
                }
            if (SegmentTest(a, b, poly[c].Position, poly[0].Position))
            {
                id = c;
                return true;
            }
            id = int.MaxValue;
            return false;
        }
        public static bool SegmentPolygonTest(Vector a, Vector b, Vertex[] poly, out Vector ins)
        {
            int c;
            for (c = 0; c < poly.Length - 1; ++c)
                if (SegmentTest(a, b, poly[c].Position, poly[c + 1].Position, out ins))
                    return true;
            if (SegmentTest(a, b, poly[c].Position, poly[0].Position, out ins))
                return true;
            ins = null;
            return false;
        }
        public static bool PointInPolygon(Vertex[] vs, Vector pt, Vector infinity)
        {
            uint count = 0;
            uint i = 0;
            while (i < vs.Length - 1)
            {
                if (JMath.SegmentTest(pt, infinity, vs[i].Position, vs[i + 1].Position))
                    ++count;
                ++i;
            }
            if (JMath.SegmentTest(pt, infinity, vs[i].Position, vs[0].Position))
                ++count;
            return (count & 1) == 1;
        }
        public static void ComputeBezierPoint(Vector begv, Vector midv, Vector endv, ref List<PointF> ptlist, uint maxiter)
        {
            Vector vab = midv - begv;
            Vector vbc = endv - midv;

            begv = begv + vab * 0.5f;
            endv = midv + vbc * 0.5f;

            vab = midv - begv;
            vbc = endv - midv;

            uint iter = 0;
            float factor = 1.0f / (float)(maxiter - 1);
            float delta = 0.0f;

            while (iter < maxiter - 1)
            {
                Vector bab = begv + vab * delta;
                Vector bbc = midv + vbc * delta;
                Vector vbb = bbc - bab;
                Vector be = bab + vbb * delta;
                ptlist.Add(be);
                ++iter;
                delta = factor * (float)iter;
            }
        }
        public static Vector ClosestPointOnSegment(Vector a, Vector b, Vector c)
        {
            /*Vector VectorA = vPoint - vA;
            Vector diff = vB - vA;
            double d = diff.Length;
            Vector VectorB = diff.Normalize();
            double t = JMath.Dot(VectorB, VectorA);

            if (t <= 0)
                return vA;
            if (t >= d)
                return vB;

            return vA + VectorB * t;*/

            Vector ab = b - a;
            double t = Dot(c - ab, ab) / Dot(ab, ab);
            if (t < 0) return a;
            if (t > 1) return b;
            return a + ab * t;
        }
    }

    public class Vector
    {
        public float X, Y;
        public Vector() { this.X = this.Y = 0.0f; }
        public Vector(float size) { this.X = this.Y = size; }
        public Vector(float x, float y) { this.X = x; this.Y = y; }
        public Vector(Vector v) { this.X = v.X; this.Y = v.Y; }
        public Vector(PointF p) { this.X = p.X; this.Y = p.Y; }
        public Vector(SizeF s) { this.X = s.Width; this.Y = s.Height; }
        public Vector(Point p) { this.X = p.X; this.Y = p.Y; }
        public Vector(Size s) { this.X = s.Width; this.Y = s.Height; }

        public static explicit operator Point(Vector v) { return new Point((int)v.X, (int)v.Y); }
        public static explicit operator Size(Vector v) { return new Size((int)v.X, (int)v.Y); }
        public static implicit operator PointF(Vector v) { return new PointF(v.X, v.Y); }
        public static implicit operator SizeF(Vector v) { return new SizeF(v.X, v.Y); }

        public static Vector operator -(Vector va) { return new Vector(-va.X, -va.Y); }
        public static Vector operator +(Vector va, Vector vb) { return new Vector(va.X + vb.X, va.Y + vb.Y); }
        public static Vector operator -(Vector va, Vector vb) { return new Vector(va.X - vb.X, va.Y - vb.Y); }
        public static Vector operator *(Vector va, float k) { return new Vector(va.X * k, va.Y * k); }
        public static Vector operator *(Vector va, double k) { return new Vector((float)(va.X * k), (float)(va.Y * k)); }
        public static Vector operator *(Vector va, int k) { return new Vector(va.X * k, va.Y * k); }
        public static Vector operator /(Vector va, float k) { float ik = 1.0f / k; return new Vector(va.X * ik, va.Y * ik); }
        public static Vector operator /(Vector va, double k) { double ik = 1.0 / k; return new Vector((float)(va.X * ik), (float)(va.Y * ik)); }
        public static Vector operator /(Vector va, int k) { float ik = 1.0f / k; return new Vector(va.X * ik, va.Y * ik); }

        public bool Limit(float len)
        {
            if (this.LengthSq > len * len)
            {
                this.Normalize(len);
                return true;
            }
            return false;
        }

        public Vector Decrease(int len) { return this.Decrease((float)len); }
        public Vector Decrease(float len)
        {
            if (this.LengthSq > len * len)
            {
                Vector t = new Vector(this);
                t.Normalize();
                t *= len;
                this.X -= t.X;
                this.Y -= t.Y;
                return this;
            }
            else
            {
                this.X = this.Y = 0.0f;
                return this;
            }
        }
        public Vector Normalize() { float len = (float)this.Length; this.X /= len; this.Y /= len; return this; }
        public Vector Normalize(float len) { float L = (float)this.Length; this.X *= len / L; this.Y *= len / L; return this; }
        public Vector Normalize(double len) { return Normalize((float)len); }
        public double Length { get { return Math.Sqrt(this.X * this.X + this.Y * this.Y); } }
        public float LengthSq { get { return this.X * this.X + this.Y * this.Y; } }

        public bool IsZero() { return this.X == 0.0f && this.Y == 0.0f; }
        public bool IsIllegal() { return float.IsNaN(this.X) || float.IsNaN(this.Y); }
        public bool NearZero() { return Math.Abs(this.X) <= float.Epsilon && Math.Abs(this.Y) <= float.Epsilon; }
        public bool NearZero(float epsilon) { return Math.Abs(this.X) <= epsilon && Math.Abs(this.Y) <= epsilon; }

        public override string ToString()
        {
            return "(" + this.X.ToString() + "," + this.Y.ToString() + ")";
        }
    }

    public class AABB
    {
        public float Left { set; get; }
        public float Right { set; get; }
        public float Top { set; get; }
        public float Bottom { set; get; }

        public AABB(Vertex[] vs)
        {
            Left = vs[0].Position.X;
            Right = vs[0].Position.X;
            Top = vs[0].Position.Y;
            Bottom = vs[0].Position.Y;

            foreach (Vertex v in vs)
            {
                if (v.Position.X < Left)
                    Left = v.Position.X;
                if (v.Position.X > Right)
                    Right = v.Position.X;
                if (v.Position.Y < Bottom)
                    Bottom = v.Position.Y;
                if (v.Position.Y > Top)
                    Top = v.Position.Y;
            }
        }

        public AABB(Vector[] vs)
        {
            Left = vs[0].X;
            Right = vs[0].X;
            Top = vs[0].Y;
            Bottom = vs[0].Y;

            foreach (Vector v in vs)
            {
                if (v.X < Left)
                    Left = v.X;
                if (v.X > Right)
                    Right = v.X;
                if (v.Y < Bottom)
                    Bottom = v.Y;
                if (v.Y > Top)
                    Top = v.Y;
            }
        }
    }

    static public class Pair
    {
        static public Pair<T> CreatePair<T>(T a, T b) where T : new()
        {
            return new Pair<T>(a, b);
        }
    }

    public class Pair<T>
        : IComparable,
        IComparable<Pair<T>>
        where T : new()
    {
        private T a, b;
        public Pair() { this.a = new T(); this.b = new T(); }
        public Pair(Pair<T> p) { this.a = p.a; this.b = p.b; }
        public Pair(T a, T b) { this.a = a; this.b = b; }
        public Pair(ref T a, ref T b) { this.a = a; this.b = b; }
        public T A
        {
            get { return this.a; }
            set { this.a = value; }
        }
        public T B
        {
            get { return this.b; }
            set { this.b = value; }
        }
        public int CompareTo(Pair<T> o)
        {
            if (o == null) return 1;
            IComparable vo = o.a as IComparable;
            return vo == null ? 1 : vo.CompareTo(this.a as IComparable);
        }
        public int CompareTo(object o)
        {
            if (o == null) return 1;
            Pair<T> to = o as Pair<T>;
            IComparable vo = to == null ? null : to.a as IComparable;
            return vo == null ? 1 : vo.CompareTo(this.a);
        }
        public void Swap()
        {
            T t = this.a;
            this.a = this.b;
            this.b = t;
        }
        public void Swap(Pair<T> p)
        {
            Pair<T> t = p;
            p.a = this.a;
            p.b = this.b;
            this.a = t.a;
            this.b = t.b;
        }
        public static Pair<T> CreatePair(T a, T b)
        {
            return new Pair<T>(a, b);
        }
        public override string ToString()
        {
            return "<" + this.a.ToString() + "," + this.b.ToString() + ">";
        }
    }

    public class Pair<T, K>
        : IComparable,
        IComparable<Pair<T, K>>
        where T : new()
        where K : new()
    {
        private T a;
        private K b;
        public Pair() { }
        public Pair(Pair<T, K> p) { this.a = p.a; this.b = p.b; }
        public Pair(T a, K b) { this.a = a; this.b = b; }
        public Pair(ref T a, ref K b) { this.a = a; this.b = b; }
        public T A
        {
            get { return this.a; }
            set { this.a = value; }
        }
        public K B
        {
            get { return this.b; }
            set { this.b = value; }
        }
        public int CompareTo(Pair<T, K> o)
        {
            if (o == null) return 1;
            IComparable vo = o.a as IComparable;
            return vo == null ? 1 : vo.CompareTo(this.a as IComparable);
        }
        public int CompareTo(object o)
        {
            if (o == null) return 1;
            Pair<T, K> to = o as Pair<T, K>;
            IComparable vo = to == null ? null : to.a as IComparable;
            return vo == null ? 1 : vo.CompareTo(this.a);
        }
        public void Swap(Pair<T, K> p)
        {
            Pair<T, K> t = p;
            p.a = this.a;
            p.b = this.b;
            this.a = t.a;
            this.b = t.b;
        }
        public static Pair<T, K> CreatePair(T a, K b)
        {
            return new Pair<T, K>(a, b);
        }
        public override string ToString()
        {
            return "<" + this.a.ToString() + "," + this.b.ToString() + ">";
        }
    }
}
