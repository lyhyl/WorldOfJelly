using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace JellyTetris.UI
{
    public class Button : Control
    {
        public delegate void PressEventHandler(Button target);
        public delegate void HoverEventHandler(Button target);
        public delegate void InEventHandler(Button target);
        public delegate void OutEventHandler(Button target);
        public event PressEventHandler PressEvent;
        public event HoverEventHandler HoverEvent;
        public event InEventHandler InEvent;
        public event OutEventHandler OutEvent;
        private Rectangle range;

        protected Texture2D _image;

        public Button(string image)
        {
            _image = ResourceManager.LoadImage(image);
            range = _image.Bounds;
        }

        public Button(string image, Rectangle rge)
        {
            _image = ResourceManager.LoadImage(image);
            range = rge;
        }

        public Button(string image, Rectangle rge, PressEventHandler handler)
        {
            _image = ResourceManager.LoadImage(image);
            range = rge;
            PressEvent = handler;
        }

        public override void Draw()
        {
            spriteBatch.Draw(_image, Position, Color.White);
        }

        public override void HandleMouse()
        {
            if (ControlSystem.MouseInRectangle(new Rectangle((int)Position.X, (int)Position.Y, range.Width, range.Height)))
                switch (ControlSystem.CurrentMouseState.LeftButton)
                {
                    case ButtonState.Pressed:
                        PressEvent(this);
                        break;
                }
        }

        protected override void preportyReset(Control.PreType pt)
        {
        }
    }
}
