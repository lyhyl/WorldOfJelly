using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace JellyTetris.UI
{
    public abstract class Compositor : Control
    {
        private Vector2 prvPos;
        protected List<Control> controlList = new List<Control>();

        protected Compositor(Vector2 pos)
        {
            prvPos = pos;
            Position = pos;
        }

        public override void Draw()
        {
            foreach (Control ctrl in controlList)
                ctrl.Draw();
        }

        public override void HandleMouse()
        {
            foreach (Control ctrl in controlList)
                ctrl.HandleMouse();
        }

        protected override void preportyReset(Control.PreType pt)
        {
            switch (pt)
            {
                case PreType.Alpha:
                    foreach (Control ctrl in controlList)
                        ctrl.Alpha = Alpha;
                    break;
                case PreType.Enable:
                    foreach (Control ctrl in controlList)
                        ctrl.Enable = Enable;
                    break;
                case PreType.Position:
                    Vector2 diff = Position - prvPos;
                    foreach (Control ctrl in controlList)
                        ctrl.Position += diff;
                    break;
                case PreType.Region:
                    break;
                case PreType.TranTime:
                    foreach (Control ctrl in controlList)
                        ctrl.TransitionTime = TransitionTime;
                    break;
            }
        }
    }
}
