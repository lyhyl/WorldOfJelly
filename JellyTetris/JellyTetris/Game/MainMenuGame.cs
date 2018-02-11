using System;
using JellyTetris.JellyWorld;
using JellyTetris.JellyWorld.Jelly;
using JellyTetris.JellyWorld.Math;
using JellyTetris.JellyWorld.Physic;
using JellyTetris.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JellyTetris.Game
{
    public class MainMenuGame : BaseGame
    {
        private const float jellyUnitMass = 0.5f;
        private const float jellyUnitSize = 35;

        private ImageLabel imageLabelClassical;
        private ImageLabel imageLabelTimeLimit;
        private ImageLabel imageLabelAbout;
        private ImageLabel imageLabelSetting;

        private Bar bgVolume;

        private int exitTime = 500;
        private int exitBeginTime;

        private int enterTime = 500;
        private int enterBeginTime;

        public MainMenuGame()
        {
            RegisterMouseEvent();

            UIManager.RemoveAll();

            bgVolume = new Bar("UI/IL_Setting", "Setting");
            //bgVolume.Position = new Vector2(0, 0);
            bgVolume.Enable = true;
            UIManager.Add(bgVolume);

            imageLabelClassical = new ImageLabel("UI/IL_Classical", Vector2.Zero);
            imageLabelClassical.Alpha = 0.9f;
            imageLabelAbout = new ImageLabel("UI/IL_About", Vector2.Zero);
            imageLabelAbout.Alpha = 0.9f;
            imageLabelSetting = new ImageLabel("UI/IL_Setting", Vector2.Zero);
            imageLabelSetting.Alpha = 0.9f;

            jellyObjectList.Add(new JellyBox(graphicsDevice, jellyUnitMass, jellyUnitSize, Color.Orange, new JellyVector2(-75, 150)));
            jellyObjectList.Add(new JellyBox(graphicsDevice, jellyUnitMass, jellyUnitSize, Color.Pink, new JellyVector2(75, 200)));
            jellyObjectList.Add(new JellyBox(graphicsDevice, jellyUnitMass, jellyUnitSize, Color.OrangeRed, new JellyVector2(-75, 250)));
            jellyObjectList.Add(new JellyBox(graphicsDevice, jellyUnitMass, jellyUnitSize, Color.Chocolate, new JellyVector2(75, 300)));
            
            LoadResource();
        }

        void btn_PressEvent(Button target)
        {
        }

        private void LoadResource()
        {
            backgroundImage = ResourceManager.LoadImage("Image/Background/TestBG");

            TransitionMarkImage = ResourceManager.LoadImage("Image/Transition/Transition_UpDown");
            TransitionImage = ResourceManager.LoadImage("Image/Background/JTBG00");

            TransitionInEffect = ResourceManager.LoadEffect("Effect/TransitionInFx");
            TransitionInEffect.Parameters[0].SetValue(TransitionMarkImage);
            TransitionInEffect.Parameters[2].SetValue(0.2f);
            TransitionInEffect.Parameters[3].SetValue(5.0f);

            TransitionOutEffect = ResourceManager.LoadEffect("Effect/TransitionOutFx");
            TransitionOutEffect.Parameters[0].SetValue(TransitionMarkImage);
            TransitionOutEffect.Parameters[2].SetValue(0.2f);
            TransitionOutEffect.Parameters[3].SetValue(5.0f);
        }

        protected override void RunGame(GameTime gameTime)
        {
            float timestep = (float)gameTime.ElapsedGameTime.TotalSeconds;

            #region Physic
            foreach (JellyObject jo in jellyObjectList)
                jo.Gravity();

            foreach (JellyObject jo in jellyObjectList)
                jo.Preservation();

            for (int i = 0; i < jellyObjectList.Count; ++i)
            {
                JellyObject joi = jellyObjectList[i];
                for (int j = i + 1; j < jellyObjectList.Count; ++j)
                {
                    JellyObject joj = jellyObjectList[j];
                    foreach (JellyVertex jv in joj.Edge)
                        JellyCollision.ProcessEdgeForce(joi.Edge, jv);
                    foreach (JellyVertex jv in joi.Edge)
                        JellyCollision.ProcessEdgeForce(joj.Edge, jv);
                }
            }

            foreach (JellyObject jo in jellyObjectList)
                jo.Integrate(timestep);
            #endregion

            foreach (JellyObject jo in jellyObjectList)
                jo.UpdateDraw();

            #region Check
            int type = 0;
            foreach (JellyObject jo in jellyObjectList)
            {
                foreach (JellyVertex jv in jo.Edge)
                    if (jv.Y > 300)
                        switch (type)
                        {
                            case 0://Classical
                                gameState = GameState.Exit;
                                exitBeginTime = Environment.TickCount;
                                exitToGame = GameType.Classical;
                                return;
                            case 1://Time Limit
                                gameState = GameState.Exit;
                                exitBeginTime = Environment.TickCount;
                                exitToGame = GameType.TimeLimit;
                                return;
                            case 2://About
                                About();
                                break;
                            case 3:
                                Setting();
                                break;
                        }
                ++type;
            }
            #endregion
        }

        private void About()
        {
        }

        private void Setting()
        {
        }

        protected override void EnterGame()
        {
            if (Environment.TickCount - enterBeginTime > enterTime + enterTime * 0.2)
                gameState = GameState.Run;
        }

        protected override void ExitGame()
        {
            if (Environment.TickCount - exitBeginTime > exitTime + exitTime * 0.2)
            {
                UnregisterMouseEvent();
                base.ExitGame();
            }
        }

        public override void BeginRun()
        {
            gameState = GameState.Enter;
            enterBeginTime = Environment.TickCount;
            imageLabelClassical.ResetClock();
            imageLabelAbout.ResetClock();
            imageLabelSetting.ResetClock();
        }

        public override void Reinitialize()
        {
            gameState = GameState.Enter;
            RegisterMouseEvent();
            /*jellyObjectList.Clear();
            jellyObjectList.Add(new JellyBox(graphicsDevice, jellyUnitMass, jellyUnitSize, Color.Orange, new JellyVector2(0, 150)));
            jellyObjectList.Add(new JellyBox(graphicsDevice, jellyUnitMass, jellyUnitSize, Color.Pink, new JellyVector2(75, 250)));
            jellyObjectList.Add(new JellyBox(graphicsDevice, jellyUnitMass, jellyUnitSize, Color.OrangeRed, new JellyVector2(-75, 200)));*/
        }

        public override void Draw()
        {
            Effect.VertexColorEnabled = false;
            Effect.TextureEnabled = true;
            foreach (JellyObject jo in jellyObjectList)
                jo.Draw();
        }

        public override void DrawFrontSprite()
        {
            base.DrawFrontSprite();

            bgVolume.Draw();
            imageLabelClassical.Draw();
            imageLabelAbout.Draw();
            imageLabelSetting.Draw();

            switch (gameState)
            {
                case GameState.Enter:
                    {
                        ResourceManager.EndDrawSprite();
                        float diff = Environment.TickCount - enterBeginTime;
                        diff /= enterTime;
                        ResourceManager.BeginDrawSpriteEx(TransitionInEffect);
                        TransitionInEffect.Parameters[1].SetValue(diff);
                        spriteBatch.Draw(TransitionImage, Vector2.Zero, Color.White);
                    }
                    break;
                case GameState.Exit:
                    {
                        ResourceManager.EndDrawSprite();
                        float diff = Environment.TickCount - exitBeginTime;
                        diff /= exitTime;
                        ResourceManager.BeginDrawSpriteEx(TransitionOutEffect);
                        TransitionOutEffect.Parameters[1].SetValue(diff);
                        spriteBatch.Draw(TransitionImage, Vector2.Zero, Color.White);
                    }
                    break;
            }
        }

        protected override void OnMouseDown()
        {
            float u = -1, v = -1, w = -1;
            int count = 0;
            selectObjID = int.MaxValue;
            foreach (JellyObject jo in jellyObjectList)
            {
                if (jo.Select(ControlSystem.MouseWorldPosition, out selectTriID, out u, out v, out w))
                {
                    selectObjID = count;
                    break;
                }
                ++count;
            }
            selectbary.A = u;
            selectbary.B = v;
            selectbary.C = w;
        }

        protected override void OnMousePressed()
        {
            if (selectObjID != int.MaxValue)
                jellyObjectList[selectObjID].Catch(selectTriID, selectbary.A, selectbary.B, selectbary.C, ControlSystem.MouseWorldPosition);
        }

        protected override void OnMouseUp()
        {
        }

        protected override void OnHandleMouse()
        {
            float u, v, w;
            int triid;
            int type = 0;
            foreach (JellyObject jo in jellyObjectList)
            {
                if (jo.Select(ControlSystem.MouseWorldPosition, out triid, out u, out v, out w))
                {
                    switch (type)
                    {
                        case 0:
                            imageLabelClassical.Position = ControlSystem.MouseScreenPosition;
                            imageLabelClassical.Enable = true;
                            break;
                        case 1:
                            break;
                        case 2:
                            imageLabelAbout.Position = ControlSystem.MouseScreenPosition;
                            imageLabelAbout.Enable = true;
                            break;
                        case 3:
                            imageLabelSetting.Position = ControlSystem.MouseScreenPosition;
                            imageLabelSetting.Enable = true;
                            break;
                    }
                    return;
                }
                ++type;
            }
            imageLabelClassical.Enable = false;
            imageLabelAbout.Enable = false;
            imageLabelSetting.Enable = false;
        }
    }
}
