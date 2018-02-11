#define _C_D_

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;

namespace UnionPreColl
{
    public partial class UnionPreColl : Form
    {
#if _C_A_
        //collision type A tmp data
        private List<Pair<int>> idpairs = new List<Pair<int>>();
        private List<Pair<Vector>> ptpairs = new List<Pair<Vector>>();
        private List<Pair<int>> oppidpairs = new List<Pair<int>>();
        private List<Pair<Vector>> oppptpairs = new List<Pair<Vector>>();
        private List<Vector> restrictpt = new List<Vector>();
        private List<Vector> opprestrictpt = new List<Vector>();
#endif
        
#if _C_B_
        //collision type B tmp data
        private List<Vector> inter_list = new List<Vector>();
        private List<Vector> opp_inter_list = new List<Vector>();
        private List<bool> io_list = new List<bool>();
        private List<bool> opp_io_list = new List<bool>();
        private List<int> discon_list = new List<int>();
        private List<int> opp_discon_list = new List<int>();
#endif
        
#if _C_D_ || _C_D2_
        //collision type D tmp data
        private List<bool> ptsio = new List<bool>();
        private List<Vector> closestpts = new List<Vector>();
#endif

        //control
        private Vector mpos;
        private int selectobjid = int.MaxValue;
        private int selecttriid = int.MaxValue;
        private Pair<double> selectbary = new Pair<double>();

        private const double unitmass = 0.5;
        private const double unitsize = 25;

        private JellyBox box = new JellyBox(unitmass, unitsize, new Vector(-120, 75));
        private JellyDouble dbl = new JellyDouble(unitmass, unitsize, new Vector(0, 50));
        private JellyTriple tri = new JellyTriple(unitmass, unitsize, new Vector(0, 200));
        private JellyBigBox bbx = new JellyBigBox(unitmass, unitsize, new Vector(0, 500));
        private JellyZShape zs = new JellyZShape(unitmass, unitsize, new Vector(0, 600));
        private JellyTShape ts = new JellyTShape(unitmass, unitsize, new Vector(75, 50));
        private JellyLong log = new JellyLong(unitmass, unitsize, new Vector(0, 400));

        internal List<JellyObject> jobjlist = new List<JellyObject>();

        private List<DebugInfo> dbginfolist = new List<DebugInfo>();
        private bool recording = false;

        public UnionPreColl()
        {
            InitializeComponent();

            JellyObject.DrawBase = true;
            JellyObject.DrawExtend = false;
            JellyObject.DrawCurve = true;

            jobjlist.Add(box);
            jobjlist.Add(dbl);
            jobjlist.Add(tri);
            /*jobjlist.Add(ts);
            jobjlist.Add(log);
            jobjlist.Add(bbx);
            jobjlist.Add(zs);*/
        }

        private void updatetimer_Tick(object sender, EventArgs e)
        {
            const double MouseJointForce = 10;

            if (selectobjid != int.MaxValue)
                jobjlist[selectobjid].AddForceAt(selecttriid, selectbary.A, selectbary.B, mpos, MouseJointForce);

            double timestep = (double)updatetimer.Interval * 0.001;

            foreach (JellyObject jo in jobjlist)
                jo.Preservation();

            //Ele Force
            for (int i = 0; i < jobjlist.Count; ++i)
            {
                JellyObject joi = jobjlist[i];
                for (int j = i + 1; j < jobjlist.Count; ++j)
                {
                    JellyObject joj = jobjlist[j];
#if _C_D_
                    foreach (JellyVertex jv in joj.Edge)
                        JellyCollision.ProcessEdgeForce(joi.Edge, jv);
                    foreach (JellyVertex jv in joi.Edge)
                        JellyCollision.ProcessEdgeForce(joj.Edge, jv);
#endif
#if _C_D2_
                    JellyCollision.ProcessClosestForce(joi.Edge, joj.Edge);
                    JellyCollision.ProcessClosestForce(joj.Edge, joi.Edge);
#endif
                }
            }

            foreach (JellyObject jo in jobjlist)
                jo.Integrate(timestep);

            if (recording)
                dbginfolist.Add(DebugInfo.Create(this));

#if _C_A_ || _C_B_
#if _C_M_ITER_
            int iteration = 4;
            for (int iter = 0; iter < iteration; ++iter)
#endif
            for (int i = 0; i < jobjlist.Count; ++i)
            {
                JellyObject joa = jobjlist[i];
                for (int j = i + 1; j < jobjlist.Count; ++j)
                {
                    JellyObject job = jobjlist[j];
#if _C_A_
                    idpairs.Clear();
                    ptpairs.Clear();
                    oppidpairs.Clear();
                    oppptpairs.Clear();

                    JellyCollision.ProcessCollision(joa.Edge, job.Edge, ref idpairs, ref ptpairs);
                    JellyCollision.ProcessCollision(job.Edge, joa.Edge, ref oppidpairs, ref oppptpairs);

                    JellyCollision.Restrict(joa.Edge, ref idpairs, ref ptpairs, ref restrictpt);
                    JellyCollision.Restrict(job.Edge, ref oppidpairs, ref oppptpairs, ref opprestrictpt);

                    joa.AddIDsPairs(ref idpairs);
                    joa.AddRestrictPts(ref restrictpt);
                    job.AddIDsPairs(ref oppidpairs);
                    job.AddRestrictPts(ref opprestrictpt);
#endif
#if _C_B_
                    JellyCollision.ProcessCollisionB(joa.Edge, job.Edge, ref io_list, ref discon_list, ref inter_list);
                    JellyCollision.ProcessCollisionB(job.Edge, joa.Edge, ref opp_io_list, ref opp_discon_list, ref opp_inter_list);
                    
                    JellyCollision.RestrictPolygon(joa.Edge, ref inter_list, ref discon_list);
                    JellyCollision.RestrictPolygon(job.Edge, ref opp_inter_list, ref opp_discon_list);
#endif
                }
            }
            foreach (JellyObject jo in jobjlist)
                jo.ApplyRestrict();
#endif

            this.Refresh();
        }

