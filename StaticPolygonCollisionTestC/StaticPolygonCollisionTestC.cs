using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace StaticPolygonCollisionTestC
{
    public partial class StaticPolygonCollisionTestC : Form
    {
        private List<Vector> closestpts = new List<Vector>();
        private Polygon polygona;
        private Polygon polygonb;
        private int selecttar = 0;
        private int selectind = 0;

        public StaticPolygonCollisionTestC()
        {
            InitializeComponent();

            Vertex.CreateOffest = new Vector(0, 0);
            polygona = new Polygon(8, 50);

            Vertex.CreateOffest = new Vector(100, 100);
            polygonb = new Polygon(4, 50);
        }

        private void StaticPolygonCollisionTestC_MouseDown(object sender, MouseEventArgs e)
        {
            Vector pos = new Vector(e.Location);
            pos.Y -= 250;
            pos.X -= 250;
            pos.Y = -pos.Y;

            if (polygona.Select((Point)pos, out selectind))
            {
                selecttar = 1;
                this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.StaticPolygonCollisionTestC_MouseMove);
            }
            else if (polygonb.Select((Point)pos, out selectind))
            {
                selecttar = 2;
                this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.StaticPolygonCollisionTestC_MouseMove);
            }
            else
                selecttar = 0;
        }

        private void StaticPolygonCollisionTestC_MouseMove(object sender, MouseEventArgs e)
        {
            Vector pos = new Vector(e.Location);
            pos.Y -= 250;
            pos.X -= 250;
            pos.Y = -pos.Y;

            switch (selecttar)
            {
                case 1:
                    polygona[selectind].Position = pos;
                    break;
                case 2:
                    polygonb[selectind].Position = pos;
                    break;
                case 0:
                    goto default;
                default:
                    break;
            }
            ProcessCollision();
            this.Refresh();
        }

        private void StaticPolygonCollisionTestC_MouseUp(object sender, MouseEventArgs e)
        {
            selecttar = 0;
            this.MouseMove -= this.StaticPolygonCollisionTestC_MouseMove;
        }

        private void StaticPolygonCollisionTestC_Paint(object sender, PaintEventArgs e)
        {
            Matrix trans = new Matrix();
            trans.Scale(1, -1);
            trans.Translate(250, -250);
            e.Graphics.Transform = trans;

            Pen redpen = new Pen(Color.Red);
            Pen bluepen = new Pen(Color.Blue);
            Pen graypen = new Pen(Color.DarkGray);

            e.Graphics.DrawLine(graypen, -250, 0, 250, 0);
            e.Graphics.DrawLine(graypen, 0, -250, 0, 250);

            polygona.Draw(e.Graphics);
            polygonb.Draw(e.Graphics);

            foreach (Vector cpt in closestpts)
                e.Graphics.DrawEllipse(bluepen, cpt.X - 5, cpt.Y - 5, 10, 10);
        }

        private void ProcessCollision()
        {
            closestpts.Clear();
            FindClosestPtsPoly(polygona, polygonb);
            FindClosestPtsPoly(polygonb, polygona);
        }

        private void FindClosestPtsPoly(Polygon polya, Polygon polyb)
        {
            foreach (Vertex v in polyb.Vertices)
                if (JMath.PointInPolygon(polya.Vertices, v.Position, new Vector(-500, -500)))
                    closestpts.Add(ClosestPoint(v.Position, polya));
        }

        private Vector ClosestPoint(Vector v, Polygon poly)
        {
            double minlen = double.MaxValue;
            int i = 0;
            Vector cpt = null, tcpt;
            double len;
            while (i < poly.Vertices.Length - 1)
            {
                tcpt = JMath.ClosestPointOnSegment(poly[i].Position, poly[i + 1].Position, v);
                ++i;
                len = JMath.LengthOf(v - tcpt);
                if (len < minlen)
                {
                    minlen = len;
                    cpt = tcpt;
                }
            }
            tcpt = JMath.ClosestPointOnSegment(poly[i].Position, poly[0].Position, v);
            len = JMath.LengthOf(v - tcpt);
            if (len < minlen)
            {
                minlen = len;
                cpt = tcpt;
            }
            return cpt;
        }
    }
}
