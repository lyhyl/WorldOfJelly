using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;

namespace JellyCodeBuilder
{
    public partial class JellyCodeBuilder : Form
    {
        public JellyCodeBuilder()
        {
            InitializeComponent();

            InformationLabel.Text = "";
            ErrorLabel.Text = "";

            TabsString = "";
            int[] tabs = { 25, 50, 75, 100, 125, 150, 175, 200 };
            CodeViewer.SelectionTabs = tabs;

            MouseState = MouseButtons.None;

            DrawCenter = new Point(200, 200);
            FocusID = new Point(0, 0);
            SelectID = new Point(0, 0);
            CreateType = 4;

            CreatingDisLink = false;
            CreatingAreLink = false;
        }

        private string TabsString { get; set; }

        private List<Point> Nodes = new List<Point>();
        private List<Pair<Point>> DisLink = new List<Pair<Point>>();
        private List<Triple<Point>> AreLink = new List<Triple<Point>>();
        private List<int> Edge = new List<int>();

        private bool CreatingDisLink { set; get; }
        private bool CreatingAreLink { set; get; }

        private Point DisLinkTmp { set; get; }
        private Point AreLinkTmpA { set; get; }
        private Point AreLinkTmpB { set; get; }

        private MouseButtons MouseState { set; get; }
        private Point MousePos { set; get; }
        private Point FocusID { set; get; }
        private Point SelectID { set; get; }
        private int CreateType { set; get; }

        private Point DrawCenter { set; get; }

        private void GenerateCode()
        {
            ClearCode();

            AppendCode("using System;");
            AppendCode("using System.Collections.Generic;");
            AppendCode("using System.Linq;");
            AppendCode("using System.Text;");
            AppendCode("using System.Drawing;");
            AppendCode("");
            AppendCode("namespace " + NamespaceTextBox.Text);
            AppendCode("{");

            EnterBlock();

            AppendCode("public class Jelly" + TypeNameTextBox.Text + " : JellyObject");
            AppendCode("{");

            EnterBlock();

            AppendCode("public Jelly" + TypeNameTextBox.Text + "(double mass, double size, Vector pos)");
            AppendCode("\t: base(mass, size, " + Edge.Count + ")");
            AppendCode("{");

            EnterBlock();

            GenerateNode();
            GenerateDisLink();
            GenerateAreLink();
            GenerateEdge();

            LeaveBlock();

            AppendCode("}");

            //AppendCode("");
            //GenerateDebugDraw();

            LeaveBlock();

            AppendCode("}");

            LeaveBlock();

            AppendCode("}");
        }

        private void GenerateDebugDraw()
        {
            AppendCode("protected override void DebugDrawEdge(Graphics g)");
            AppendCode("{");
            EnterBlock();
            AppendCode("Pen pen = new Pen(Color.Green);");
            for (int c = 0; c < Edge.Count - 1; )
                AppendCode("g.DrawLine(pen, _nodes[" + Edge[c] + "].Position, _nodes[" + Edge[++c] + "].Position);");
            if (Edge.Count != 0) AppendCode("g.DrawLine(pen, _nodes[" + Edge[Edge.Count - 1] + "].Position, _nodes[" + Edge[0] + "].Position);");
            LeaveBlock();
            AppendCode("}");
        }

