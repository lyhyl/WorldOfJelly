using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace StaticPolygonCollisionTestB
{
    public partial class StaticPolygonCollisionTestB : Form
    {
        private int select_index = -1;
        private int select_target = -1;

        private Polygon polya;
        private Polygon polyb;

        private List<Vector> inter_list = new List<Vector>();
        private List<Vector> opp_inter_list = new List<Vector>();

        private List<bool> io_list;
        private List<bool> opp_io_list;
        private List<int> discon_list = new List<int>();
        private List<int> opp_discon_list = new List<int>();

        public StaticPolygonCollisionTestB()
        {
            InitializeComponent();

            Vertex.CreateOffest = new Vector(50, 50);
            polya = new Polygon(8, 50);
            Vertex.CreateOffest = new Vector(-50, -50);
            polyb = new Polygon(4, 25);

            io_list = new List<bool>(polya.Vertices.Length);
            opp_io_list = new List<bool>(polyb.Vertices.Length);
        }

        private void StaticPolygonCollisionTestB_Paint(object sender, PaintEventArgs e)
        {
            Matrix trans = new Matrix();
            trans.Scale(1, -1);
            trans.Translate(250, -250);
            e.Graphics.Transform = trans;

            Pen graypen = new Pen(Color.DarkGray);

            Pen greenpen = new Pen(Color.Green);
            Pen orangepen = new Pen(Color.Orange);

            Pen greenlpen = new Pen(Color.Green, 2f);
            Pen orangelpen = new Pen(Color.Orange, 2f);

            e.Graphics.DrawLine(graypen, -250, 0, 250, 0);
            e.Graphics.DrawLine(graypen, 0, -250, 0, 250);

            polya.Draw(e.Graphics);
            polyb.Draw(e.Graphics);

            foreach (Vector i in inter_list)
                e.Graphics.DrawEllipse(greenpen, i.X - 2.5f, i.Y - 2.5f, 5, 5);

            for (int i = 0; i < discon_list.Count; i += 2)
                if (discon_list[i] > discon_list[i + 1])
                    for (int j = discon_list[i]; j < discon_list[i + 1] + polya.Vertices.Length; ++j)
                        e.Graphics.DrawEllipse(greenpen, polya[j % polya.Vertices.Length].Position.X - 3, polya[j % polya.Vertices.Length].Position.Y - 3, 6, 6);
                else
                    for (int j = discon_list[i]; j < discon_list[i + 1]; ++j)
                        e.Graphics.DrawEllipse(greenpen, polya[j].Position.X - 3, polya[j].Position.Y - 3, 6, 6);

            foreach (Vector i in opp_inter_list)
                e.Graphics.DrawEllipse(orangepen, i.X - 2.5f, i.Y - 2.5f, 5, 5);

            for (int i = 0; i < opp_discon_list.Count; i += 2)
                if (opp_discon_list[i] > opp_discon_list[i + 1])
                    for (int j = opp_discon_list[i]; j < opp_discon_list[i + 1] + polyb.Vertices.Length; ++j)
                        e.Graphics.DrawEllipse(orangepen, polya[j % polyb.Vertices.Length].Position.X - 3, polya[j % polyb.Vertices.Length].Position.Y - 3, 6, 6);
                else
                    for (int j = opp_discon_list[i]; j < opp_discon_list[i + 1]; ++j)
                        e.Graphics.DrawEllipse(orangepen, polyb[j].Position.X - 3, polyb[j].Position.Y - 3, 6, 6);
        }

        private void StaticPolygonCollisionTestB_MouseDown(object sender, MouseEventArgs e)
        {
            Point pos = e.Location;
            pos.Y -= 250;
            pos.X -= 250;
            pos.Y = -pos.Y;

            select_target = -1;
            if (polya.Select(pos, out select_index))
            {
                this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.StaticPolygonCollisionTestB_MouseMove);
                select_target = 0;
            }
            else if (polyb.Select(pos, out select_index))
            {
                this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.StaticPolygonCollisionTestB_MouseMove);
                select_target = 1;
            }
        }

        private void StaticPolygonCollisionTestB_MouseMove(object sender, MouseEventArgs e)
        {
            Vector pos = new Vector(e.Location);
            pos.Y -= 250;
            pos.X -= 250;
            pos.Y = -pos.Y;

            switch (select_target)
            {
                case 0:
                    polya[select_index].Position = pos;
                    ProcessCollisionB(polya, polyb);
                    break;
                case 1:
                    polyb[select_index].Position = pos;
                    ProcessCollisionB(polya, polyb);
                    break;
                case -1:
                    goto default;
                default:
                    break;
            }

            this.Refresh();
        }

        private void ProcessCollisionB(Polygon polya, Polygon polyb)
        {
            io_list.Clear();
            opp_io_list.Clear();

            GetInOutState(polya, polyb, ref io_list);
            GetInOutState(polyb, polya, ref opp_io_list);

            discon_list.Clear();
            opp_discon_list.Clear();

            FindDiscontinuity(ref io_list, ref discon_list);
            FindDiscontinuity(ref opp_io_list, ref opp_discon_list);

            //!!
            if ((discon_list.Count & 1) != 0 || (opp_discon_list.Count & 1) != 0)
                throw new Exception("assert failed.");

            inter_list.Clear();
            opp_inter_list.Clear();

            GetIntersetions(polya, polyb, ref discon_list, ref inter_list);
            GetIntersetions(polyb, polya, ref opp_discon_list, ref opp_inter_list);

            //!!
            if ((inter_list.Count & 1) != 0 || (opp_inter_list.Count & 1) != 0)
                throw new Exception("assert failed.");
        }

        private void GetIntersetions(Polygon pa, Polygon pb, ref List<int> discons, ref List<Vector> inters)
        {
            Vector inter;
            foreach (int id in discons)
                if (JMath.SegmentPolygonTest(
                    pa[(id + pa.Vertices.Length - 1) % pa.Vertices.Length].Position,
                    pa[id].Position,
                    pb.Vertices,
                    out inter))
                    inters.Add(inter);
                else
                    throw new Exception("assert failed.");
        }

        private void FindDiscontinuity(ref List<bool> io_states, ref List<int> discons)
        {
            int c = 0;
            if (io_states[0])
            {
                if (io_states[io_states.Count - 1])//cycle
                {
                    //notice here ,
                    //if polygonA is fully inside the polygonB ,
                    //it will get ArgumentOutOfRangeException ,
                    //because all the vertices' io state are true

                    while (io_states[c]) ++c;

                    //foreach (bool s in io_states)
                    //    if (s) ++c; else break;

                    int tmp = c;

                    while (c < io_states.Count - 1)
                        if (io_states[c] != io_states[++c])
                            discons.Add(c);

                    discons.Add(tmp);
                }
                else
                { 
                    discons.Add(0);

                    while (c < io_states.Count - 1)
                        if (io_states[c] != io_states[++c])
                            discons.Add(c);
                }
            }
            else
            {
                while (c < io_states.Count - 1)
                    if (io_states[c] != io_states[++c])
                        discons.Add(c);

                if (io_states[c])
                    discons.Add(0);
            }
        }

        private void GetInOutState(Polygon pa, Polygon pb, ref List<bool> io_states)
        {
            foreach (Vertex v in pa.Vertices)
                io_states.Add(JMath.PointInPolygon(pb.Vertices, v.Position, new Vector(0, -500)));
        }

        private void ProcessCollisionA(Polygon polya, Polygon polyb)
        {
            inter_list.Clear();
            opp_inter_list.Clear();
            FindIntersections(polya, polyb, ref inter_list);
            FindIntersections(polyb, polya, ref opp_inter_list);
        }

        private void FindIntersections(Polygon pa, Polygon pb, ref List<Vector> inters)
        {
            int c = 0;
            Vector inter;
            while (c < pa.Vertices.Length - 1)
                if (JMath.SegmentPolygonTest(pa.Vertices[c].Position, pa.Vertices[++c].Position, pb.Vertices, out inter))
                    inters.Add(inter);
            if (JMath.SegmentPolygonTest(pa.Vertices[c].Position, pa.Vertices[0].Position, pb.Vertices, out inter))
                inters.Add(inter);
        }

        private void StaticPolygonCollisionTestB_MouseUp(object sender, MouseEventArgs e)
        {
            this.MouseMove -= this.StaticPolygonCollisionTestB_MouseMove;
        }

        private void restrict_btn_Click(object sender, EventArgs e)
        {
            RestrictPolygon(polya, ref inter_list, ref discon_list);
            RestrictPolygon(polyb, ref opp_inter_list, ref opp_discon_list);
            this.Refresh();
        }

        private void RestrictPolygon(Polygon poly,ref List<Vector> inters,ref List<int> discons)
        {
            for (int c = 0; c < inters.Count; c += 2)
            {
                Vector segb = inters[c];
                Vector sege = inters[c + 1];

                int indexa = discons[c];
                int indexb = discons[c + 1];

                if (indexa < indexb)
                    for (int vi = indexa; vi < indexb; ++vi)
                        poly[vi].Position = JMath.ClosestPointOnSegment(segb, sege, poly[vi].Position);
                else
                    for (int vi = indexb; vi < indexa + poly.Vertices.Length; ++vi)
                        poly[vi % poly.Vertices.Length].Position = JMath.ClosestPointOnSegment(segb, sege, poly[vi % poly.Vertices.Length].Position);
            }
        }
    }
}
