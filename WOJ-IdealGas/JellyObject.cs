using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using WOJ_IdealGas.Spring;

namespace WOJ_IdealGas.JellyObjects
{
    public class JellyObject
    {
        protected JellyVertex[] _vertices;
        protected EdgeSpring[] _edge_springs;
        protected InternalSpring[] _internal_springs;

        protected float Pressure { set; get; }
        protected float KS { set; get; }
        protected float KD { set; get; }
        protected int _vertexcount;
        public int VertexCount { get { return _vertexcount; } }

        public const float InfMass = float.MaxValue;
        public float Mass { set; get; }

        protected JellyObject()
        {
            Pressure = 5000000f;
            KS = 5000f;
            KD = 5000f;//500
        }

        public JellyObject(VectorF[] vertices, float mass)
        {
            vertices.CopyTo(_vertices, 0);
            _vertexcount = vertices.Length;

            _edge_springs = new EdgeSpring[_vertexcount];
            int i;
            for (i = 0; i < _vertexcount - 1; ++i)
                AddEdgeSpring(i, i, i + 1);
            AddEdgeSpring(i, i, 0);

            Mass = mass;

            Pressure = 1000000f;
            KS = 5000f;
            KD = 5000f;//500
        }

        public float GetVolume()
        {
            float volume = 0;
            for (int i = 0; i < _vertexcount - 1; ++i)
                volume += (float)JellyMath.Cross(_vertices[i].Position, _vertices[i + 1].Position);
            return Math.Abs(volume);
        }

        protected void AddEdgeSpring(int springid, int indexa, int indexb)
        {
            _edge_springs[springid] = new EdgeSpring();
            _edge_springs[springid].IndexA = indexa;
            _edge_springs[springid].IndexB = indexb;
            _edge_springs[springid].Length = (float)(_vertices[indexa].Position - _vertices[indexb].Position).Length;
        }

        protected void AddInternalSpring(int springid, int indexa, int indexb)
        {
            _internal_springs[springid] = new InternalSpring();
            _internal_springs[springid].IndexA = indexa;
            _internal_springs[springid].IndexB = indexb;
            _internal_springs[springid].Length = (float)(_vertices[indexa].Position - _vertices[indexb].Position).Length;
        }

        public void Update(float delay)
        {
            GravityForce();
            SpringForce();
            PressureForce();
            IntegrateEuler(delay);
        }

        private void GravityForce()
        {
            foreach (JellyVertex jv in _vertices)
            {
                jv.FX = 0;
                jv.FY = Mass * -200f;
            }
        }

        private void SpringForce()
        {
            foreach (EdgeSpring spr in _edge_springs)
            {
                VectorF diff = _vertices[spr.IndexA].Position - _vertices[spr.IndexB].Position;
                float dis = (float)diff.Length;
                float invdis = 1.0f / dis;

                if (dis != 0.0f)
                {
                    VectorF v = _vertices[spr.IndexA].Velocity - _vertices[spr.IndexB].Velocity;

                    float factor = (dis - spr.Length) * KS + (float)JellyMath.Dot(v, diff) * KD * invdis;

                    VectorF f = diff * (factor * invdis);

                    _vertices[spr.IndexA].Force -= f;
                    _vertices[spr.IndexB].Force += f;
                }

                spr.NVX = diff.Y * invdis;
                spr.NVY = -diff.X * invdis;
            }
            if (_internal_springs != null)
                foreach (InternalSpring spr in _internal_springs)
                {
                    VectorF diff = _vertices[spr.IndexA].Position - _vertices[spr.IndexB].Position;
                    float dis = (float)diff.Length;
                    float invdis = 1.0f / dis;

                    if (dis != 0.0f)
                    {
                        VectorF v = _vertices[spr.IndexA].Velocity - _vertices[spr.IndexB].Velocity;

                        float factor = (dis - spr.Length) * KS + (float)JellyMath.Dot(v, diff) * KD * invdis;

                        VectorF f = diff * (factor * invdis);

                        _vertices[spr.IndexA].Force -= f;
                        _vertices[spr.IndexB].Force += f;
                    }
                }
        }

        private void PressureForce()
        {
            float volume = GetVolume();
            if (volume == 0.0)
            {
                return;
            }
            for (int i = 0; i < _vertexcount; ++i)
            {
                VectorF diff = _vertices[_edge_springs[i].IndexA].Position - _vertices[_edge_springs[i].IndexB].Position;
                float dis = (float)diff.Length;
                float pressurev = dis * Pressure  / volume;

                VectorF f = _edge_springs[i].NormalVector * pressurev;

                _vertices[_edge_springs[i].IndexA].Force += f;
                _vertices[_edge_springs[i].IndexB].Force += f;
            }
        }

        private void IntegrateEuler(float delay)
        {
            float dry;
            foreach (JellyVertex jv in _vertices)
            {
                /* x */
                jv.VX += jv.FX / Mass * delay;
                jv.X += jv.VX * delay;
                /* y */
                jv.VY += jv.FY / Mass * delay;

                dry = jv.VY * delay;
                /* Boundaries Y */
                if (jv.Y + dry < -200)
                {
                    dry = -200 - jv.Y;
                    jv.VY = -0.1f * jv.VY;
                }
                jv.Y += dry;
            }
        }

        public virtual void DebugDraw(Graphics g)
        {
            Pen RedPen = new Pen(Color.Red);
            Pen BluePen = new Pen(Color.Blue);

            foreach (JellyVertex jv in _vertices)
                g.DrawEllipse(RedPen, jv.X - 3, jv.Y - 3, 6, 6);

            foreach (EdgeSpring spr in _edge_springs)
            {
                VectorF va = _vertices[spr.IndexA].Position;
                VectorF vb = _vertices[spr.IndexB].Position;
                VectorF pos = (va + vb) * 0.5f;
                g.DrawEllipse(BluePen, pos.X - 2, pos.Y - 2, 4, 4);
                g.DrawLine(BluePen, pos, pos + spr.NormalVector * 15);
                g.DrawLine(BluePen, va, vb);
            }
            if (_internal_springs != null)
                foreach (InternalSpring spr in _internal_springs)
                {
                    VectorF va = _vertices[spr.IndexA].Position;
                    VectorF vb = _vertices[spr.IndexB].Position;
                    VectorF pos = (va + vb) * 0.5f;
                    g.DrawLine(BluePen, va, vb);
                }
        }
    }
}