        private void GenerateEdge()
        {
            AppendComment("edges");
            int count = 0;
            foreach (int num in Edge)
            {
                AppendCode("_edge[" + count + "] = _nodes[" + num + "];");
                ++count;
            }

            int resa, resb;
            AppendCode("");
            AppendComment("edges' normals");
            int i = 0;
            while (i < Edge.Count - 1)
            {
                resa = DisLink.FindIndex(pp => (pp.A == Nodes[Edge[i]] && pp.B == Nodes[Edge[i + 1]]));
                resb = DisLink.FindIndex(pp => (pp.A == Nodes[Edge[i + 1]] && pp.B == Nodes[Edge[i]]));
                AppendCode("_edgenor[" + i + "] = " + "_disprelinks[" + (resa >= 0 ? resa : resb) + "]." + (resa >= 0 ? "NegNormal;" : "PosNormal;"));
                ++i;
            }
            if (Edge.Count != 0)
            {
                resa = DisLink.FindIndex(pp => (pp.A == Nodes[Edge[i]] && pp.B == Nodes[Edge[0]]));
                resb = DisLink.FindIndex(pp => (pp.A == Nodes[Edge[0]] && pp.B == Nodes[Edge[i]]));
                AppendCode("_edgenor[" + i + "] = " + "_disprelinks[" + (resa >= 0 ? resa : resb) + "]." + (resa >= 0 ? "NegNormal;" : "PosNormal;"));
            }
        }

        private void GenerateAreLink()
        {
            AppendComment("area links");
            AppendCode("double KA = 10000;");
            AppendCode("");
            AppendCode("_areprelinks = new ArePreLink[" + AreLink.Count.ToString() + "];");
            int c = 0;
            foreach (Triple<Point> are in AreLink)
            {
                Point target = are.A;
                Predicate<Point> equ = delegate(Point p) { return p == target; };
                int resa = Nodes.FindIndex(equ);
                target = are.B;
                int resb = Nodes.FindIndex(equ);
                target = are.C;
                int resc = Nodes.FindIndex(equ);

                Point distara = are.A, distarb = are.B;
                bool pos = true;
                Predicate<Pair<Point>> disequ = delegate(Pair<Point> dis)
                {
                    if (dis.A == distara && dis.B == distarb)
                        return pos = true;
                    else if (dis.A == distarb && dis.B == distara)
                    {
                        pos = false;
                        return true;
                    }
                    return false;
                };
                int resda = DisLink.FindIndex(disequ);
                bool posa = pos;
                distara = are.B;
                distarb = are.C;
                int resdb = DisLink.FindIndex(disequ);
                bool posb = pos;
                distara = are.C;
                distarb = are.A;
                int resdc = DisLink.FindIndex(disequ);
                bool posc = pos;
                AppendCode("_areprelinks[" + c + "] = new ArePreLink(_nodes[" + resa + "], _nodes[" + resb + "], _nodes[" + resc + "]," +
                    " KA, _disprelinks[" + resda + "]." + (posa ? "PosNormal" : "NegNormal") +
                    ", _disprelinks[" + resdb + "]." + (posb ? "PosNormal" : "NegNormal") +
                    ", _disprelinks[" + resdc + "]." + (posc ? "PosNormal" : "NegNormal") + ");");
                ++c;
            }
            AppendCode("");
        }

        private void GenerateDisLink()
        {
            AppendComment("distance links");
            AppendCode("double KS = JellyWorld.KS;");
            AppendCode("double KD = JellyWorld.KD;");
            AppendCode("");
            AppendCode("_disprelinks = new DisPreLink[" + DisLink.Count.ToString() + "];");
            int c = 0;
            foreach (Pair<Point> dis in DisLink)
            {
                Point target = dis.A;
                Predicate<Point> equ = delegate(Point p) { return p == target; };
                int resa = Nodes.FindIndex(equ);
                target = dis.B;
                int resb = Nodes.FindIndex(equ);
                AppendCode("_disprelinks[" + c + "] = new DisPreLink(_nodes[" + resa + "], _nodes[" + resb + "], KS, KD);");
                ++c;
            }
            AppendCode("");
        }

        private void GenerateNode()
        {
            AppendComment("nodes");
            AppendCode("_nodes = new JellyVertex[" + Nodes.Count.ToString() + "];");
            int c = 0;
            foreach (Point node in Nodes)
            {
                AppendCode("_nodes[" + c.ToString() + "] = new JellyVertex(new Vector(pos.X + " + node.X + " * size, pos.Y + " + (-node.Y) + " * size));");
                ++c;
            }
            AppendCode("");
        }

