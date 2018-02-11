using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WOJ_IdealGas
{
    public class JellyVertex
    {
        private VectorF _position = new VectorF(0, 0);
        public VectorF Position { set { _position = value; } get { return _position; } }
        public float X { set { _position.X = value; } get { return _position.X; } }
        public float Y { set { _position.Y = value; } get { return _position.Y; } }

        private VectorF _velocity = new VectorF(0, 0);
        public VectorF Velocity { set { _velocity = value; } get { return _velocity; } }
        public float VX { set { _velocity.X = value; } get { return _velocity.X; } }
        public float VY { set { _velocity.Y = value; } get { return _velocity.Y; } }

        private VectorF _force = new VectorF(0, 0);
        public VectorF Force { set { _force = value; } get { return _force; } }
        public float FX { set { _force.X = value; } get { return _force.X; } }
        public float FY { set { _force.Y = value; } get { return _force.Y; } }

        public JellyVertex(float x, float y)
        {
            _position.X = x;
            _position.Y = y;
        }
    }
}
