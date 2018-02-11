using System.Collections.Generic;
using JellyTetris.JellyWorld.Jelly;
using JellyTetris.JellyWorld.Math;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace JellyTetris.Game
{
    public abstract class BaseGame
    {
        protected enum GameState { Suspend, Enter, Run, Exit };

        public delegate void SwitchGame(GameType type);
        public SwitchGame SwitchGameEventHandler { set; get; }

        public BasicEffect Effect { set; get; }

        protected GameType exitToGame = GameType.MainMenu;
        protected GameState gameState = GameState.Suspend;
        public bool Suspending { get { return gameState == GameState.Suspend; } }
        public bool Entering { get { return gameState == GameState.Enter; } }
        public bool Running { get { return gameState == GameState.Run; } }
        public bool Exting { get { return gameState == GameState.Exit; } }

        protected Effect TransitionInEffect;
        protected Effect TransitionOutEffect;
        protected Texture2D TransitionImage;
        protected Texture2D TransitionMarkImage;

        protected GraphicsDevice graphicsDevice;
        protected SpriteBatch spriteBatch;

        protected Texture2D backgroundImage;
        protected List<JellyObject> jellyObjectList = new List<JellyObject>();

        protected int hoverObjID = int.MaxValue;
        protected int selectObjID = int.MaxValue;
        protected int selectTriID = int.MaxValue;
        protected Triple<float> selectbary = new Triple<float>();

        protected BaseGame()
        {
            graphicsDevice = ResourceManager.GraphicsDevice;
            spriteBatch = ResourceManager.SpriteBatch;
            Effect = ResourceManager.DefaultEffect;
        }

        protected abstract void OnHandleMouse();
        protected abstract void OnMouseDown();
        protected abstract void OnMousePressed();
        protected abstract void OnMouseUp();

        protected void RegisterMouseEvent()
        {
            ControlSystem.MouseMainHandler += OnHandleMouse;
            ControlSystem.MouseDownHandler += OnMouseDown;
            ControlSystem.MousePressedHandler += OnMousePressed;
            ControlSystem.MouseUpHandler += OnMouseUp;
        }

        protected void UnregisterMouseEvent()
        {
            ControlSystem.MouseMainHandler -= OnHandleMouse;
            ControlSystem.MouseDownHandler -= OnMouseDown;
            ControlSystem.MousePressedHandler -= OnMousePressed;
            ControlSystem.MouseUpHandler -= OnMouseUp;
        }

        public abstract void BeginRun();
        public void Run(GameTime gameTime)
        {
            switch (gameState)
            {
                case GameState.Enter:
                    EnterGame();
                    break;
                case GameState.Exit:
                    ExitGame();
                    break;
            }
            RunGame(gameTime);
        }
        public virtual void Reinitialize()
        {
            RegisterMouseEvent();
        }
        public abstract void Draw();

        protected abstract void RunGame(GameTime gameTime);
        protected abstract void EnterGame();
        protected virtual void ExitGame()
        {
            SwitchGameEventHandler(exitToGame);
        }

        public virtual void DrawBackSprite()
        {
            spriteBatch.Draw(backgroundImage, Vector2.Zero, Color.White);
        }
        public virtual void DrawFrontSprite()
        {
        }
    }
}