        private void EnterBlock()
        {
            TabsString += "\t";
        }

        private void LeaveBlock()
        {
            TabsString = TabsString.Remove(TabsString.Length - 1);
        }

        private void ClearCode()
        {
            TabsString = "";
            CodeViewer.ResetText();
        }

        private void AppendCode(string code)
        {
            CodeViewer.AppendText(TabsString + code + "\n");
        }

        private void AppendComment(string cmt)
        {
            CodeViewer.AppendText(TabsString + "//" + cmt + "\n");
        }

        private void BeginLargeComment()
        {
            CodeViewer.AppendText(TabsString + "/*\n");
        }

        private void EndLargeComment()
        {
            CodeViewer.AppendText(TabsString + "*/\n");
        }

        private void JellyCodeGenerater_Load(object sender, EventArgs e)
        {
            GenerateCode();
        }

        private void TypeNameTextBox_TextChanged(object sender, EventArgs e)
        {
            GenerateCode();
        }

        private void NamespaceTextBox_TextChanged(object sender, EventArgs e)
        {
            GenerateCode();
        }

        private void DrawPlane_Paint(object sender, PaintEventArgs e)
        {
            Matrix trans = new Matrix();
            trans.Translate(DrawCenter.X, DrawCenter.Y);
            e.Graphics.Transform = trans;

            Pen redpen = new Pen(Color.Red);
            Pen blackpen = new Pen(Color.Black);
            Pen yellowpen = new Pen(Color.Yellow, 2);
            Pen orangepen = new Pen(Color.Orange, 2);
            Pen greenpen = new Pen(Color.LightGreen);

            Font font = new Font("Arial", 9);

            for (int c = -(int)RangeNUD.Value; c <= (int)RangeNUD.Value; ++c)
            {
                e.Graphics.DrawEllipse(blackpen, c * (float)UnitSizeNUD.Value - 2, -2, 4, 4);
                e.Graphics.DrawEllipse(blackpen, - 2, c * (float)UnitSizeNUD.Value - 2, 4, 4);
            }

            float rectinfo = -(float)(RangeNUD.Value * UnitSizeNUD.Value);
            e.Graphics.DrawRectangle(blackpen, rectinfo, rectinfo, rectinfo * -2, rectinfo * -2);
            e.Graphics.DrawLine(blackpen, rectinfo * 1.5f, 0, rectinfo * -1.5f, 0);
            e.Graphics.DrawLine(blackpen, 0, rectinfo * 1.5f, 0, rectinfo * -1.5f);

            e.Graphics.DrawEllipse(yellowpen, (float)UnitSizeNUD.Value * FocusID.X - 4, (float)UnitSizeNUD.Value * FocusID.Y - 4, 8, 8);
            e.Graphics.DrawEllipse(orangepen, (float)UnitSizeNUD.Value * SelectID.X - 4, (float)UnitSizeNUD.Value * SelectID.Y - 4, 8, 8);

            int counter;

            counter = 0;
            foreach (Point node in Nodes)
            {
                DrawNode((int)(UnitSizeNUD.Value * node.X), (int)(UnitSizeNUD.Value * node.Y), e.Graphics, blackpen);
                e.Graphics.DrawString(counter.ToString(), font, Brushes.Black, (float)UnitSizeNUD.Value * node.X, (float)UnitSizeNUD.Value * node.Y);
                ++counter;
            }
            counter = 0;
            foreach (Pair<Point> dis in DisLink)
            {
                DrawDis(dis.A, dis.B, e.Graphics, greenpen);
                e.Graphics.DrawString(counter.ToString(), font, Brushes.LightGreen, (float)UnitSizeNUD.Value * (dis.A.X + dis.B.X) * 0.5f,
                    (float)UnitSizeNUD.Value * (dis.A.Y + dis.B.Y) * 0.5f);
                ++counter;
            }
            if (CreatingDisLink)
                DrawDis(SelectID, FocusID, e.Graphics, greenpen);

            counter = 0;
            foreach (Triple<Point> are in AreLink)
            {
                DrawAre(are.A, are.B, are.C, e.Graphics, redpen);
                e.Graphics.DrawString(counter.ToString(), font, Brushes.Red,
                    (float)UnitSizeNUD.Value / 3 * (are.A.X + are.B.X + are.C.X),
                    (float)UnitSizeNUD.Value / 3 * (are.A.Y + are.B.Y + are.C.Y));
                ++counter;
            }
        }

