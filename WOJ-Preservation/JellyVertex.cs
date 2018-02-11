using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnionPreColl
{
    public class JellyVertex
    {
        private Vector _position = new Vector(0, 0);
        public Vector Position { set { _position = value; } get { return _position; } }
        public float X { set { _position.X = value; } get { return _position.X; } }
        public float Y { set { _position.Y = value; } get { return _position.Y; } }

        private Vector _velocity = new Vector(0, 0);
        public Vector Velocity { set { _velocity = value; } get { return _velocity; } }
        public float VX { set { _velocity.X = value; } get { return _velocity.X; } }
        public float VY { set { _velocity.Y = value; } get { return _velocity.Y; } }

        private Vector _force = new Vector(0, 0);
        public Vector Force { set { _force = value; } get { return _force; } }
        public float FX { set { _force.X = value; } get { return _force.X; } }
        public float FY { set { _force.Y = value; } get { return _force.Y; } }

        public JellyVertex(float x, float y)
        {
            _position.X = x;
            _position.Y = y;
        }
        public JellyVertex(Vector pos)
        {
            _position = pos;
        }
    }
}
