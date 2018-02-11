using System;
using JellyTetris.Game;
using JellyTetris.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace JellyTetris
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class JellyTetris : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;

        BaseGame currentGame;

        TitleGame titleGame;
        MainMenuGame mainMenuGame;
        ClassicalGame classicalGame;

        Texture2D mouseImg, mouseDownImg;

        public JellyTetris()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 500;
            graphics.PreferredBackBufferHeight = 700;
            graphics.PreferMultiSampling = true;

            Content.RootDirectory = "Resource";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //IsMouseVisible = true;

            ControlSystem.Initialize(GraphicsDevice);
            ControlSystem.HandleInFormOnly = true;
            ControlSystem.EffectVolume = Settings.Default.EffectVolume;
            ControlSystem.BackgroundVolume = Settings.Default.BackgroundVolume;

            ResourceManager.Initialize(GraphicsDevice, Content);

            mouseImg = ResourceManager.LoadImage("Cursor/Cursor");
            mouseDownImg = ResourceManager.LoadImage("Cursor/CursorDown");

            GraphicsDevice.DeviceReset += new EventHandler<EventArgs>(GraphicsDevice_DeviceReset);

            titleGame = new TitleGame();
            titleGame.SwitchGameEventHandler = ChooseGame;

            mainMenuGame = new MainMenuGame();
            mainMenuGame.SwitchGameEventHandler = ChooseGame;

            classicalGame = new ClassicalGame();
            classicalGame.SwitchGameEventHandler = ChooseGame;

            currentGame = titleGame;

            base.Initialize();
        }

        void GraphicsDevice_DeviceReset(object sender, EventArgs e)
        {
            SamplerState ss = new SamplerState();
            ss.AddressU = TextureAddressMode.Clamp;
            ss.AddressV = TextureAddressMode.Clamp;
            for (int i = 1; i < 2; ++i)//max number of textures usedin the same time ( now is 2 )
                GraphicsDevice.SamplerStates[i] = ss;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            /*if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();*/

            ControlSystem.Update();
            UIManager.HandleMouse();

            KeyboardState kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(Keys.Escape))
                this.Exit();

            if (currentGame.Suspending)
                currentGame.BeginRun();
            currentGame.Run(gameTime);

            base.Update(gameTime);
        }

        private void ChooseGame(GameType type)
        {
            switch (type)
            {
                case GameType.MainMenu:
                    if (currentGame != mainMenuGame)
                        currentGame = mainMenuGame;
                    break;
                case GameType.Classical:
                    if (currentGame != classicalGame)
                        currentGame = classicalGame;
                    break;
                case GameType.TimeLimit:
                    break;
            }
            currentGame.Reinitialize();
            currentGame.BeginRun();
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            ResourceManager.BeginDrawSprite();
            currentGame.DrawBackSprite();
            ResourceManager.EndDrawSprite();

            GraphicsDevice.RasterizerState = RasterizerState.CullNone;

            ResourceManager.BeginDrawSprite();
            currentGame.Draw();
            ResourceManager.EndDrawSprite();

            ResourceManager.BeginDrawSprite();
            currentGame.DrawFrontSprite();
            ResourceManager.EndDrawSprite();
            //it may use EX
            //the curosr will use the effect too
            //I don't want that
            ResourceManager.BeginDrawSprite();
            ResourceManager.SpriteBatch.Draw(
                ControlSystem.MouseLeftButtonPressed ? mouseDownImg : mouseImg,
                ControlSystem.MouseScreenPosition,
                Color.White);
            ResourceManager.EndDrawSprite();

            base.Draw(gameTime);
        }

        protected override void OnExiting(object sender, EventArgs args)
        {
            Settings.Default.EffectVolume = ControlSystem.EffectVolume;
            Settings.Default.BackgroundVolume = ControlSystem.BackgroundVolume;
            Settings.Default.Save();

            base.OnExiting(sender, args);
        }
    }
}
