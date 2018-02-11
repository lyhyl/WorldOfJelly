using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace JellyTetris
{
    public static class ControlSystem
    {
        public static float GlobalVolume
        {
            set
            {
                EffectVolume = value;
                BackgroundVolume = value;
            }
        }
        public static float EffectVolume { set { SoundEffect.MasterVolume = value; } get { return SoundEffect.MasterVolume; } }
        public static float BackgroundVolume { set { MediaPlayer.Volume = value; } get { return MediaPlayer.Volume; } }

        private static GraphicsDevice graphicsDevice;
        private static MouseState currentMouseState;
        private static MouseState lastMouseState;

        public static MouseState CurrentMouseState { get { return currentMouseState; } }
        public static MouseState LastMouseState { get { return lastMouseState; } }

        public delegate void MouseHandler();
        public static event MouseHandler MousePressedHandler;
        public static event MouseHandler MouseReleasedHandler;
        public static event MouseHandler MouseDownHandler;
        public static event MouseHandler MouseUpHandler;

        public static event MouseHandler MouseMainHandler;

        public static event MouseHandler MouseMoveHandler;
        public static event MouseHandler MouseDragHandler;
        public static event MouseHandler MouseStayHandler;

        private static bool leftMarked = false;

        public static void Initialize(GraphicsDevice gd)
        {
            graphicsDevice = gd;
        }

        public static void Update()
        {
            lastMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

            if (MouseMainHandler != null)
                MouseMainHandler();
            if (HandleInFormOnly)
            {
                if (MouseInForm)
                    HandleMouseEvent();
            }
            else
                HandleMouseEvent();
        }

        private static void HandleMouseEvent()
        {
            if (currentMouseState.X != lastMouseState.X || currentMouseState.Y != lastMouseState.Y)
            {
                if (MouseMoveHandler != null)
                    MouseMoveHandler();
                if (MouseLeftButtonPressed)
                    if (MouseDragHandler != null)
                        MouseDragHandler();
            }
            else if (MouseStayHandler != null)
                MouseStayHandler();
            if (MouseLeftButtonPressed)
            {
                if (!leftMarked)
                {
                    if (MouseDownHandler != null)
                        MouseDownHandler();
                    leftMarked = true;
                }
                if (MousePressedHandler != null)
                    MousePressedHandler();
            }
            else
            {
                if (leftMarked)
                {
                    if (MouseUpHandler != null)
                        MouseUpHandler();
                    leftMarked = false;
                }
                if (MouseReleasedHandler != null)
                    MouseReleasedHandler();
            }
        }

        public static bool MouseLeftButtonPressed { get { return currentMouseState.LeftButton == ButtonState.Pressed; } }
        public static bool MouseLeftButtonReleased { get { return currentMouseState.LeftButton == ButtonState.Released; } }
        public static bool MouseRightButtonPressed { get { return currentMouseState.RightButton == ButtonState.Pressed; } }
        public static bool MouseRightButtonReleased { get { return currentMouseState.RightButton == ButtonState.Released; } }
        public static bool MouseMiddleButtonPressed { get { return currentMouseState.MiddleButton == ButtonState.Pressed; } }
        public static bool MouseMiddleButtonReleased { get { return currentMouseState.MiddleButton == ButtonState.Released; } }

        //public static bool MouseWheelScrollUp { get { return currentMouseState.ScrollWheelValue > 0; } } TODO

        public static int SpaceWidth { get { return graphicsDevice == null ? 0 : graphicsDevice.Viewport.Width; } }
        public static int SpaceHeight { get { return graphicsDevice == null ? 0 : graphicsDevice.Viewport.Height; } }

        public static int MouseWorldX { get { return currentMouseState.X - (graphicsDevice.Viewport.Width >> 1); } }
        public static int MouseWorldY { get { return (graphicsDevice.Viewport.Height >> 1) - currentMouseState.Y; } }
        public static JellyWorld.Math.JellyVector2 MouseWorldPosition
        {
            get { return new JellyWorld.Math.JellyVector2(ControlSystem.MouseWorldX, ControlSystem.MouseWorldY); }
        }

        public static int MouseScreenX { get { return currentMouseState.X; } }
        public static int MouseScreenY { get { return currentMouseState.Y; } }
        public static JellyWorld.Math.JellyVector2 MouseScreenPosition
        {
            get { return new JellyWorld.Math.JellyVector2(currentMouseState.X, currentMouseState.Y); }
        }

        public static bool HandleInFormOnly { set; get; }
        public static bool MouseInForm
        {
            get
            {
                return currentMouseState.X > 0 &&
                    currentMouseState.X < SpaceWidth &&
                    currentMouseState.Y > 0 &&
                    currentMouseState.Y < SpaceHeight;
            }
        }

        public static bool MouseInRectangle(Rectangle rect)
        {
            return currentMouseState.X > rect.Left &&
                currentMouseState.X < rect.Right &&
                currentMouseState.Y > rect.Top &&
                currentMouseState.Y < rect.Bottom;
        }
    }
}