using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JellyTetris.UI
{
    public abstract class Control
    {
        protected Control()
        {
            spriteBatch = ResourceManager.SpriteBatch;
            graphicsDevice = ResourceManager.GraphicsDevice;
        }

        private bool enable;
        public bool Enable { set { enable = value; preportyReset(PreType.Enable); } get { return enable; } }
        private float alpha = 1;
        public float Alpha { set { alpha = value; preportyReset(PreType.Alpha); } get { return alpha; } }
        private float transtime = 250;
        public float TransitionTime { set { transtime = value; preportyReset(PreType.TranTime); } get { return transtime; } }
        private Vector2 position = Vector2.Zero;
        public Vector2 Position { set { position = value; preportyReset(PreType.Position); } get { return position; } }

        protected enum PreType { Enable, Alpha, TranTime, Position, Region };
        protected abstract void preportyReset(PreType pt);

        protected SpriteBatch spriteBatch;
        protected GraphicsDevice graphicsDevice;
        protected int lasttime;

        public void ResetClock()
        {
            lasttime = Environment.TickCount;
        }

        public abstract void Draw();
        public abstract void HandleMouse();
    }
}
