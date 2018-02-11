using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JellyTetris.UI
{
    public class Image : Control
    {
        protected Texture2D _image;
        private float _trans_alpha;
        public int Width { get { return _image.Bounds.Width; } }
        public int Height { get { return _image.Bounds.Height; } }

        public Image(string image)
        {
            Position = Vector2.Zero;
            _image = ResourceManager.LoadImage(image);
            Enable = true;
        }

        public override void Draw()
        {
            float delay = Environment.TickCount - lasttime;

            float ex = 255.0f * delay / TransitionTime;

            if (Enable)
                _trans_alpha = _trans_alpha + ex < 255 ? _trans_alpha + ex : 255;
            else
                _trans_alpha = _trans_alpha - ex > 0 ? _trans_alpha - ex : 0;

            Color col = Color.White;
            col.A = (byte)(_trans_alpha * Alpha);

            if (col.A != 0)
                spriteBatch.Draw(_image, Position, col);

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
