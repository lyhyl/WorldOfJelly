using Microsoft.Xna.Framework;

namespace JellyTetris.JellyWorld.Math
{
    public class JellyVector2
    {
        /*private Vector2 v;

        public JVector2() { v = Vector2.Zero; }
        public JVector2(Vector2 v2) { v = v2; }
        public JVector2(float x, float y) { v.X = x; v.Y = y; }

        public static JVector2 Zero { get { return new JVector2(); } }

        public float X { get { return v.X; } set { v.X = value; } }
        public float Y { get { return v.Y; } set { v.Y = value; } }
        public void Normalize() { v.Normalize(); }
        public float Length() { return v.Length(); }
        public float LengthSquared() { return v.LengthSquared(); }
        public override string ToString() { return v.ToString(); }

        public static JVector2 operator +(JVector2 a, JVector2 b) { return new JVector2(a.v + b.v); }
        public static JVector2 operator -(JVector2 v) { return new JVector2(-v.v); }
        public static JVector2 operator -(JVector2 a, JVector2 b) { return new JVector2(a.v + b.v); }
        public static JVector2 operator *(JVector2 v, float f) { return new JVector2(v.v * f); }
        public static JVector2 operator /(JVector2 v, float f) { return new JVector2(v.v / f); }
        public static implicit operator Vector2(JVector2 jv) { return jv.v; }

        public static float Dot(JVector2 a, JVector2 b) { return a.X * b.X + a.Y * b.Y; }*/

        public static JellyVector2 Zero { get { return new JellyVector2(); } }

        public float X { set; get; }
        public float Y { set; get; }
        public JellyVector2() { this.X = this.Y = 0.0f; }
        public JellyVector2(float size) { this.X = this.Y = size; }
        public JellyVector2(float x, float y) { this.X = x; this.Y = y; }
        public JellyVector2(JellyVector2 v) { this.X = v.X; this.Y = v.Y; }

        public static implicit operator Vector2(JellyVector2 jv) { return new Vector2(jv.X, jv.Y); }

        public static JellyVector2 operator -(JellyVector2 va) { return new JellyVector2(-va.X, -va.Y); }
        public static JellyVector2 operator +(JellyVector2 va, JellyVector2 vb) { return new JellyVector2(va.X + vb.X, va.Y + vb.Y); }
        public static JellyVector2 operator -(JellyVector2 va, JellyVector2 vb) { return new JellyVector2(va.X - vb.X, va.Y - vb.Y); }
        public static JellyVector2 operator *(JellyVector2 va, float k) { return new JellyVector2(va.X * k, va.Y * k); }
        public static JellyVector2 operator *(JellyVector2 va, int k) { return new JellyVector2(va.X * k, va.Y * k); }
        public static JellyVector2 operator /(JellyVector2 va, float k) { float ik = 1.0f / k; return new JellyVector2(va.X * ik, va.Y * ik); }
        public static JellyVector2 operator /(JellyVector2 va, int k) { float ik = 1.0f / k; return new JellyVector2(va.X * ik, va.Y * ik); }

        public JellyVector2 Normalize() { float len = Length(); if (len == 0.0f)return this; this.X /= len; this.Y /= len; return this; }

        public float Length() { return (float)System.Math.Sqrt((double)LengthSquared()); }
        public float LengthSquared() { return this.X * this.X + this.Y * this.Y; }

        public static float Dot(JellyVector2 a, JellyVector2 b) { return a.X * b.X + a.Y * b.Y; }

        public bool IsZero() { return this.X == 0.0 && this.Y == 0.0; }
        public bool IsIllegal() { return float.IsNaN(X) || float.IsNaN(Y); }

        public override string ToString()
        {
            return "(" + this.X.ToString() + "," + this.Y.ToString() + ")";
        }
    }
}
