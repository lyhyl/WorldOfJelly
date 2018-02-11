using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Drawing2D;

namespace SiteRecover
{
    public partial class SiteRecover : Form
    {
        //collision
        private List<Vector> restrictpt = new List<Vector>();
        private List<Vector> opprestrictpt = new List<Vector>();
        private List<Pair<int>> idpairs = new List<Pair<int>>();
        private List<Pair<int>> oppidpairs = new List<Pair<int>>();
        private List<Pair<Vector>> ptpairs = new List<Pair<Vector>>();
        private List<Pair<Vector>> oppptpairs = new List<Pair<Vector>>();

        private List<Vector> totrestrictpt = new List<Vector>();
        private List<Pair<Vector>> totptpairs = new List<Pair<Vector>>();


        //
        private FileStream file = null;
        private StreamReader reader = null;
        private string[] framebuffer;
        private List<Polygon> objlist = new List<Polygon>();
        private Point DrawOffest = new Point(250, 250);
        private Vector OrgMouse = new Vector();

        public SiteRecover()
        {
            InitializeComponent();
        }

        private void file_btn_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (reader != null)
                    reader.Close();
                if (file != null)
                    file.Close();

                try
                {
                    file = new FileStream(openFileDialog.FileName, FileMode.Open);
                }
                catch (IOException exp)
                {
                    MessageBox.Show("Cann't open the site file. Error Msg: " + exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception exp)
                {
                    MessageBox.Show("Unknow error. Error Msg: " + exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }

                reader = new StreamReader(file);
                string buffer = "";
                try
                {
                    buffer = reader.ReadToEnd();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }

                StringBuilder sbuilder = new StringBuilder(buffer);
                sbuilder.Replace("Frame", "");
                sbuilder.Replace("JellyObject", "t");
                buffer = sbuilder.ToString();

                char[] spt = { ':' };
                framebuffer = buffer.Split(spt);
                if (framebuffer.Length > 1)
                {
                    recover_frame_id.Minimum = 0;
                    recover_frame_id.Maximum = framebuffer.Length - 2;
                }
                else
                {
                    recover_frame_id.Minimum = -1;
                    recover_frame_id.Maximum = -1;
                }
            }
        }

        private Vector GetVector(ref string data)
        {
            StringBuilder sbuilder = new StringBuilder(data);
            sbuilder.Replace("<", "");
            sbuilder.Replace("(", "");
            sbuilder.Replace(")", "");

            string con = sbuilder.ToString();
            char[] spt = { ',' };
            string[] info = con.Split(spt);
            float x = 0;
            float y = 0;
            try
            {
                x = (float)Convert.ToDouble(info[0].Trim());
                y = (float)Convert.ToDouble(info[1].Trim());
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }

            return new Vector(x, y);
        }

        private void RecoverFrame(int fid)
        {
            objlist.Clear();

            char[] spt = { 't' };
            char[] sptb = { '>' };
            string[] objsptdata = framebuffer[fid].Split(spt);

            int c = 1;
            string obj;
            string[] vers;
            Polygon poly;
            for (; c < objsptdata.Length - 1; ++c)
            {
                obj = objsptdata[c];
                vers = obj.Split(sptb);
                poly = new Polygon(vers.Length - 2);//last two is ignored
                for (int v = 0; v < vers.Length - 2; ++v)
                    poly.Vertices[v].Position = GetVector(ref vers[v]);
                objlist.Add(poly);
            }

            obj = objsptdata[c];
            vers = obj.Split(sptb);
            poly = new Polygon(vers.Length - 3);//last three is ignored
            for (int v = 0; v < vers.Length - 3; ++v)
                poly.Vertices[v].Position = GetVector(ref vers[v]);
            objlist.Add(poly);

            this.Refresh();
        }

        private void recover_event(object sender, EventArgs e)
        {
            int frameid = (int)recover_frame_id.Value;
            if (frameid == -1)
                return;
            ++frameid;//the first one should be ignored. its value must is "Frame 0 :"
            RecoverFrame(frameid);
        }

