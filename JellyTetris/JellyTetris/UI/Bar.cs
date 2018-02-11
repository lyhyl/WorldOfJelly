using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JellyTetris.UI
{
    public class Bar : Compositor
    {
        private Image background;
        private PureText ptext;
        private BarDraggable drag;

        public Bar(string bg,string text)
            : base(Vector2.Zero)
        {
            controlList.Add(background = new Image(bg));
            controlList.Add(ptext = new PureText());
            controlList.Add(drag = new BarDraggable(10, 20));

            Vector2 size = ptext.Font.MeasureString(text);
            ptext.Position = new Vector2((background.Width - size.X) * 0.5f, (background.Height - size.Y) * 0.5f);
            ptext.Text = text;
            ptext.AutoPosition = false;
            drag.image = ResourceManager.LoadImage("UI/Drag");
            drag.Position = new Vector2((background.Width - size.X) * 0.5f, (background.Height - size.Y) * 0.5f);
        }

        private class BarDraggable : Control
        {
            public Texture2D image;
            public int Width { get { return rectangle.Width; } set { rectangle.Width = value; } }
            public int Height { get { return rectangle.Height; } set { rectangle.Height = value; } }
            public bool LockXAxis = false;
            public bool LockYAxis = false;

            private Vector2 lastPos = new Vector2(-100, 0);
            private Rectangle rectangle = new Rectangle();
            private bool dragging = false;

            public BarDraggable(int w, int h)
            {
                Position = Vector2.Zero;
                rectangle.Width = w;
                rectangle.Height = h;
            }

            protected override void preportyReset(PreType pt)
            {
            }

            public override void Draw()
            {
                spriteBatch.Draw(image, Position, Color.White);
            }

            public override void HandleMouse()
            {
                rectangle.X = (int)Position.X;
                rectangle.Y = (int)Position.Y;

                if (ControlSystem.MouseLeftButtonPressed && ControlSystem.MouseInRectangle(rectangle))
                {
                    dragging = true;
                    if (lastPos.X == -100) lastPos = ControlSystem.MouseWorldPosition;
                }
                if (dragging)
                {
                    dragging = ControlSystem.MouseLeftButtonPressed;

                    Vector2 pos = ControlSystem.MouseWorldPosition;
                    Vector2 diff = pos - lastPos;
                    diff.Y = -diff.Y;
                    if (LockXAxis) diff.X = 0;
                    if (LockYAxis) diff.Y = 0;
                    Position += diff;
                    lastPos = pos;
                }
            }
        }
    }
}
