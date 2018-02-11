using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JellyTetris.UI
{
    public class PureText : Control
    {
        public int Width { set; get; }
        public int Height { set; get; }
        public bool AutoPosition { set; get; }
        public Color TextColor { set; get; }

        private string text = "";
        public string Text { set { text = value; } get { return text; } }
        public SpriteFont Font;

        private float _trans_alpha = 0;

        public PureText()
        {
            TextColor = Color.Black;
            AutoPosition = false;
            Font = ResourceManager.TextFontSmall;
        }
        public PureText(SpriteFont font)
        {
            TextColor = Color.Black;
            AutoPosition = false;
            Font = font;
        }
        public PureText(SpriteFont font, string txt)
        {
            TextColor = Color.Black;
            AutoPosition = false;
            Font = font;
            text = txt;
        }

        public override void Draw()
        {
            if (AutoPosition)
            {
                Vector2 wh = Font.MeasureString(Text);
                Position = new Vector2(Width >> 1, Height >> 1) - wh * 0.5f;
            }

            float delay = Environment.TickCount - lasttime;

            float ex = 255.0f * delay / TransitionTime;

            if (Enable)
                _trans_alpha = _trans_alpha + ex < 255 ? _trans_alpha + ex : 255;
            else
                _trans_alpha = _trans_alpha - ex > 0 ? _trans_alpha - ex : 0;

            Color col = TextColor;
            col.A = (byte)(_trans_alpha * Alpha);

            if (col.A != 0)
                spriteBatch.DrawString(Font, text, Position, col);

            lasttime = Environment.TickCount;
        }

        public override void HandleMouse()
        {
        }

        protected override void preportyReset(Control.PreType pt)
        {
            if (pt == PreType.Enable && Enable) _trans_alpha = 0;
        }
    }
}
