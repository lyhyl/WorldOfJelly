using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace StaticPolygonCollisionTest
{
    public partial class StaticPolygonCollisionTest : Form
    {
        private enum Slt { A, B, N };
        private Slt sltObj = Slt.N;
        private int index = int.MaxValue;
        private Polygon polyA, polyB;

        private List<int> CollPosID = new List<int>();
        private List<int> OppCollPosID = new List<int>();

        private List<Pair<int>> InsIDPairs = new List<Pair<int>>();
        private List<Pair<int>> OppInsIDPairs = new List<Pair<int>>();

        private List<Vector> RetPts = new List<Vector>();
        private List<Vector> OppRetPts = new List<Vector>();

        private List<Pair<Vector>> InsPtPairs = new List<Pair<Vector>>();
        private List<Pair<Vector>> OppInsPtPairs = new List<Pair<Vector>>();

        private int maxtime = 0;

        public StaticPolygonCollisionTest()
        {
            InitializeComponent();
            Vertex.CreateOffest = new Vector(50, 50);
            polyA = new Polygon(4, 60);
            Vertex.CreateOffest = new Vector(-75, -100);
            polyB = new Polygon(8, 75);
        }
        
        private void ProcessCollision(Polygon polya, Polygon polyb)
        {
            FindInVertex(polya, polyb, ref CollPosID);

            if (CollPosID.Count >= 1)
                GenerateRangePairs(polya, ref InsIDPairs, ref CollPosID);

            FindOppsiteInVertexPairs(polya, polyb);
        }

        private void ProcessCollisionB(Polygon polya, Polygon polyb)
        {
            CollPosID.Clear();
            OppCollPosID.Clear();

            int time = Environment.TickCount;

            InsIDPairs.Clear();
            OppInsIDPairs.Clear();

            InsPtPairs.Clear();

            bool startin = JMath.PointInOutTest(polyb.Vertices, polya.Vertices[0].Position) == JMath.InOutTestResult.In;
            int beginiter = 0;

            bool usefirst = true;
            Vector insa = null, insb = null;
            int selfid = int.MaxValue, oppida = int.MaxValue, oppidb = int.MaxValue;

            Vector insbuf = null;
            int idbuf = int.MaxValue, oppidbuf = int.MaxValue;

            if (startin)
            {
                for (; beginiter < polya.Vertices.Length - 1; ++beginiter)
                {
                    if (JMath.SegmentPolygonTest(polya.Vertices[beginiter].Position, polya.Vertices[beginiter + 1].Position, polyb.Vertices, out oppidbuf, out insbuf))
                    {
                        idbuf = beginiter;
                        break;
                    }
                }
                ++beginiter;
            }

            int acounter;
            for (acounter = beginiter; acounter < polya.Vertices.Length - 1; ++acounter)
            {
                FindInPairs(polya, polyb, ref usefirst, acounter, acounter + 1, ref selfid, ref oppida, ref insa, ref oppidb, ref insb);
            }
            FindInPairs(polya, polyb, ref usefirst, acounter, 0, ref selfid, ref oppida, ref insa, ref oppidb, ref insb);

            if (startin)
            {
                insb = insbuf;
                oppidb = oppidbuf;

                if (JMath.PointInOutTest(polya.Vertices, polyb.Vertices[oppida].Position) != JMath.InOutTestResult.In)
                    oppida = (oppida + 1) % polyb.Vertices.Length;
                if (JMath.PointInOutTest(polya.Vertices, polyb.Vertices[oppidb].Position) != JMath.InOutTestResult.In)
                    oppidb = (oppidb + 1) % polyb.Vertices.Length;

                if (oppida == oppidb)
                    if (JMath.PointInOutTest(polya.Vertices, polyb.Vertices[oppida].Position) == JMath.InOutTestResult.Out)
                    {
                        OppInsIDPairs.Add(Pair.CreatePair(int.MaxValue, oppida));
                        goto skip;
                    }

                if (JMath.PointInOutTest(polya.Vertices, polyb.Vertices[(oppida + oppidb) >> 1].Position) == JMath.InOutTestResult.In)//min to max
                {
                    if (oppida > oppidb)
                        JMath.Swap(ref oppida, ref oppidb);
                }
                else//max to min
                {
                    if (oppida < oppidb)
                        JMath.Swap(ref oppida, ref oppidb);
                }

                OppInsIDPairs.Add(Pair.CreatePair(oppida, oppidb));
            skip:
                usefirst = true;
                InsIDPairs.Add(Pair.CreatePair((selfid + 1) % polya.Vertices.Length, idbuf));
                InsPtPairs.Add(Pair.CreatePair(insa, insb));
            }

            int ctime = Environment.TickCount;
            int delta = ctime - time;
            UseTimeLabel.Text = delta.ToString() + " ms";
            time = ctime;
            if (delta > maxtime)
            {
                maxtime = delta;
                MaxTimeLabel.Text = UseTimeLabel.Text;
            }
        }
        
        private void ProcessCollisionC(Polygon polya, Polygon polyb, ref List<Pair<int>> idpairs, ref List<Pair<Vector>> ptpairs)
        {
            int time = Environment.TickCount;

            idpairs.Clear();
            ptpairs.Clear();

            bool startin = JMath.PointInOutTest(polyb.Vertices, polya.Vertices[0].Position) == JMath.InOutTestResult.In;
            int beginiter = 0;

            bool usefirst = true;
            Vector insa = null, insb = null;
            int id = int.MaxValue;

            Vector insbuf = null;
            int idbuf = int.MaxValue;

            if (startin)
            {
                for (; beginiter < polya.Vertices.Length - 1; ++beginiter)
                {
                    if (JMath.SegmentPolygonTest(polya.Vertices[beginiter].Position, polya.Vertices[beginiter + 1].Position, polyb.Vertices, out insbuf))
                    {
                        idbuf = beginiter;
                        break;
                    }
                }
                ++beginiter;
            }

            int counter;
            for (counter = beginiter; counter < polya.Vertices.Length - 1; ++counter)
                FindInPairs(polya, polyb, ref usefirst, counter, counter + 1, ref id, ref insa, ref insb, ref idpairs, ref ptpairs);
            FindInPairs(polya, polyb, ref usefirst, counter, 0, ref id, ref insa, ref insb, ref idpairs, ref ptpairs);

            if (startin)
            {
                insb = insbuf;
                idpairs.Add(Pair.CreatePair((id + 1) % polya.Vertices.Length, idbuf));
                ptpairs.Add(Pair.CreatePair(insa, insb));
            }

            int ctime = Environment.TickCount;
            int delta = ctime - time;
            UseTimeLabel.Text = delta.ToString() + " ms";
            time = ctime;
            if (delta > maxtime)
            {
                maxtime = delta;
                MaxTimeLabel.Text = UseTimeLabel.Text;
            }
        }

        private void FindInPairs(Polygon polya, Polygon polyb, ref bool usefirst, int indexa, int indexb, ref int selfid, ref Vector insa, ref Vector insb, ref List<Pair<int>> idpairs, ref List<Pair<Vector>> ptpairs)
        {
            if (usefirst)
            {
                if (JMath.SegmentPolygonTest(polya.Vertices[indexa].Position, polya.Vertices[indexb].Position, polyb.Vertices, out insa))
                {
                    usefirst = false;
                    selfid = indexa;
                }
            }
            else
            {
                if (JMath.SegmentPolygonTest(polya.Vertices[indexa].Position, polya.Vertices[indexb].Position, polyb.Vertices, out insb))
                {
                    usefirst = true;
                    idpairs.Add(Pair.CreatePair(selfid + 1, indexa));
                    ptpairs.Add(Pair.CreatePair(insa, insb));
                }
            }
        }
        
        private void FindInPairs(Polygon polya, Polygon polyb, ref bool usefirst, int indexa, int indexb, ref int selfid, ref int oppida, ref Vector insa, ref int oppidb, ref Vector insb)
        {
            if (usefirst)
            {
                if (JMath.SegmentPolygonTest(polya.Vertices[indexa].Position, polya.Vertices[indexb].Position, polyb.Vertices, out oppida, out insa))
                {
                    usefirst = false;
                    selfid = indexa;
                }
            }
            else
            {
                if (JMath.SegmentPolygonTest(polya.Vertices[indexa].Position, polya.Vertices[indexb].Position, polyb.Vertices, out oppidb, out insb))
                {
                    if (JMath.PointInOutTest(polya.Vertices, polyb.Vertices[oppida].Position) != JMath.InOutTestResult.In)
                        oppida = (oppida + 1) % polyb.Vertices.Length;
                    if (JMath.PointInOutTest(polya.Vertices, polyb.Vertices[oppidb].Position) != JMath.InOutTestResult.In)
                        oppidb = (oppidb + 1) % polyb.Vertices.Length;

                    if (oppida == oppidb)
                        if (JMath.PointInOutTest(polya.Vertices, polyb.Vertices[oppida].Position) == JMath.InOutTestResult.Out)
                        {
                            OppInsIDPairs.Add(Pair.CreatePair(int.MaxValue, oppida));
                            goto skip;
                        }

                    if (JMath.PointInOutTest(polya.Vertices, polyb.Vertices[(oppida + oppidb) >> 1].Position) == JMath.InOutTestResult.In)//min to max
                    {
                        if (oppida > oppidb)
                            JMath.Swap(ref oppida, ref oppidb);
                    }
                    else//max to min
                    {
                        if (oppida < oppidb)
                            JMath.Swap(ref oppida, ref oppidb);
                    }

                    OppInsIDPairs.Add(Pair.CreatePair(oppida, oppidb));
                skip:
                    usefirst = true;
                    InsIDPairs.Add(Pair.CreatePair(selfid + 1, indexa));
                    InsPtPairs.Add(Pair.CreatePair(insa, insb));
                }
            }
        }

        private void FindOppsiteInVertexPairs(Polygon polya, Polygon polyb)
        {
            /*
             * ins point
             * */
            OppInsIDPairs.Clear();
            InsPtPairs.Clear();
            foreach (Pair<int> paid in InsIDPairs)
            {
                Vector insa, insb;
                int ida, idb;
                if (JMath.SegmentPolygonTest
                    (polya[paid.A].Position,
                    polya[(paid.A + polya.Vertices.Length - 1) % polya.Vertices.Length].Position,
                    polyb.Vertices, out ida, out insa))
                {
                    if (JMath.SegmentPolygonTest
                        (polya[paid.B].Position,
                        polya[(paid.B + 1) % polya.Vertices.Length].Position,
                        polyb.Vertices, out idb, out insb))
                    {
                        if (JMath.PointInOutTest(polya.Vertices, polyb.Vertices[ida].Position) != JMath.InOutTestResult.In)
                            ida = (ida + 1) % polyb.Vertices.Length;
                        if (JMath.PointInOutTest(polya.Vertices, polyb.Vertices[idb].Position) != JMath.InOutTestResult.In)
                            idb = (idb + 1) % polyb.Vertices.Length;

                        InsPtPairs.Add(Pair.CreatePair(insa, insb));

                        if (ida == idb)
                            if (JMath.PointInOutTest(polya.Vertices, polyb.Vertices[ida].Position) == JMath.InOutTestResult.Out)
                            {
                                OppInsIDPairs.Add(Pair.CreatePair(int.MaxValue, ida));
                                continue;
                            }

                        if (JMath.PointInOutTest(polya.Vertices, polyb.Vertices[(ida + idb) >> 1].Position) == JMath.InOutTestResult.In)//min to max
                        {
                            if (ida > idb)
                                JMath.Swap(ref ida, ref idb);
                        }
                        else//max to min
                        {
                            if (ida < idb)
                                JMath.Swap(ref ida, ref idb);
                        }

                        OppInsIDPairs.Add(Pair.CreatePair(ida, idb));
                    }
                }
            }
        }

        private void GenerateRangePairs(Polygon polya,ref List<Pair<int>> IDRangePairs,ref List<int> CollisionIDs)
        {
            /*
             * find pairs
             * */
            IDRangePairs.Clear();
            int lastid = CollisionIDs[0];
            for (int pid = 0; pid < CollisionIDs.Count - 1; ++pid)
            {
                if (CollisionIDs[pid + 1] - CollisionIDs[pid] != 1)
                {
                    IDRangePairs.Add(Pair.CreatePair(lastid, CollisionIDs[pid]));
                    lastid = CollisionIDs[pid + 1];
                }
            }
            int headdiff = CollisionIDs[CollisionIDs.Count - 1] - CollisionIDs[0];
            if (headdiff == 1)
            {
                IDRangePairs.Add(Pair.CreatePair(lastid, CollisionIDs[CollisionIDs.Count - 1]));
            }
            else if (headdiff == polya.Vertices.Length - 1)
            {
                Pair<int> paa = IDRangePairs[0];
                IDRangePairs.RemoveAt(0);
                IDRangePairs.Add(Pair.CreatePair(lastid, paa.B));
            }
            else
            {
                IDRangePairs.Add(Pair.CreatePair(lastid, CollisionIDs[CollisionIDs.Count - 1]));
            }
        }

        private void FindInVertex(Polygon polya, Polygon polyb, ref List<int> CollisionIDs)
        {
            /*
             * find In-vertex
             * */
            CollisionIDs.Clear();
            int counter = 0;
            foreach (Vertex v in polya.Vertices)
            {
                if (JMath.PointInOutTest(polyb.Vertices, v.Position) == JMath.InOutTestResult.In)
                    CollisionIDs.Add(counter);
                ++counter;
            }
        }
        
        private void StaticPolygonCollisionTest_Paint(object sender, PaintEventArgs e)
        {
            Matrix trans = new Matrix();
            trans.Scale(1, -1);
            trans.Translate(250, -250);
            e.Graphics.Transform = trans;

            Pen graypen = new Pen(Color.DarkGray);
            Pen greenpen = new Pen(Color.Green, 2f);
            Pen orangepen = new Pen(Color.Orange, 2f);

            e.Graphics.DrawLine(graypen, -250, 0, 250, 0);
            e.Graphics.DrawLine(graypen, 0, -250, 0, 250);

            foreach (int pid in CollPosID)
                e.Graphics.DrawEllipse(graypen, polyA.Vertices[pid].Position.X - 2.5f, polyA.Vertices[pid].Position.Y - 2.5f, 5, 5);
            foreach (int pid in OppCollPosID)
                e.Graphics.DrawEllipse(graypen, polyB.Vertices[pid].Position.X - 2.5f, polyB.Vertices[pid].Position.Y - 2.5f, 5, 5);

            foreach (PointF pt in RetPts)
                e.Graphics.DrawEllipse(graypen, pt.X - 2.5f, pt.Y - 2.5f, 5, 5);
            foreach (PointF pt in OppRetPts)
                e.Graphics.DrawEllipse(graypen, pt.X - 2.5f, pt.Y - 2.5f, 5, 5);

            polyA.Draw(e.Graphics);
            polyB.Draw(e.Graphics);

            AABB aa = new AABB(polyA.Vertices);
            AABB ab = new AABB(polyB.Vertices);

            e.Graphics.DrawRectangle(graypen, aa.Left, aa.Bottom, aa.Right - aa.Left, aa.Top - aa.Bottom);
            e.Graphics.DrawRectangle(graypen, ab.Left, ab.Bottom, ab.Right - ab.Left, ab.Top - ab.Bottom);

            foreach (Pair<int> pa in InsIDPairs)
                e.Graphics.DrawLine(orangepen, polyA.Vertices[pa.A].Position, polyA[pa.B].Position);
            foreach (Pair<int> pa in OppInsIDPairs)
                //if (pa.A != int.MaxValue)
                    e.Graphics.DrawLine(orangepen, polyB.Vertices[pa.A].Position, polyB[pa.B].Position);

            foreach (Pair<Vector> pa in InsPtPairs)
                e.Graphics.DrawLine(greenpen, pa.A, pa.B);
            foreach (Pair<Vector> pa in OppInsPtPairs)
                e.Graphics.DrawLine(greenpen, pa.A, pa.B);
        }

        private void StaticPolygonCollisionTest_MouseDown(object sender, MouseEventArgs e)
        {
            Point pos = e.Location;
            pos.Y -= 250;
            pos.X -= 250;
            pos.Y = -pos.Y;

            if (polyA.Select(pos, out index))
            {
                this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.StaticPolygonCollisionTest_MouseMove);
                sltObj = Slt.A;
            }
            else if (polyB.Select(pos, out index))
            {
                this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.StaticPolygonCollisionTest_MouseMove);
                sltObj = Slt.B;
            }
            else
                sltObj = Slt.N;
        }

        private void StaticPolygonCollisionTest_MouseUp(object sender, MouseEventArgs e)
        {
            if (sltObj != Slt.N)
            {
                this.MouseMove -= this.StaticPolygonCollisionTest_MouseMove;
                this.MouseMove -= this.StaticPolygonCollisionTest_MouseMove;
            }
        }

        private void StaticPolygonCollisionTest_MouseMove(object sender, MouseEventArgs e)
        {
            Vector pos = new Vector(e.Location);
            pos.Y -= 250;
            pos.X -= 250;
            pos.Y = - pos.Y;

            MousePositionLabel.Text = pos.ToString();

            switch (sltObj)
            {
                case Slt.A:
                    polyA[index].Position = pos;
                    ProcessCollisionC(polyA, polyB, ref InsIDPairs, ref InsPtPairs);
                    ProcessCollisionC(polyB, polyA, ref OppInsIDPairs, ref OppInsPtPairs);
                    this.Refresh();
                    break;
                case Slt.B:
                    polyB[index].Position = pos;
                    ProcessCollisionC(polyA, polyB, ref InsIDPairs, ref InsPtPairs);
                    ProcessCollisionC(polyB, polyA, ref OppInsIDPairs, ref OppInsPtPairs);
                    this.Refresh();
                    break;
                case Slt.N:
                    goto default;
                default:
                    break;
            }
        }

        private void restrict_btn_Click(object sender, EventArgs e)
        {
            RetPts.Clear();
            OppRetPts.Clear();
            /*for (int c = 0; c < InsIDPairs.Count; ++c)
            {
                Vector segs = InsPtPairs[c].A;
                Vector sege = InsPtPairs[c].B;
                int itermax = InsIDPairs[c].B < InsIDPairs[c].A ? InsIDPairs[c].B + polyA.Vertices.Length : InsIDPairs[c].B;
                for (int iter = InsIDPairs[c].A; iter <= itermax; ++iter)
                {
                    Vector pt = polyA[iter % polyA.Vertices.Length].Position;
                    RetPts.Add(JMath.ClosestPointOnLine(segs, sege, pt));
                }
                if (OppInsIDPairs[c].A == int.MaxValue)
                    OppRetPts.Add(polyB[OppInsIDPairs[c].B % polyB.Vertices.Length].Position);
                else
                {
                    int itermaxb = OppInsIDPairs[c].B < OppInsIDPairs[c].A ? OppInsIDPairs[c].B + polyB.Vertices.Length : OppInsIDPairs[c].B;
                    for (int iter = OppInsIDPairs[c].A; iter <= itermaxb; ++iter)
                    {
                        Vector pt = polyB[iter % polyB.Vertices.Length].Position;
                        OppRetPts.Add(JMath.ClosestPointOnLine(segs, sege, pt));
                    }
                }
            }*/
            for (int c = 0; c < InsIDPairs.Count; ++c)
            {
                Vector segs = InsPtPairs[c].A;
                Vector sege = InsPtPairs[c].B;
                int itermax = InsIDPairs[c].B < InsIDPairs[c].A ? InsIDPairs[c].B + polyA.Vertices.Length : InsIDPairs[c].B;
                for (int iter = InsIDPairs[c].A; iter <= itermax; ++iter)
                {
                    Vector pt = polyA[iter % polyA.Vertices.Length].Position;
                    RetPts.Add(JMath.ClosestPointOnSegment(segs, sege, pt));
                }
            }
            for (int c = 0; c < OppInsIDPairs.Count; ++c)
            {
                Vector segs = OppInsPtPairs[c].A;
                Vector sege = OppInsPtPairs[c].B;
                int itermax = OppInsIDPairs[c].B < OppInsIDPairs[c].A ? OppInsIDPairs[c].B + polyB.Vertices.Length : OppInsIDPairs[c].B;
                for (int iter = OppInsIDPairs[c].A; iter <= itermax; ++iter)
                {
                    Vector pt = polyB[iter % polyB.Vertices.Length].Position;
                    OppRetPts.Add(JMath.ClosestPointOnSegment(segs, sege, pt));
                }
            }
            this.Refresh();
        }

        private void apply_btn_Click(object sender, EventArgs e)
        {
            for (int c = 0; c < InsIDPairs.Count; ++c)
            {
                int ass = 0;
                for (int i = InsIDPairs[c].A; i <= InsIDPairs[c].B; ++i, ++ass)
                    polyA[i].Position = RetPts[ass];
            }
            for (int c = 0; c < OppInsIDPairs.Count; ++c)
            {
                int ass = 0;
                for (int i = OppInsIDPairs[c].A; i <= OppInsIDPairs[c].B; ++i, ++ass)
                    polyB[i].Position = OppRetPts[ass];
            }
            this.Refresh();
        }

        private void TransConst_ValueChanged(object sender, EventArgs e)
        {
            this.Refresh();
        }
    }
}
