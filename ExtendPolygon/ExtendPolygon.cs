using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System;

namespace ExtendPolygon
{
    public partial class ExtendPolygon : Form
    {
        private Polygon polygon;
        private bool selected = false;
        private int selectind = 0;

        public ExtendPolygon()
        {
            InitializeComponent();

            Vertex.CreateOffest = new Vector(0, 0);
            polygon = new Polygon(8, 50);

            Vertex.CreateOffest = new Vector(-100, -100);
        }

        private void ExtendPolygon_MouseDown(object sender, MouseEventArgs e)
        {
            Vector pos = new Vector(e.Location);
            pos.Y -= 250;
            pos.X -= 250;
            pos.Y = -pos.Y;

            if (selected = polygon.Select((Point)pos, out selectind))
                this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ExtendPolygon_MouseMove);
        }

        private void ExtendPolygon_MouseMove(object sender, MouseEventArgs e)
        {
            Vector pos = new Vector(e.Location);
            pos.Y -= 250;
            pos.X -= 250;
            pos.Y = -pos.Y;

            if (selected)
            {
                polygon[selectind].Position = pos;
                this.Refresh();
            }
        }

        private void ExtendPolygon_MouseUp(object sender, MouseEventArgs e)
        {
            selected = false;
            this.MouseMove -= this.ExtendPolygon_MouseMove;
        }

        private void ExtendPolygon_Paint(object sender, PaintEventArgs e)
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

            polygon.Draw(e.Graphics);

            Vector[] diff = new Vector[polygon.Vertices.Length];
            Vector[] nor = new Vector[polygon.Vertices.Length];
            int i = 0;
            while (i < polygon.Vertices.Length - 1)
            {
                diff[i] = polygon[i + 1].Position - polygon[i].Position;
                nor[i] = new Vector(-diff[i].Y, diff[i].X).Normalize();
                ++i;
            }
            diff[i] = polygon[0].Position - polygon[i].Position;
            nor[i] = new Vector(-diff[i].Y, diff[i].X).Normalize();

            Vector[] megnor = new Vector[polygon.Vertices.Length];
            int k = 1;
            while (k < polygon.Vertices.Length)
            {
                megnor[k] = (nor[k - 1] + nor[k]);//.Normalize();
                ++k;
            }
            megnor[0] = (nor[0] + nor[k - 1]);//.Normalize();

            Vector[] extvtx = new Vector[polygon.Vertices.Length];

            const double NormalSize = 40;

            int vc = 0;
            Vector mid;
            while (vc < polygon.Vertices.Length - 1)
            {
                mid = diff[vc] * 0.5 + polygon[vc].Position;
                e.Graphics.DrawLine(bluepen, mid, mid + nor[vc] * NormalSize);
                extvtx[vc] = polygon[vc].Position + megnor[vc] * NormalSize;
                e.Graphics.DrawLine(bluepen, polygon[vc].Position, extvtx[vc]);
                ++vc;
            }
            mid = diff[vc] * 0.5 + polygon[vc].Position;
            e.Graphics.DrawLine(bluepen, mid, mid + nor[vc] * NormalSize);
            extvtx[vc] = polygon[vc].Position + megnor[vc] * NormalSize;
            e.Graphics.DrawLine(bluepen, polygon[vc].Position, extvtx[vc]);

            vc = 0;
            while (vc < polygon.Vertices.Length - 1)
            {
                e.Graphics.DrawLine(bluepen, extvtx[vc], extvtx[vc + 1]);
                ++vc;
            }
            e.Graphics.DrawLine(bluepen, extvtx[vc], extvtx[0]);
        }
    }
}