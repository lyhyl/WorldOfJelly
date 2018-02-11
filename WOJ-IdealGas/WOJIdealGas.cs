using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using WOJ_IdealGas.JellyObjects;

namespace WOJ_IdealGas
{
    public partial class WOJIdealGas : Form
    {
        private JellyBall ball = new JellyBall(25, 16, 50);
        private JellyBox box = new JellyBox(50, 50);

        public WOJIdealGas()
        {
            InitializeComponent();
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            float step = 0.03f;

            ball.Update(step);
            box.Update(step);
            volume_lbl.Text = "volume : " + ball.GetVolume().ToString();

            this.Refresh();
        }

        private void WOJIdealGas_Paint(object sender, PaintEventArgs e)
        {
            Matrix trans = new Matrix();
            trans.Scale(1, -1);
            trans.Translate(400, -300);
            e.Graphics.Transform = trans;

            ball.DebugDraw(e.Graphics);
            box.DebugDraw(e.Graphics);
        }

        private void CtrlBtn_Click(object sender, EventArgs e)
        {
            if (UpdateTimer.Enabled)
            {
                UpdateTimer.Stop();
                CtrlBtn.Text = "Run";
            }
            else
            {
                UpdateTimer.Start();
                CtrlBtn.Text = "Pause";
            }
        }

        private void StepBtn_Click(object sender, EventArgs e)
        {
            UpdateTimer_Tick(null, null);
        }
    }
}
