using JellyTetris.JellyWorld.Math;
using Microsoft.Xna.Framework;

namespace JellyTetris.JellyWorld
{
    public class JellyVertex
    {
        public JellyVector2 Position = JellyVector2.Zero;
        public float X { set { Position.X = value; } get { return Position.X; } }
        public float Y { set { Position.Y = value; } get { return Position.Y; } }
        public JellyVector2 Velocity = JellyVector2.Zero;
        public float VX { set { Velocity.X = value; } get { return Velocity.X; } }
        public float VY { set { Velocity.Y = value; } get { return Velocity.Y; } }
        public JellyVector2 Force = JellyVector2.Zero;
        public float FX { set { Force.X = value; } get { return Force.X; } }
        public float FY { set { Force.Y = value; } get { return Force.Y; } }

        public JellyVertex(JellyVertex jv)
        {
            Position = jv.Position;
            Velocity = jv.Velocity;
            Force = jv.Force;
        }
        public JellyVertex(float x, float y)
        {
            Position.X = x;
            Position.Y = y;
        }
        public JellyVertex(Vector2 pos)
        {
            Position = new JellyVector2(pos.X, pos.Y);
        }
        public JellyVertex(JellyVector2 pos)
        {
            Position = pos;
        }

        internal void Integrate(float delay, float mass)
        {
            Velocity += Force * (delay / mass);
            //Limit Veloctiy Maximum
            if (Velocity.LengthSquared() > JellyWorld.MaximumVelocitySq)
            {
                Velocity.Normalize();
                Velocity *= JellyWorld.MaximumVelocity;
            }
            Position += Velocity * delay;

            FX = FY = 0;
        }

        internal void Restrict()
        {
            if (Position.Y < JellyWorld.WorldBottom)
            {
                VY = -VY * JellyWorld.Friciton;
                Y = JellyWorld.WorldBottom;
                VX *= JellyWorld.Friciton;
            }
            if (Position.Y > JellyWorld.WorldTop)
            {
                VY = 0;
                Y = JellyWorld.WorldTop;
                VX *= JellyWorld.Friciton;
            }
            if (Position.X < JellyWorld.WorldLeft)
            {
                VX = -VX * JellyWorld.Friciton;
                X = JellyWorld.WorldLeft;
                VY *= JellyWorld.Friciton;
            }
            if (Position.X > JellyWorld.WorldRight)
            {
                VX = -VX * JellyWorld.Friciton;
                X = JellyWorld.WorldRight;
                VY *= JellyWorld.Friciton;
            }
        }

        public override string ToString()
        {
            return Position.ToString();
        }
    }
}
