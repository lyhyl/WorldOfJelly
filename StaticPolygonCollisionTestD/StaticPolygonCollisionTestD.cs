using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System;

namespace StaticPolygonCollisionTestD
{
    public partial class StaticPolygonCollisionTestD : Form
    {
        private List<bool> ptsio = new List<bool>();
        private List<Vector> closestpts = new List<Vector>();
        private Polygon polygona;
        //private Polygon polygonb;
        private Vertex vertex;
        private int selecttar = 0;
        private int selectind = 0;

        public StaticPolygonCollisionTestD()
        {
            InitializeComponent();

            Vertex.CreateOffest = new Vector(0, 0);
            polygona = new Polygon(8, 50);
            vertex = new Vertex(100, 100);

            Vertex.CreateOffest = new Vector(-100, -100);
            //polygonb = new Polygon(5, 50);
        }

        private void StaticPolygonCollisionTestD_MouseDown(object sender, MouseEventArgs e)
        {
            Vector pos = new Vector(e.Location);
            pos.Y -= 250;
            pos.X -= 250;
            pos.Y = -pos.Y;

            if (polygona.Select((Point)pos, out selectind))
            {
                selecttar = 1;
                this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.StaticPolygonCollisionTestD_MouseMove);
            }
            /*else if (polygonb.Select((Point)pos, out selectind))
            {
                selecttar = 2;
                this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.StaticPolygonCollisionTestD_MouseMove);
            }*/
            else if ((vertex.Position - pos).LengthSq < 100)
            {
                selecttar = 3;
                this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.StaticPolygonCollisionTestD_MouseMove);
            }
            else
                selecttar = 0;
        }

        private void StaticPolygonCollisionTestD_MouseMove(object sender, MouseEventArgs e)
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
                /*case 2:
                    polygonb[selectind].Position = pos;
                    break;*/
                case 3:
                    vertex.Position = pos;
                    break;
                case 0:
                    goto default;
                default:
                    break;
            }
            ProcessForce();
            this.Refresh();
        }

        private void StaticPolygonCollisionTestD_MouseUp(object sender, MouseEventArgs e)
        {
            selecttar = 0;
            this.MouseMove -= this.StaticPolygonCollisionTestD_MouseMove;
        }

        private void StaticPolygonCollisionTestD_Paint(object sender, PaintEventArgs e)
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
            //polygonb.Draw(e.Graphics);

            foreach (Vector v in closestpts)
                if (v != null)
                    e.Graphics.DrawEllipse(bluepen, v.X - 5, v.Y - 5, 10, 10);

            e.Graphics.DrawEllipse(redpen, vertex.Position.X - 5, vertex.Position.Y - 5, 10, 10);
        }

        private void ProcessForce()
        {
            ptsio.Clear();
            closestpts.Clear();

            double threshold = 1.0 / Math.Pow((double)ConstantDlim.Value / (double)ConstantC.Value, 0.5);
            ForceLabel.Text = "Force Information:\n" + "Threshold: " + threshold + "\n";

            if (JMath.PointInPolygon(polygona.Vertices, vertex.Position, new Vector(-1000, -1000)))
                vertex.Position = ClosestPoint(vertex.Position, polygona);

            int i = 0;
            Vector cpt;
            bool inVertex;
            while (i < polygona.Vertices.Length - 1)
            {
                FindClosestPointThreshold(vertex.Position, polygona[i].Position, polygona[i + 1].Position, threshold * threshold, out cpt, out inVertex);
                ++i;
                closestpts.Add(cpt);
                ptsio.Add(inVertex);
            }
            FindClosestPointThreshold(vertex.Position, polygona[i].Position, polygona[0].Position, threshold * threshold, out cpt, out inVertex);
            closestpts.Add(cpt);
            ptsio.Add(inVertex);

            /*for (int i = 0; i < polygonb.Vertices.Length; ++i)
            {
                ptsio.Add(JMath.PointInPolygon(polygona.Vertices, polygonb[i].Position, new Vector(-1000, -1000)));
                closestpts.Add(ClosestPoint(polygonb[i].Position, polygona));
            }

            ForceLabel.Text = "Force Information:\n";
            for (int i = 0; i < polygonb.Vertices.Length; ++i)
            {
                if (ptsio[i])
                    polygonb[i].Position = closestpts[i];
                else
                {
                    Vector diff = closestpts[i] - polygonb[i].Position;
                    float force = ((float)ConstantC.Value / diff.LengthSq - (float)ConstantDlim.Value);
                    ForceLabel.Text += "LenSq: " + diff.LengthSq + "\n" +
                        "Len: " + diff.Length + "\n" +
                        "Force: " + force + "\n" +
                        "Limit Force:" + (force < 0 ? 0 : force) + "\n";
                }
            }*/
        }

        private void FindClosestPointThreshold(Vector c, Vector a, Vector b, double threshold, out Vector cpt, out bool inVex)
        {
            double lensq;
            Vector ab = b - a, ac = c - a, bc = b - c;
            double dacab = JMath.Dot(ac, ab);
            if (dacab <= 0)
            {
                lensq = JMath.Dot(ac, ac);
                cpt = lensq < threshold ? new Vector(a) : null;
                inVex = true;
                return;
            }
            double dabab = JMath.Dot(ab, ab);
            if (dacab >= dabab)
            {
                lensq = JMath.Dot(bc, bc);
                cpt = lensq < threshold ? new Vector(b) : null;
                inVex = true;
                return;
            }
            double t = dacab / dabab;
            lensq = JMath.Dot(ac, ac) - dacab * t;
            cpt = lensq < threshold ? a + ab * t : null;
            inVex = false;
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
