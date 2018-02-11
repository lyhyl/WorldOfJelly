using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace JellyTetris
{
    public static class ResourceManager
    {
        private static ContentManager contentManager = null;
        public static ContentManager Content { get { return contentManager; } }

        private static GraphicsDevice graphicsDevice = null;
        public static GraphicsDevice GraphicsDevice { get { return graphicsDevice; } }

        private static SpriteBatch spriteBatch;
        public static SpriteBatch SpriteBatch { get { return spriteBatch; } }

        private static BasicEffect basicEffect;
        public static BasicEffect DefaultEffect { get { return basicEffect; } }

        private static SpriteFont textFontS;
        public static SpriteFont TextFontSmall { get { return textFontS; } }
        private static SpriteFont textFontM;
        public static SpriteFont TextFontMiddle { get { return textFontM; } }
        private static SpriteFont textFontL;
        public static SpriteFont TextFontLarge { get { return textFontL; } }

        private static Dictionary<string, SoundEffect> sounds = new Dictionary<string, SoundEffect>();

        public static void Initialize(GraphicsDevice device, ContentManager content)
        {
            graphicsDevice = device;
            contentManager = content;

            if (spriteBatch != null)
                spriteBatch.Dispose();
            spriteBatch = new SpriteBatch(graphicsDevice);

            if (basicEffect != null)
                basicEffect.Dispose();
            basicEffect = new BasicEffect(GraphicsDevice);
            basicEffect.DiffuseColor = Vector3.One;
            basicEffect.World = Matrix.Identity;
            basicEffect.View = Matrix.Identity;
            basicEffect.Projection = Matrix.CreateOrthographic(
                GraphicsDevice.Viewport.Width,
                GraphicsDevice.Viewport.Height,
                0, 1);

            textFontS = content.Load<SpriteFont>("Font/FontA");
            textFontM = content.Load<SpriteFont>("Font/FontB");
            textFontL = content.Load<SpriteFont>("Font/FontC");
        }

        public static Texture2D LoadImage(string name)
        {
            return contentManager.Load<Texture2D>(name);
        }
        public static Video LoadVideo(string name)
        {
            return contentManager.Load<Video>(name);
        }
        public static Song LoadSong(string name)
        {
            return contentManager.Load<Song>(name);
        }
        public static SoundEffectInstance LoadVoice(string name)
        {
            if (!sounds.ContainsKey(name))
                sounds[name] = contentManager.Load<SoundEffect>(name);
            return sounds[name].CreateInstance();
        }
        public static Effect LoadEffect(string name)
        {
            return contentManager.Load<Effect>(name);
        }
        public static Effect LoadSetupEffect(string name)
        {
            Effect effect = contentManager.Load<Effect>(name);
            effect.Parameters[0].SetValue(Matrix.Identity * Matrix.CreateOrthographic(
                GraphicsDevice.Viewport.Width,
                GraphicsDevice.Viewport.Height,
                0, 1));
            return effect;
        }

        public static void BeginDrawSprite()
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
        }
        public static void EndDrawSprite()
        {
            spriteBatch.End();
        }
        public static void BeginDrawSpriteEx(Effect effect)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred,
                BlendState.NonPremultiplied,
                SamplerState.LinearClamp,
                DepthStencilState.None,
                RasterizerState.CullNone,
                effect);
        }
    }
}