        private void DrawTrianlge(Point pos, int size, Graphics g, Pen pen)
        {
            Size top = new Size(0, -size);
            Size left = new Size(-size, size);
            Size right = new Size(size, size);
            g.DrawLine(pen, pos + top, pos + left);
            g.DrawLine(pen, pos + left, pos + right);
            g.DrawLine(pen, pos + right, pos + top);
        }
        private void DrawAre(Point a, Point b, Point c, Graphics g, Pen pen)
        {
            Point mid = new Point((int)(UnitSizeNUD.Value * (a.X + b.X + c.X) / 3),
                    (int)(UnitSizeNUD.Value * (a.Y + b.Y + c.Y) / 3));
            DrawTrianlge(mid, 5, g, pen);
            g.DrawLine(pen, mid.X, mid.Y, a.X * (float)UnitSizeNUD.Value, a.Y * (float)UnitSizeNUD.Value);
            g.DrawLine(pen, mid.X, mid.Y, b.X * (float)UnitSizeNUD.Value, b.Y * (float)UnitSizeNUD.Value);
            g.DrawLine(pen, mid.X, mid.Y,c.X * (float)UnitSizeNUD.Value, c.Y * (float)UnitSizeNUD.Value);
        }

        private void DrawDis(Point a, Point b, Graphics g, Pen pen)
        {
            PointF start = new PointF((float)UnitSizeNUD.Value * a.X, (float)UnitSizeNUD.Value * a.Y);
            PointF end = new PointF((float)UnitSizeNUD.Value * b.X, (float)UnitSizeNUD.Value * b.Y);
            g.DrawLine(pen, start, end);
            PointF mid = new PointF((end.X + start.X) * 0.5f, (end.Y + start.Y) * 0.5f);
            SizeF nor = new SizeF(-(end.Y - start.Y) * 0.25f, (end.X - start.X) * 0.25f);
            g.DrawLine(pen, mid, mid + nor);
            pen.Width = 3;
            PointF diff = new PointF(end.X * 0.7f + start.X * 0.3f, end.Y * 0.7f + start.Y * 0.3f);
            g.DrawLine(pen, diff, end);
            pen.Width = 1;
        }

        private void DrawNode(int x, int y, Graphics g, Pen pen)
        {
            g.DrawLine(pen, x - 4, y - 4, x + 4, y + 4);
            g.DrawLine(pen, x + 4, y - 4, x - 4, y + 4);
        }

