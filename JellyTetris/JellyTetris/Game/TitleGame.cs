using JellyTetris.JellyWorld.Math;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;

namespace JellyTetris.Game
{
    public class TitleGame : BaseGame
    {
        private Video video;
        private VideoPlayer videoPlayer;

        public TitleGame()
        {
            RegisterMouseEvent();

            video = ResourceManager.LoadVideo("Video/Wildlife");
            videoPlayer = new VideoPlayer();
            videoPlayer.Play(video);
            exitToGame = GameType.MainMenu;
        }

        protected override void OnHandleMouse()
        {
        }

        protected override void OnMouseDown()
        {
            gameState = GameState.Exit;
        }

        protected override void OnMousePressed()
        {
        }

        protected override void OnMouseUp()
        {
        }

        public override void BeginRun()
        {
        }

        public override void Reinitialize()
        {
        }

        public override void DrawBackSprite()
        {
            spriteBatch.Draw(videoPlayer.GetTexture(), Vector2.Zero, Color.White);
        }

        public override void Draw()
        {
        }

        protected override void RunGame(GameTime gameTime)
        {
            if (videoPlayer.State == MediaState.Stopped)
                gameState = GameState.Exit;

            //JellyWorld.Jelly.JellyTriple db = new JellyWorld.Jelly.JellyTriple(ResourceManager.GraphicsDevice, 0, 50, Color.AliceBlue, new JellyVector2(100, 100));
        }

        protected override void EnterGame()
        {
        }

        protected override void ExitGame()
        {
            videoPlayer.Stop();
            UnregisterMouseEvent();
            base.ExitGame();
        }
    }
}
