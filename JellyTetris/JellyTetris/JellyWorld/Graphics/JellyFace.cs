using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JellyTetris.JellyWorld.Graphics
{
    public class JellyFace
    {
        private Texture2D texture;
        private int height, width;

        public int Frame = -1;
        public Vector2 Position;
        public float Rotation;

        public JellyFace(string img, int w)
        {
            Position = Vector2.Zero;

            texture = ResourceManager.LoadImage(img);
            height = texture.Height;
            width = w;
        }

        public void Draw()
        {
            if (Frame == -1)
                return;
            int dx = Frame * width;
            ResourceManager.SpriteBatch.Draw(
                texture,
                new Rectangle((int)Position.X + dx, (int)Position.Y, width, height),
                new Rectangle(dx, 0, width, height),
                Color.White,
                Rotation,
                new Vector2(dx + (width >> 1), height >> 1),
                SpriteEffects.None, 0);
        }
    }
}