        private bool IsCCW(Point a, Point b, Point c)
        {
            int abx = b.X - a.X;
            int aby = b.Y - a.Y;
            int acx = c.X - a.X;
            int acy = c.Y - a.Y;
            return aby * acx > abx * acy;
        }
        private bool FindExistAreLink(Point A, Point B, Point C)
        {
            Predicate<Triple<Point>> equ = delegate(Triple<Point> tr)
            {
                if (tr.A == A)
                {
                    if (tr.B == B)
                        return tr.C == C;
                    else if (tr.C == B)
                        return tr.B == C;
                }
                else if (tr.B == A)
                {
                    if (tr.C == B)
                        return tr.A == C;
                    else if (tr.A == B)
                        return tr.C == C;
                }
                else if (tr.C == A)
                {
                    if (tr.A == B)
                        return tr.B == C;
                    else if (tr.B == B)
                        return tr.A == C;
                }
                return false;
            };
            return AreLink.FindIndex(equ) != -1;
        }
        private void InterruptAreCreate(string error)
        {
            CreatingAreLink = false;
            AreLinkTmpA = Point.Empty;
            AreLinkTmpB = Point.Empty;
            ErrorLabel.Text = error;
        }
        private void InterruptDisCreate(string error)
        {
            CreatingDisLink = false;
            DisLinkTmp = Point.Empty;
            ErrorLabel.Text = error;
        }
        private void DrawPlane_MouseDown(object sender, MouseEventArgs e)
        {
            MousePos = e.Location;
            MouseState = e.Button;
            switch (MouseState)
            {
                case MouseButtons.Left:
                    SelectID = FocusID;
                    InformationLabel.Text = "Select Node(" + SelectID + ")";
                    ErrorLabel.Text = "";

                    Predicate<Point> equ = delegate(Point p) { return p == SelectID; };
                    switch (CreateType)
                    {
                        case 0:
                            {
                                int res = Nodes.FindIndex(equ);
                                if (res == -1)
                                {
                                    Nodes.Add(SelectID);
                                    GenerateCode();
                                    InformationLabel.Text += "(No." + Nodes.Count + ")";
                                }
                                else
                                    ErrorLabel.Text = "Node Exist";
                            }
                            break;
                        case 1:
                            {
                                if (!CreatingDisLink)
                                {
                                    int res = Nodes.FindIndex(equ);
                                    if (res == -1)
                                        InterruptDisCreate("Need Node");
                                    else
                                    {
                                        CreatingDisLink = true;
                                        DisLinkTmp = SelectID;
                                    }
                                }
                                else
                                {
                                    if (SelectID == DisLinkTmp)
                                    {
                                        InterruptDisCreate("Length = 0");
                                        break;
                                    }
                                    int res = Nodes.FindIndex(equ);
                                    if (res == -1)
                                        InterruptDisCreate("Need Node");
                                    else
                                    {
                                        Pair<Point> pp = Pair.CreatePair(DisLinkTmp, SelectID);
                                        Pair<Point> ppb = Pair.CreatePair(SelectID, DisLinkTmp);
                                        Predicate<Pair<Point>> dequ = delegate(Pair<Point> p) { return p == pp || p == ppb; };
                                        res = DisLink.FindIndex(dequ);
                                        if (res == -1)
                                        {
                                            DisLink.Add(pp);
                                            CreatingDisLink = false;
                                            GenerateCode();
                                        }
                                        else
                                            InterruptDisCreate("Dis Link Exist");
                                    }
                                }
                            }
                            break;
                        case 2:
                            {
                                if (!CreatingAreLink)
                                {
                                    int res = Nodes.FindIndex(equ);
                                    if (res == -1)
                                        InterruptAreCreate("Need Node");
                                    else
                                    {
                                        CreatingAreLink = true;
                                        AreLinkTmpA = SelectID;
                                    }
                                }
                                else
                                {
                                    int res = Nodes.FindIndex(equ);
                                    if (res == -1)
                                        InterruptAreCreate("Need Node");
                                    else if (AreLinkTmpA != SelectID)
                                    {
                                        if (AreLinkTmpB.IsEmpty)
                                            AreLinkTmpB = SelectID;
                                        else if (AreLinkTmpB != SelectID)
                                        {
                                            if (!FindExistAreLink(AreLinkTmpA, AreLinkTmpB, SelectID))
                                            {
                                                if (IsCCW(AreLinkTmpA, AreLinkTmpB, SelectID))
                                                    AreLink.Add(new Triple<Point>(AreLinkTmpA, AreLinkTmpB, SelectID));
                                                else
                                                    AreLink.Add(new Triple<Point>(AreLinkTmpA, SelectID, AreLinkTmpB));
                                                CreatingAreLink = false;
                                                AreLinkTmpA = Point.Empty;
                                                AreLinkTmpB = Point.Empty;
                                                GenerateCode();
                                            }
                                            else
                                                InterruptAreCreate("Are Exist");
                                        }
                                        else
                                            InterruptAreCreate("Length = 0");
                                    }
                                    else
                                        InterruptAreCreate("Length = 0");
                                }
                            }
                            break;
                        case 3:
                            {
                                int res = Nodes.FindIndex(equ);
                                if (res != -1)
                                    InformationLabel.Text += "(No." + res + ")";
                            }
                            break;
                    }
                    break;
                case MouseButtons.Middle:
                    Cursor = Cursors.NoMove2D;
                    break;
                case MouseButtons.Right:
                    break;
            }
        }

