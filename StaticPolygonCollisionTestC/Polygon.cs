using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace StaticPolygonCollisionTestC
{
    public class Polygon
    {
        static public float GeomScale = 1f;
        private Font idfont = new Font("Arial", 10.0f);
        private Vertex[] _vertices;
        public Vertex[] Vertices
        {
            get
            {
                Vector tmpco = Vertex.CreateOffest;
                Vertex.CreateOffest = new Vector(0, 0);
                Vertex[] _vs = new Vertex[_vertices.Length];
                for (int c = 0; c < _vs.Length; ++c)
                    _vs[c] = new Vertex(_vertices[c].Position * GeomScale);
                Vertex.CreateOffest = tmpco;
                return _vs;
            }
        }

        public Polygon(uint count, float rad)
        {
            _vertices = new Vertex[count];
            float deg = 2 * (float)Math.PI / (float)count;
            for (int d = 0; d < count; ++d)
                _vertices[d] = new Vertex((float)Math.Sin(d * deg) * rad, (float)Math.Cos(d * deg) * rad);
        }

        public Vertex this[int index] 
        {
            set { if (index != int.MaxValue)_vertices[index] = value; }
            get { return index != int.MaxValue ? _vertices[index] : null; }
        }

        public void Draw(Graphics g)
        {
            Pen redpen = new Pen(Color.Red);
            Pen bluepen = new Pen(Color.Blue);
            PointF[] poly = new PointF[_vertices.Length];
            int counter = 0;
            foreach (Vertex v in _vertices)
            {
                poly[counter] = v.Position;
                g.DrawEllipse(redpen, v.Position.X - 5, v.Position.Y - 5, 10, 10);
                g.DrawString(counter.ToString(), idfont, Brushes.Black, v.Position);
                ++counter;
            }
            g.DrawPolygon(bluepen, poly);
        }

        public bool Select(Point pos, out int index)
        {
            Vector poss = new Vector(pos);
            int counter = 0;
            foreach (Vertex v in _vertices)
            {
                Vector diff = v.Position - poss;
                if (diff.X * diff.X + diff.Y * diff.Y < 10 * 10)
                {
                    index = counter;
                    return true;
                }
                ++counter;
            }

            index = -1;
            return false;
        }
    }
}
