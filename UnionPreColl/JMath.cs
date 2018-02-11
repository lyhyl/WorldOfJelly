using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace UnionPreColl
{
    public static class JMath
    {
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
            double t = Dot(c - a, ab) / Dot(ab, ab);
            if (t < 0) return a;
            if (t > 1) return b;
            return a + ab * t;
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
                    i = (a + b + c + d) * 0.25;
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
            double triac = 0;
            double triad = 0;
            if (triaa * triab < 0.0)
            {
                triac = JMath.SignedTriangleArea(c, d, a);
                triad = triac + triab - triaa;
                if (triac * triad < 0.0)
                    return true;
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
                return (min < abdab && max > 0);
            }
            return false;
        }
        public static bool SegmentPolygonTest(Vector a, Vector b, JellyVertex[] poly, out int id, out Vector ins)
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
        public static bool SegmentPolygonTest(Vector a, Vector b, JellyVertex[] poly, out int id)
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
        public static bool SegmentPolygonTest(Vector a, Vector b, JellyVertex[] poly, out Vector ins)
        {
            int c = 0;
            while (c < poly.Length - 1)
                if (SegmentTest(a, b, poly[c].Position, poly[++c].Position, out ins))
                    return true;
            if (SegmentTest(a, b, poly[c].Position, poly[0].Position, out ins))
                return true;
            ins = null;
            return false;
        }
        public static bool PointInPolygon(JellyVertex[] vs, Vector pt, Vector infinity)
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
        public static void Barycentric(Vector a, Vector b, Vector c, Vector p, out double u, out double v, out double w)
        {
            Vector v0 = b - a, v1 = c - a, v2 = p - a;
            double d00 = (double)JMath.Dot(v0, v0);
            double d01 = (double)JMath.Dot(v0, v1);
            double d11 = (double)JMath.Dot(v1, v1);
            double d20 = (double)JMath.Dot(v2, v0);
            double d21 = (double)JMath.Dot(v2, v1);
            double denom = d00 * d11 - d01 * d01;
            v = (d11 * d20 - d01 * d21) / denom;
            w = (d00 * d21 - d01 * d20) / denom;
            u = 1 - v - w;
        }
        public static bool PointTriangle(Vector p, Vector a, Vector b, Vector c)
        {
            double u, v, w;
            Barycentric(a, b, c, p, out u, out v, out w);
            return v >= 0.0 && w >= 0.0 && (v + w) <= 1.0;
        }
        public static bool PointTriangle(Vector p, Vector a, Vector b, Vector c, out double u, out double v, out double w)
        {
            Barycentric(a, b, c, p, out u, out v, out w);
            return v >= 0.0 && w >= 0.0 && (v + w) <= 1.0;
        }
        public static void ComputeBezierPoint(Vector begv, Vector midv, Vector endv, ref List<Vector> ptlist, uint maxiter)
        {
            Vector vab = midv - begv;
            Vector vbc = endv - midv;

            begv = begv + vab * 0.5;
            endv = midv + vbc * 0.5;

            vab = midv - begv;
            vbc = endv - midv;

            uint iter = 0;
            double factor = 1.0 / (double)(maxiter - 1);
            double delta = 0.0;

            while (iter < maxiter - 1)
            {
                Vector bab = begv + vab * delta;
                Vector bbc = midv + vbc * delta;
                Vector vbb = bbc - bab;
                Vector be = bab + vbb * delta;
                ptlist.Add(be);
                ++iter;
                delta = factor * (double)iter;
            }
        }
    }

    public class Vector
    {
        public double X { set; get; }
        public double Y { set; get; }
        public Vector() { this.X = this.Y = 0.0; }
        public Vector(double size) { this.X = this.Y = size; }
        public Vector(double x, double y) { this.X = x; this.Y = y; }
        public Vector(Vector v) { this.X = v.X; this.Y = v.Y; }
        public Vector(PointF p) { this.X = p.X; this.Y = p.Y; }
        public Vector(SizeF s) { this.X = s.Width; this.Y = s.Height; }
        public Vector(Point p) { this.X = p.X; this.Y = p.Y; }
        public Vector(Size s) { this.X = s.Width; this.Y = s.Height; }

        public static explicit operator Point(Vector v) { return new Point((int)v.X, (int)v.Y); }
        public static explicit operator Size(Vector v) { return new Size((int)v.X, (int)v.Y); }
        public static implicit operator PointF(Vector v) { return new PointF((float)v.X, (float)v.Y); }
        public static implicit operator SizeF(Vector v) { return new SizeF((float)v.X, (float)v.Y); }

        public static Vector operator -(Vector va) { return new Vector(-va.X, -va.Y); }
        public static Vector operator +(Vector va, Vector vb) { return new Vector(va.X + vb.X, va.Y + vb.Y); }
        public static Vector operator -(Vector va, Vector vb) { return new Vector(va.X - vb.X, va.Y - vb.Y); }
        public static Vector operator *(Vector va, double k) { return new Vector(va.X * k, va.Y * k); }
        public static Vector operator *(Vector va, int k) { return new Vector(va.X * k, va.Y * k); }
        public static Vector operator /(Vector va, double k) { double ik = 1.0 / k; return new Vector(va.X * ik, va.Y * ik); }
        public static Vector operator /(Vector va, int k) { double ik = 1.0 / k; return new Vector(va.X * ik, va.Y * ik); }

        public bool Limit(double len)
        {
            if (this.LengthSq > len * len)
            {
                this.Normalize(len);
                return true;
            }
            return false;
        }

        public Vector Decrease(int len) { return this.Decrease((double)len); }
        public Vector Decrease(double len)
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
                this.X = this.Y = 0.0;
                return this;
            }
        }
        public Vector Normalize() { double len = (double)this.Length; if (Length == 0.0)return this; this.X /= len; this.Y /= len; return this; }
        public Vector Normalize(double len) { double L = (double)this.Length; if (Length == 0.0)return this; this.X *= len / L; this.Y *= len / L; return this; }
        public double Length { get { return Math.Sqrt(this.X * this.X + this.Y * this.Y); } }
        public double LengthSq { get { return this.X * this.X + this.Y * this.Y; } }

        public bool IsZero() { return this.X == 0.0 && this.Y == 0.0; }
        public bool IsIllegal() { return double.IsNaN(this.X) || double.IsNaN(this.Y); }
        public bool NearZero() { return Math.Abs(this.X) <= double.Epsilon && Math.Abs(this.Y) <= double.Epsilon; }
        public bool NearZero(double epsilon) { return Math.Abs(this.X) <= epsilon && Math.Abs(this.Y) <= epsilon; }

        public override string ToString()
        {
            return "(" + this.X.ToString() + "," + this.Y.ToString() + ")";
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

    public class FourMapping<T, K, P, Q>
        : IComparable,
        IComparable<FourMapping<T, K, P, Q>>
    {
        private T t;
        private K k;
        private P p;
        private Q q;
        public T Key { get { return this.t; } set { this.t = value; } }
        public K VA { get { return this.k; } set { this.k = value; } }
        public P VB { get { return this.p; } set { this.p = value; } }
        public Q VC { get { return this.q; } set { this.q = value; } }
        public FourMapping()
        {
        }
        public FourMapping(T a, K b, P c, Q d)
        {
            this.t = a;
            this.k = b;
            this.p = c;
            this.q = d;
        }
        public int CompareTo(FourMapping<T, K, P, Q> o)
        {
            if (o == null) return 1;
            IComparable vo = o.k as IComparable;
            return vo == null ? 1 : vo.CompareTo(this.k as IComparable);
        }
        public int CompareTo(object o)
        {
            if (o == null) return 1;
            FourMapping<T, K, P, Q> to = o as FourMapping<T, K, P, Q>;
            IComparable vo = to == null ? null : to.k as IComparable;
            return vo == null ? 1 : vo.CompareTo(this.k);
        }
        public override string ToString()
        {
            return "<" + this.t.ToString() + "," + this.k.ToString() + "," + this.p.ToString() + "," + this.q.ToString() + ">";
        }
    }
    public class FourMapping<T, K>
        : FourMapping<T, T, K, K>
    {
        public FourMapping(T a, T b, K c, K d) : base(a, b, c, d) { }
    }

    public class Matrix<T>
    {
        public class Rows
        {
            private Matrix<T> m;
            private MatrixRow rowa, rowb, rowc;
            internal Rows(Matrix<T> matrix)
            {
                this.m = matrix;
                this.rowa = new Matrix<T>.MatrixRow(ref matrix, 0);
                this.rowb = new Matrix<T>.MatrixRow(ref matrix, 1);
                this.rowc = new Matrix<T>.MatrixRow(ref matrix, 2);
            }
            public MatrixRow this[int index]
            {
                get
                {
                    switch (index)
                    {
                        case 0: return this.rowa;
                        case 1: return this.rowb;
                        case 2: return this.rowc;
                        default: return null;
                    }
                }
            }
        }
        public class Colunms
        {
            private Matrix<T> m;
            private MatrixCol cola, colb, colc;
            internal Colunms(Matrix<T> matrix)
            {
                this.m = matrix;
                this.cola = new Matrix<T>.MatrixCol(ref matrix, 0);
                this.colb = new Matrix<T>.MatrixCol(ref matrix, 1);
                this.colc = new Matrix<T>.MatrixCol(ref matrix, 2);
            }
            public MatrixCol this[int index]
            {
                get
                {
                    switch (index)
                    {
                        case 0: return this.cola;
                        case 1: return this.colb;
                        case 2: return this.colc;
                        default: return null;
                    }
                }
            }
        }
        public class MatrixRow
        {
            private Matrix<T> m;
            private uint ind;
            internal MatrixRow(ref Matrix<T> matrix, uint index)
            {
                this.m = matrix;
                this.ind = index;
            }
            public T this[int index]
            {
                get
                {
                    switch ((this.ind << 2) & index)
                    {
                        case (0 << 2) | 0: return this.m.a;
                        case (0 << 2) | 1: return this.m.b;
                        case (0 << 2) | 2: return this.m.c;

                        case (1 << 2) | 0: return this.m.d;
                        case (1 << 2) | 1: return this.m.e;
                        case (1 << 2) | 2: return this.m.f;

                        case (2 << 2) | 0: return this.m.g;
                        case (2 << 2) | 1: return this.m.h;
                        case (2 << 2) | 2: return this.m.i;

                        default: return default(T);
                    }
                }
                set
                {
                    switch ((this.ind << 2) & index)
                    {
                        case (0 << 2) | 0: this.m.a = value; break;
                        case (0 << 2) | 1: this.m.b = value; break;
                        case (0 << 2) | 2: this.m.c = value; break;

                        case (1 << 2) | 0: this.m.d = value; break;
                        case (1 << 2) | 1: this.m.e = value; break;
                        case (1 << 2) | 2: this.m.f = value; break;

                        case (2 << 2) | 0: this.m.g = value; break;
                        case (2 << 2) | 1: this.m.h = value; break;
                        case (2 << 2) | 2: this.m.i = value; break;

                        default: break;
                    }
                }
            }
        }
        public class MatrixCol
        {
            private Matrix<T> m;
            private uint ind;
            internal MatrixCol(ref Matrix<T> matrix, uint index)
            {
                this.m = matrix;
                this.ind = index;
            }
            public T this[int index]
            {
                get
                {
                    switch ((this.ind << 2) & index)
                    {
                        case (0 << 2) | 0: return this.m.a;
                        case (0 << 2) | 1: return this.m.d;
                        case (0 << 2) | 2: return this.m.g;

                        case (1 << 2) | 0: return this.m.b;
                        case (1 << 2) | 1: return this.m.e;
                        case (1 << 2) | 2: return this.m.h;

                        case (2 << 2) | 0: return this.m.c;
                        case (2 << 2) | 1: return this.m.f;
                        case (2 << 2) | 2: return this.m.i;

                        default: return default(T);
                    }
                }
                set
                {
                    switch ((this.ind << 2) & index)
                    {
                        case (0 << 2) | 0: this.m.a = value; break;
                        case (0 << 2) | 1: this.m.d = value; break;
                        case (0 << 2) | 2: this.m.g = value; break;

                        case (1 << 2) | 0: this.m.b = value; break;
                        case (1 << 2) | 1: this.m.e = value; break;
                        case (1 << 2) | 2: this.m.h = value; break;

                        case (2 << 2) | 0: this.m.c = value; break;
                        case (2 << 2) | 1: this.m.f = value; break;
                        case (2 << 2) | 2: this.m.i = value; break;

                        default: break;
                    }
                }
            }
        }

        private T
            a, b, c,
            d, e, f,
            g, h, i;

        public T A { get { return this.a; } set { this.a = value; } }
        public T B { get { return this.b; } set { this.b = value; } }
        public T C { get { return this.c; } set { this.c = value; } }
        public T D { get { return this.d; } set { this.d = value; } }
        public T E { get { return this.e; } set { this.e = value; } }
        public T F { get { return this.f; } set { this.f = value; } }
        public T G { get { return this.g; } set { this.g = value; } }
        public T H { get { return this.h; } set { this.h = value; } }
        public T I { get { return this.i; } set { this.i = value; } }

        public T Ele11 { get { return this.a; } set { this.a = value; } }
        public T Ele12 { get { return this.b; } set { this.b = value; } }
        public T Ele13 { get { return this.c; } set { this.c = value; } }
        public T Ele21 { get { return this.d; } set { this.d = value; } }
        public T Ele22 { get { return this.e; } set { this.e = value; } }
        public T Ele23 { get { return this.f; } set { this.f = value; } }
        public T Ele31 { get { return this.g; } set { this.g = value; } }
        public T Ele32 { get { return this.h; } set { this.h = value; } }
        public T Ele33 { get { return this.i; } set { this.i = value; } }

        public T _Ele00 { get { return this.a; } set { this.a = value; } }
        public T _Ele01 { get { return this.b; } set { this.b = value; } }
        public T _Ele02 { get { return this.c; } set { this.c = value; } }
        public T _Ele10 { get { return this.d; } set { this.d = value; } }
        public T _Ele11 { get { return this.e; } set { this.e = value; } }
        public T _Ele12 { get { return this.f; } set { this.f = value; } }
        public T _Ele20 { get { return this.g; } set { this.g = value; } }
        public T _Ele21 { get { return this.h; } set { this.h = value; } }
        public T _Ele22 { get { return this.i; } set { this.i = value; } }

        public Matrix()
        {
        }
        public Matrix(T v)
        {
            this.a = v; this.b = v; this.c = v;
            this.d = v; this.e = v; this.f = v;
            this.g = v; this.h = v; this.i = v;
        }
        public Matrix(Matrix<T> m)
        {
            this.a = m.a; this.b = m.b; this.c = m.c;
            this.d = m.d; this.e = m.e; this.f = m.f;
            this.g = m.g; this.h = m.h; this.i = m.i;
        }

        public Rows Row { get { return new Rows(this); } }
    }
}
