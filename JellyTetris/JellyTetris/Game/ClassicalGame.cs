using System;
using System.Collections.Generic;
using JellyTetris.JellyWorld;
using JellyTetris.JellyWorld.Jelly;
using JellyTetris.JellyWorld.Math;
using JellyTetris.JellyWorld.Physic;
using JellyTetris.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;

namespace JellyTetris.Game
{
    public class ClassicalGame : BaseGame
    {
        private const float unitMass = 1;
        private const float unitSize = 30;

        private int exitTime = 500;
        private int exitBeginTime;

        private int enterTime = 500;
        private int enterBeginTime;

        private bool freezing = false;
        private int freezeBeginTime;
        private int freezingTime = 1000;

        private int playTime;
        private int roundCount = 0;
        private int prepareTime = 4000;

        private PureText msg;
        private Random random = new Random();

        private Song backgroundMusic;

        private Color[] colorTab =
        {
            Color.Orange, Color.OrangeRed, Color.Pink,
            Color.Purple, Color.LightBlue, Color.Chocolate,
            Color.Gold, Color.HotPink
        };

        private const int linesCount = 11;
        private const float threshold = unitSize * 0.5f;
        private const float lineSep = unitSize * 2 + 10;
        private float[] lineTab;
        private HashSet<Pair<JellyObject, int>>[] boxesInfo;
        private Dictionary<JellyObject, LinkedList<int>> clearTarget = new Dictionary<JellyObject, LinkedList<int>>();
        private bool needClear = false;

        public ClassicalGame()
        {
            RegisterMouseEvent();
            LoadResource();

            msg = new PureText(ResourceManager.TextFontLarge);
            msg.Height = ControlSystem.SpaceHeight;
            msg.Width = ControlSystem.SpaceWidth;
            msg.TransitionTime = 250;
            msg.AutoPosition = true;
            msg.Enable = false;

            lineTab = new float[linesCount];
            lineTab[0] = lineSep * 0.5f;
            for (int i = 1; i < linesCount; ++i)
                lineTab[i] = lineTab[i - 1] + lineSep;

            boxesInfo = new HashSet<Pair<JellyObject, int>>[linesCount];
            for (int i = 0; i < linesCount; ++i)
                boxesInfo[i] = new HashSet<Pair<JellyObject, int>>();
        }