        private void UnionPreColl_Paint(object sender, PaintEventArgs e)
        {
            Matrix trans = new Matrix();
            trans.Scale(1, -1);
            trans.Translate(this.Size.Width >> 1, -this.Size.Height >> 1);
            e.Graphics.Transform = trans;

            Pen graypen = new Pen(Color.Gray);
            Pen greenpen = new Pen(Color.Green, 2f);

            e.Graphics.DrawLine(graypen, JellyWorld.WorldLeft, 0, JellyWorld.WorldRight, 0);
            e.Graphics.DrawLine(graypen, 0, JellyWorld.WorldTop, 0, JellyWorld.WorldBottom);

            foreach (JellyObject jo in jobjlist)
                jo.DebugDraw(e.Graphics);
        }

        private void UnionPreColl_MouseDown(object sender, MouseEventArgs e)
        {
            mpos = new Vector(e.Location);
            mpos.X -= this.Size.Width >> 1;
            mpos.Y -= this.Size.Height >> 1;
            mpos.Y = -mpos.Y;
            double u = -1, v = -1;
            int count = 0;
            selectobjid = int.MaxValue;
            foreach (JellyObject jo in jobjlist)
            {
                if (jo.Select(mpos, out selecttriid, out u, out v))
                {
                    selectobjid = count;
                    this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.UnionPreColl_MouseMove);
                    break;
                }
                ++count;
            }
            selectbary.A = u;
            selectbary.B = v;
        }

        private void UnionPreColl_MouseMove(object sender, MouseEventArgs e)
        {
            mpos = new Vector(e.Location);
            mpos.X -= this.Size.Width >> 1;
            mpos.Y -= this.Size.Height >> 1;
            mpos.Y = -mpos.Y;
        }

        private void UnionPreColl_MouseUp(object sender, MouseEventArgs e)
        {
            this.MouseMove -= this.UnionPreColl_MouseMove;
            selecttriid = int.MaxValue;
            selectobjid = int.MaxValue;
        }

        private void ctrl_btn_Click(object sender, EventArgs e)
        {
            if (updatetimer.Enabled)
                updatetimer.Stop();
            else
                updatetimer.Start();
        }

        private void setp_btn_Click(object sender, EventArgs e)
        {
            updatetimer_Tick(null, null);
        }

        private void record_btn_Click(object sender, EventArgs e)
        {
            if (recording)
                record_btn.Text = "Record";
            else
                record_btn.Text = "Recording";
            recording = !recording;
        }

        private void output_btn_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileStream file = new FileStream(saveFileDialog.FileName, FileMode.Create);
                StreamWriter writer = new StreamWriter(file);
                int id = 0;
                foreach (DebugInfo dbginfo in dbginfolist)
                    writer.Write(dbginfo.ToString(id++));
                writer.Close();
                file.Close();
            }
        }

        private void UnionPreColl_Resize(object sender, EventArgs e)
        {
            int w = (this.Size.Width - 100) >> 1;
            int h = (this.Size.Height - 100) >> 1;
            JellyWorld.WorldBottom = -h;
            JellyWorld.WorldTop = h + 700;
            JellyWorld.WorldLeft = -w;
            JellyWorld.WorldRight = w;
        }
    }
}
