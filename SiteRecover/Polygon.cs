using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SiteRecover
{
    public class Polygon
    {
        public static float DrawScale = 1.0f;
        private Font idfont = new Font("Arial", 10.0f);
        private Vertex[] _vertices;
        public Vertex[] Vertices { get { return _vertices; } }

        public Polygon(int count)
        {
            _vertices = new Vertex[count];
            for (int d = 0; d < count; ++d)
                _vertices[d] = new Vertex(0, 0);
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
                poly[counter] = v.Position * DrawScale;
                g.DrawEllipse(redpen, v.Position.X * DrawScale - 1, v.Position.Y * DrawScale - 1, 2, 2);
                g.DrawString(counter.ToString(), idfont, Brushes.Black, v.Position * DrawScale);
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

            index = int.MaxValue;
            return false;
        }
    }
}
