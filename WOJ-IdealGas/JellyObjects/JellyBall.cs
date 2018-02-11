using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using WOJ_IdealGas.Spring;

namespace WOJ_IdealGas.JellyObjects
{
    public class JellyBall : JellyObject
    {
        /// <summary>
        /// Only available in construction
        /// </summary>
        private float Radius { set; get; }
        public JellyBall(float rad, int vc, float mass)
            : base()
        {
            _vertexcount = vc;
            Radius = rad;
            Mass = mass;

            float PRad = (float)Math.PI * 2.0f / (float)vc;
            int i;

            _vertices = new JellyVertex[vc];
            for (i = 0; i < vc; ++i)
            {
                float deg = (float)i * PRad;
                _vertices[i] = new JellyVertex((float)Math.Sin(deg) * Radius + 150, (float)Math.Cos(deg) * Radius);
            }

            _edge_springs = new EdgeSpring[vc];
            for (i = 0; i < vc - 1; ++i)
                AddEdgeSpring(i, i, i + 1);
            AddEdgeSpring(i, i, 0);

            Pressure = 160000000f;
            KS = 20000f;
            KD = 500f;
        }

        public override void DebugDraw(Graphics g)
        {
            base.DebugDraw(g);
            VectorF cen = new VectorF(0, 0);
            VectorF top = _vertices[0].Position;
            foreach (JellyVertex jv in _vertices)
            {
                cen += jv.Position;
                if (jv.Position.Y > top.Y)
                    top = jv.Position;
            }
            cen /= _vertices.Length;

            VectorF eyecen = (cen + top) * 0.5f;

            g.DrawEllipse(new Pen(Color.Green), eyecen.X - 5, eyecen.Y - 5, 10, 10);
        }
    }
}
