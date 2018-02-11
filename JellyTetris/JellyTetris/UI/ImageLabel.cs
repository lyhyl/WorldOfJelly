using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace JellyTetris.UI
{
    public class ImageLabel : Control
    {
        protected Texture2D _image;
        private Vector2 targetPosition;
        private Vector2 currPosition;
        private const int offest = 50;
        private float _trans_alpha;

        private VertexPositionColor[] dvs;

        public ImageLabel(string image, Vector2 pos)
        {
            _image = ResourceManager.LoadImage(image);
            Position = pos;
            Enable = true;
            targetPosition = new Vector2(pos.X, pos.Y);
            currPosition = new Vector2(pos.X, pos.Y);

            dvs = new VertexPositionColor[2];
            dvs[0] = new VertexPositionColor(Vector3.Zero, Color.Orange);
            dvs[1] = new VertexPositionColor(Vector3.Zero, Color.Orange);
        }

        public override void Draw()
        {
            float delay = Environment.TickCount - lasttime;

            float ex = 255.0f * delay / TransitionTime;

            if (Enable)
                _trans_alpha = _trans_alpha + ex < 255 ? _trans_alpha + ex : 255;
            else
                _trans_alpha = _trans_alpha - ex > 0 ? _trans_alpha - ex : 0;

            //B
            targetPosition.X = Position.X;
            if (Position.X > (ControlSystem.SpaceWidth >> 1))//left
                targetPosition.X -= _image.Bounds.Width + offest;
            else//right
                targetPosition.X += offest;
            targetPosition.Y = Position.Y;
            if (Position.Y > (ControlSystem.SpaceHeight >> 1))//top
                targetPosition.Y -= _image.Bounds.Height + offest;
            else//bottom
                targetPosition.Y += offest;

            //A
            /*targetPosition.X = Position.X;
            int xd = -Image.Bounds.Width - offest;
            targetPosition.X += Position.X > UIResourceManager.SpaceWidth + xd ? xd : offest;
            targetPosition.Y = Position.Y;
            int yd = Image.Bounds.Height + offest;
            targetPosition.Y += Position.Y < yd ? offest : -yd;*/

            Vector2 dir = targetPosition - currPosition;
            dir *= delay / TransitionTime;
            currPosition = currPosition + dir;

            Color col = Color.White;
            col.A = (byte)(_trans_alpha * Alpha);

            dvs[0].Position.X = Position.X - (graphicsDevice.Viewport.Width >> 1);
            dvs[0].Position.Y = -Position.Y + (graphicsDevice.Viewport.Height >> 1);
            dvs[0].Color.A = col.A;
            dvs[1].Position.X = currPosition.X + ((_image.Bounds.Width - graphicsDevice.Viewport.Width) >> 1);
            dvs[1].Position.Y = -currPosition.Y - _image.Bounds.Height + (graphicsDevice.Viewport.Height >> 1);
            dvs[1].Color.A = col.A;

            if (col.A != 0)
            {
                spriteBatch.Draw(_image, currPosition, col);

                ResourceManager.DefaultEffect.TextureEnabled = false;
                ResourceManager.DefaultEffect.VertexColorEnabled = true;
                foreach (EffectPass pass in ResourceManager.DefaultEffect.CurrentTechnique.Passes)
                {
                    pass.Apply();
                    graphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, dvs, 0, 1);
                }
            }

            lasttime = Environment.TickCount;
        }

        public override void HandleMouse()
        {
        }

        protected override void preportyReset(Control.PreType pt)
        {
        }
    }
}