        private void proccoll_btn_Click(object sender, EventArgs e)
        {
            totptpairs.Clear();
            totrestrictpt.Clear();

            for (int i = 0; i < objlist.Count; ++i)
            {
                Polygon joa = objlist[i];
                for (int j = i + 1; j < objlist.Count; ++j)
                {
                    Polygon job = objlist[j];

                    idpairs.Clear();
                    ptpairs.Clear();
                    oppidpairs.Clear();
                    oppptpairs.Clear();

                    JellyCollision.ProcessCollision(joa.Vertices, job.Vertices, ref idpairs, ref ptpairs);
                    JellyCollision.ProcessCollision(job.Vertices, joa.Vertices, ref oppidpairs, ref oppptpairs);

                    totptpairs.AddRange(ptpairs);
                    totptpairs.AddRange(oppptpairs);

                    JellyCollision.Restrict(joa.Vertices, ref idpairs, ref ptpairs, ref restrictpt);
                    JellyCollision.Restrict(job.Vertices, ref oppidpairs, ref oppptpairs, ref opprestrictpt);

                    totrestrictpt.AddRange(restrictpt);
                    totrestrictpt.AddRange(opprestrictpt);
                }
            }

            this.Refresh();
        }

        private void SiteRecover_Paint(object sender, PaintEventArgs e)
        {
            Matrix trans = new Matrix();
            trans.Scale(1, -1);
            trans.Translate(DrawOffest.X, -DrawOffest.Y);
            e.Graphics.Transform = trans;

            Pen edgepen = new Pen(Color.Blue);
            Pen pointpen = new Pen(Color.Green);

            foreach (Polygon poly in objlist)
                poly.Draw(e.Graphics);

            foreach (Pair<Vector> vp in totptpairs)
            {
                e.Graphics.DrawEllipse(pointpen, vp.A.X * Polygon.DrawScale - 2.5f, vp.A.Y * Polygon.DrawScale - 2.5f, 5, 5);
                e.Graphics.DrawEllipse(pointpen, vp.B.X * Polygon.DrawScale - 2.5f, vp.B.Y * Polygon.DrawScale - 2.5f, 5, 5);
            }

            foreach (Vector v in totrestrictpt)
            {
                e.Graphics.DrawEllipse(pointpen, v.X * Polygon.DrawScale - 2.5f, v.Y * Polygon.DrawScale - 2.5f, 5, 5);
            }
        }

        private void SiteRecover_MouseDown(object sender, MouseEventArgs e)
        {
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.SiteRecover_MouseMove);
            OrgMouse = new Vector(e.Location);
        }

        private void SiteRecover_MouseMove(object sender, MouseEventArgs e)
        {
            Vector pos = new Vector(e.Location);
            Vector diff = pos - OrgMouse;

            DrawOffest.X += (int)diff.X;
            DrawOffest.Y += (int)diff.Y;

            OrgMouse = pos;
            this.Refresh();
        }

        private void SiteRecover_MouseUp(object sender, MouseEventArgs e)
        {
            this.MouseMove -= this.SiteRecover_MouseMove;
        }

        private void SiteRecover_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Polygon.DrawScale += e.Delta > 0 ? 0.1f : -0.1f;
            if (Polygon.DrawScale < 0.1f)
                Polygon.DrawScale = 0.1f;
            this.Refresh();
        }

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            if (recover_frame_id.Value == recover_frame_id.Maximum)
            {
                play_btn.Text = "auto play";
                updateTimer.Stop();
                return;
            }
            recover_frame_id.Value += 1;
            recover_event(null, null);
        }

        private void play_btn_Click(object sender, EventArgs e)
        {
            if (updateTimer.Enabled)
            {
                play_btn.Text = "auto play";
                updateTimer.Stop();
            }
            else
            {
                play_btn.Text = "stop play";
                updateTimer.Start();
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            updateTimer.Interval = inval_bar.Value;
        }
    }
}
