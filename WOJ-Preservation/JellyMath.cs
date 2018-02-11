using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace UnionPreColl
{
    public static class JellyMath
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
            return Math.Acos(JellyMath.Dot(va, vb) / (va.Length + vb.Length));
        }
        public static double AngleDeg(Vector va, Vector vb)
        {
            return JellyMath.AngleRad(va, vb) / Math.PI * 180.0;
        }
        public static double SignedTriangleArea(Vector a, Vector b, Vector c)
        {
            return (a.X - c.X) * (b.Y - c.Y) - (a.Y - c.Y) * (b.X - c.X);
        }
        public static bool SegmentTest(Vector a, Vector b, Vector c, Vector d, out Vector i)
        {
            double triaa = JellyMath.SignedTriangleArea(a, b, d);
            double triab = JellyMath.SignedTriangleArea(a, b, c);
            if (triaa * triab < 0.0)
            {
                double triac = JellyMath.SignedTriangleArea(c, d, a);
                double triad = triac + triab - triaa;
                if (triac * triad < 0.0)
                {
                    double t = triac / (triac - triad);
                    i = a + (b - a) * t;
                    return true;
                }
            }
            i = null;
            return false;
        }
        public static bool SegmentTest(Vector a, Vector b, Vector c, Vector d)
        {
            double triaa = JellyMath.SignedTriangleArea(a, b, d);
            double triab = JellyMath.SignedTriangleArea(a, b, c);
            if (triaa * triab < 0.0)
            {
                double triac = JellyMath.SignedTriangleArea(c, d, a);
                double triad = triac + triab - triaa;
                if (triac * triad < 0.0)
                    return true;
            }
            return false;
        }
        public static void Barycentric(Vector a, Vector b, Vector c, Vector p, out float u, out float v, out float w)
        {
            Vector v0 = b - a, v1 = c - a, v2 = p - a;
            float d00 = (float)JellyMath.Dot(v0, v0);
            float d01 = (float)JellyMath.Dot(v0, v1);
            float d11 = (float)JellyMath.Dot(v1, v1);
            float d20 = (float)JellyMath.Dot(v2, v0);
            float d21 = (float)JellyMath.Dot(v2, v1);
            float denom = d00 * d11 - d01 * d01;
            v = (d11 * d20 - d01 * d21) / denom;
            w = (d00 * d21 - d01 * d20) / denom;
            u = 1 - v - w;
        }
        public static bool PointTriangle(Vector p, Vector a, Vector b, Vector c)
        {
            float u, v, w;
            Barycentric(a, b, c, p, out u, out v, out w);
            return v >= 0.0f && w >= 0.0f && (v + w) <= 1.0f;
        }
        public static bool PointTriangle(Vector p, Vector a, Vector b, Vector c, out float u, out float v, out float w)
        {
            Barycentric(a, b, c, p, out u, out v, out w);
            return v >= 0.0f && w >= 0.0f && (v + w) <= 1.0f;
        }
        public static void ComputeBezierPoint(Vector begv, Vector midv, Vector endv, ref List<PointF> ptlist, uint maxiter)
        {
            Pen blackpen = new Pen(Color.Black);
            Pen bluepen = new Pen(Color.Blue);
            Pen redpen = new Pen(Color.Red);

            Vector vab = midv - begv;
            Vector vbc = endv - midv;

            begv = begv + vab * 0.5f;
            endv = midv + vbc * 0.5f;

            vab = midv - begv;
            vbc = endv - midv;

            PointF begpt = begv;
            PointF midpt = midv;
            PointF endpt = endv;

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
    }

    public class Vector
    {
        public float X { set; get; }
        public float Y { set; get; }
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
        public Vector Normalize() { float len = (float)this.Length; if (Length == 0.0)return this; this.X /= len; this.Y /= len; return this; }
        public Vector Normalize(float len) { float L = (float)this.Length; if (Length == 0.0)return this; this.X *= len / L; this.Y *= len / L; return this; }
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