        private void DrawPlane_MouseMove(object sender, MouseEventArgs e)
        {
            switch (MouseState)
            {
                case MouseButtons.None:
                    Point mpos = e.Location;
                    mpos.X -= DrawCenter.X;
                    mpos.Y -= DrawCenter.Y;

                    Point sid = new Point((int)Math.Round((decimal)mpos.X / UnitSizeNUD.Value), (int)Math.Round((decimal)mpos.Y / UnitSizeNUD.Value));
                    if (sid != FocusID)
                    {
                        FocusID = sid;
                        MousePos = e.Location;
                        DrawPlane.Refresh();
                    }
                    break;
                case MouseButtons.Middle:
                    Point diff = e.Location - new Size(MousePos);
                    DrawCenter += new Size(diff);
                    MousePos = e.Location;
                    DrawPlane.Refresh();
                    break;
            }
        }

        private void DrawPlane_MouseUp(object sender, MouseEventArgs e)
        {
            MouseState = MouseButtons.None;
            Cursor = Cursors.Default;
        }

        private void rebuildNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TypeNameTextBox.Text = "Default";
            NamespaceTextBox.Text = "JellyFactory";
        }

        private void copyCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CodeViewer.SelectionStart = 0;
            CodeViewer.SelectionLength = CodeViewer.TextLength;
            CodeViewer.Copy();
        }

        private void RangeNUD_ValueChanged(object sender, EventArgs e)
        {
            this.DrawPlane.Refresh();
        }

        private void UnitSizeNUD_ValueChanged(object sender, EventArgs e)
        {
            this.DrawPlane.Refresh();
        }

        private void CheckedChanged(object sender, EventArgs e)
        {
            CreateType = Convert.ToInt32((string)((RadioButton)sender).Tag);
        }

        private void JellyCodeGenerater_KeyDown(object sender, KeyEventArgs e)
        {
            switch (CreateType)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    if (e.KeyCode == Keys.Delete)
                    {
                        Predicate<Pair<Point>> finddis = delegate(Pair<Point> p) { return p.A == SelectID || p.B == SelectID; };
                        List<Pair<Point>> matchdis = DisLink.FindAll(finddis);
                        foreach (Pair<Point> p in matchdis)
                            DisLink.Remove(p);
                        Predicate<Triple<Point>> findare = delegate(Triple<Point> p) { return p.A == SelectID || p.B == SelectID || p.C == SelectID; };
                        List<Triple<Point>> matchare = AreLink.FindAll(findare);
                        foreach (Triple<Point> p in matchare)
                            AreLink.Remove(p);
                        Nodes.Remove(SelectID);
                        GenerateCode();
                    }
                    break;
            }
        }

        private void EdgeBox_TextChanged(object sender, EventArgs e)
        {
            char[] spt=new char[]{','};
            string[] sptlist = EdgeBox.Text.Split(spt, StringSplitOptions.RemoveEmptyEntries);
            Edge.Clear();
            try
            {
                foreach (string str in sptlist)
                    Edge.Add(Convert.ToInt32(str));
            }
            catch (Exception exp)
            {
                ErrorLabel.Text = exp.Message + "   Need Numbers";
            }
            GenerateCode();
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "Jelly" + TypeNameTextBox.Text;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileStream file = new FileStream(saveFileDialog1.FileName, FileMode.Create);
                StreamWriter writer = new StreamWriter(file);
                writer.Write(CodeViewer.Text);
                writer.Close();
                file.Close();
            }
        }
    }
}