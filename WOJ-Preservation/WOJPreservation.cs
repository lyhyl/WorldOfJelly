using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace UnionPreColl
{
    public partial class WOJPreservation : Form
    {
        private Vector mpos;
        private int selectobj = int.MaxValue;
        private int selectid = int.MaxValue;
        private Pair<float> selectbary = new Pair<float>();
        private JellyBaseTriangle tri = new JellyBaseTriangle(new JellyVertex(0, 0), new JellyVertex(50, 10), new JellyVertex(20, 60), 50);
        private JellyBox box = new JellyBox(50, new Vector(-150, 100), 50);

        public WOJPreservation()
        {
            InitializeComponent();
        }

        private void updatetimer_Tick(object sender, EventArgs e)
        {
            if (selectobj != int.MaxValue)
            {
                if (selectobj == 0)
                    tri.AddForceAt(selectid, selectbary.A, selectbary.B, mpos, 150);
                if (selectobj == 1)
                    box.AddForceAt(selectid, selectbary.A, selectbary.B, mpos, 150);
            }

            float timestep = 0.025f;

            tri.Update(timestep);

            box.Update(timestep);


            this.Refresh();
        }

        private void WOJPreservation_Paint(object sender, PaintEventArgs e)
        {
            area_info.Text = "Area : " + tri.DoubleArea.ToString();

            Matrix trans = new Matrix();
            trans.Scale(1, -1);
            trans.Translate(250, -250);
            e.Graphics.Transform = trans;

            Pen graypen = new Pen(Color.Gray);
            Pen greenpen = new Pen(Color.Green, 2f);

            e.Graphics.DrawLine(graypen, -250, 0, 250, 0);
            e.Graphics.DrawLine(graypen, 0, -250, 0, 250);

            tri.DebugDraw(e.Graphics);
            box.DebugDraw(e.Graphics);
        }

        private void ctrl_btn_Click(object sender, EventArgs e)
        {
            if (updatetimer.Enabled)
                updatetimer.Stop();
            else
                updatetimer.Start();
        }

        private void step_btn_Click(object sender, EventArgs e)
        {
            updatetimer_Tick(null, null);
        }

        private void WOJPreservation_MouseDown(object sender, MouseEventArgs e)
        {
            mpos = new Vector(e.Location);
            mpos.X -= 250;
            mpos.Y -= 250;
            mpos.Y = -mpos.Y;
            float u, v;
            if (tri.Select(mpos, out selectid, out u, out v))
            {
                selectobj = 0;
                this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.WOJPreservation_MouseMove);
            }
            else if (box.Select(mpos, out selectid, out u, out v))
            {
                selectobj = 1;
                this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.WOJPreservation_MouseMove);
            }
            selectbary.A = u;
            selectbary.B = v;
        }

        private void WOJPreservation_MouseMove(object sender, MouseEventArgs e)
        {
            mpos = new Vector(e.Location);
            mpos.X -= 250;
            mpos.Y -= 250;
            mpos.Y = -mpos.Y;
        }

        private void WOJPreservation_MouseUp(object sender, MouseEventArgs e)
        {
            this.MouseMove -= this.WOJPreservation_MouseMove;
            selectid = int.MaxValue;
            selectobj = int.MaxValue;
        }
    }
}