        private void LoadResource()
        {
            backgroundMusic = ResourceManager.LoadSong("Sound/BackgroundMusic/Peter Ilyich Tchaikovsky-Nutcracker- Dance of the Sugar-Plum Fairy");

            backgroundImage = ResourceManager.LoadImage("Image/Background/TestBG");

            TransitionMarkImage = ResourceManager.LoadImage("Image/Transition/Transition_Twirl");
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

        protected override void OnHandleMouse()
        {
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

        protected override void OnMouseUp()
        {
        }

        protected override void OnMousePressed()
        {
            if (selectObjID < jellyObjectList.Count)
                jellyObjectList[selectObjID].Catch(selectTriID, selectbary.A, selectbary.B, selectbary.C, ControlSystem.MouseWorldPosition);
        }

        protected override void EnterGame()
        {
            int ticks = Environment.TickCount;
            if (ticks - enterBeginTime > enterTime + enterTime * 0.2)
            {
                gameState = GameState.Run;
                playTime = ticks;
                msg.ResetClock();
            }
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
            MediaPlayer.Play(backgroundMusic);
            MediaPlayer.IsRepeating = true;
        }

        protected override void RunGame(GameTime gameTime)
        {
            foreach (HashSet<Pair<JellyObject, int>> bi in boxesInfo)
                bi.Clear();

            int tick = Environment.TickCount;
            if (!freezing)
            {
                if (gameState == GameState.Run)
                {
                    if (tick - playTime > prepareTime)
                    {
                        playTime = tick;
                        ++roundCount;

                        GenerateNextJelly();
                    }
                    if (needClear)
                    {
                        ClearLine();
                        needClear = false;
                    }
                    CheckJellyOnLine();
                    HandleJellyOnLine();
                }

                Update(gameTime);
            }
        }

        private void HandleJellyOnLine()
        {
            foreach (HashSet<Pair<JellyObject, int>> n in boxesInfo)
            {
                if (n.Count >= 6)
                {
                    SetClearLine(n);

                    //stop catching
                    selectObjID = int.MaxValue;

                    //freeze the scene
                    freezing = true;
                    freezeBeginTime = Environment.TickCount;
                    msg.ResetClock();
                }
            }
        }

        private void ClearLine()
        {
            foreach (JellyObject jo in clearTarget.Keys)
            {
                LinkedList<int> list = clearTarget[jo];
                int[] idlist = new int[list.Count];
                list.CopyTo(idlist, 0);
                jellyObjectList.Remove(jo);
                JellyObject[] jos = jo.Convert(idlist);
                if (jos != null)
                    jellyObjectList.AddRange(jos);
            }

            //clear up for next
            clearTarget.Clear();
        }

        private void SetClearLine(HashSet<Pair<JellyObject, int>> line)
        {
            foreach (Pair<JellyObject, int> pjoid in line)
            {
                if (!clearTarget.ContainsKey(pjoid.A))
                    clearTarget.Add(pjoid.A, new LinkedList<int>());
                clearTarget[pjoid.A].AddLast(pjoid.B);
            }
        }

        private void CheckJellyOnLine()
        {
            foreach (JellyObject jo in jellyObjectList)
                foreach (int jvid in jo.Cells)
                {
                    float yp = jo.Nodes[jvid].Position.Y - JellyWorld.JellyWorld.WorldBottom;
                    int lid = (int)(yp / lineSep);
                    lid = lid < 0 ? 0 : lid;
                    if (Math.Abs(lineTab[lid] - yp) < threshold)
                        boxesInfo[lid].Add(new Pair<JellyObject, int>(jo, jvid));
                }
        }

        private void Update(GameTime gameTime)
        {
            float timestep = (float)gameTime.ElapsedGameTime.TotalSeconds;

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

            foreach (JellyObject jo in jellyObjectList)
                jo.UpdateDraw();
        }

        private void GenerateNextJelly()
        {
            JellyObject jo = null;
            const float height = 350;
            switch (random.Next(6))
            {
                    /*jo = new JellyBox(graphicsDevice, unitMass, unitSize,
                        colorTab[random.Next(colorTab.Length)], new JellyVector2(0, height));
                    break;
                    jo = new JellyDouble(graphicsDevice, unitMass, unitSize,
                        colorTab[random.Next(colorTab.Length)], new JellyVector2(0, height));
                    break;*/
                case 0:
                    jo = new JellyTripleL(graphicsDevice, unitMass, unitSize,
                        colorTab[random.Next(colorTab.Length)], new JellyVector2(0, height));
                    break;
                case 1:
                    jo = new JellyBigBox(graphicsDevice, unitMass, unitSize,
                        colorTab[random.Next(colorTab.Length)], new JellyVector2(0, height));
                    break;
                case 2:
                    jo = new JellyTShape(graphicsDevice, unitMass, unitSize,
                        colorTab[random.Next(colorTab.Length)], new JellyVector2(0, height));
                    break;
                case 3:
                    jo = new JellyZShape(graphicsDevice, unitMass, unitSize,
                        colorTab[random.Next(colorTab.Length)], new JellyVector2(0, height));
                    break;
                case 4:
                    jo = new JellyLong(graphicsDevice, unitMass, unitSize,
                        colorTab[random.Next(colorTab.Length)], new JellyVector2(0, height));
                    break;
                case 5:
                    jo = new JellyLShape(graphicsDevice, unitMass, unitSize,
                        colorTab[random.Next(colorTab.Length)], new JellyVector2(0, height));
                    break;
            }
            jellyObjectList.Add(jo);
        }

        public override void Reinitialize()
        {
            RegisterMouseEvent();
        }

        public override void Draw()
        {
            //Effect.VertexColorEnabled = true;
            Effect.TextureEnabled = true;
            foreach (JellyObject jo in jellyObjectList)
                jo.Draw();
        }

        public override void DrawFrontSprite()
        {
            base.DrawFrontSprite();

            int tick = Environment.TickCount;

            switch (gameState)
            {
                case GameState.Enter:
                    {
                        ResourceManager.EndDrawSprite();
                        float diff = tick - enterBeginTime;
                        diff /= enterTime;
                        ResourceManager.BeginDrawSpriteEx(TransitionInEffect);
                        TransitionInEffect.Parameters[1].SetValue(diff);
                        spriteBatch.Draw(TransitionImage, Vector2.Zero, Color.White);
                    }
                    break;
                case GameState.Run:
                    {
                        if (roundCount == 0)
                            switch (((tick - playTime) << 2) / prepareTime)
                            {
                                case 0: showMsg("3"); break;
                                case 1: showMsg("2"); break;
                                case 2: showMsg("1"); break;
                                case 3: showMsg("Start !"); break;
                            }

                        if (freezing)
                            if (tick - freezeBeginTime > freezingTime)
                            {
                                freezing = false;
                                needClear = true;
                                msg.Text = "";
                            }
                            else
                                showMsg("Great!");
                    }
                    break;
                case GameState.Exit:
                    {
                        ResourceManager.EndDrawSprite();
                        float diff = tick - exitBeginTime;
                        diff /= exitTime;
                        ResourceManager.BeginDrawSpriteEx(TransitionOutEffect);
                        TransitionOutEffect.Parameters[1].SetValue(diff);
                        spriteBatch.Draw(TransitionImage, Vector2.Zero, Color.White);
                    }
                    break;
            }
        }

        private void showMsg(string m)
        {
            if (msg.Text != m)
            {
                msg.Text = m;
                msg.Enable = true;
            }
            msg.Draw();
        }
    }
}
