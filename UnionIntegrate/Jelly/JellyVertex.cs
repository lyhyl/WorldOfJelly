//#define _RK2_
#define _Euler_

namespace UnionIntegrate
{
    public class JellyVertex
    {
        private Vector _position = new Vector(0, 0);
        public Vector Position { set { _position = value; } get { return _position; } }
        public double X { set { _position.X = value; } get { return _position.X; } }
        public double Y { set { _position.Y = value; } get { return _position.Y; } }

        private Vector _velocity = new Vector(0, 0);
        public Vector Velocity { set { _velocity = value; } get { return _velocity; } }
        public double VX { set { _velocity.X = value; } get { return _velocity.X; } }
        public double VY { set { _velocity.Y = value; } get { return _velocity.Y; } }

        private Vector _force = new Vector(0, 0);
        private Vector Force { set { _force = value; } get { return _force; } }
        public Vector GetForce() { return _force; }
        private double FX { set { _force.X = value; } get { return _force.X; } }
        private double FY { set { _force.Y = value; } get { return _force.Y; } }

        private Vector _nxt_force = new Vector(0, 0);
        public Vector NxtForce { set { _nxt_force = value; } get { return _nxt_force; } }
        public double NxtFX { set { _nxt_force.X = value; } get { return _nxt_force.X; } }
        public double NxtFY { set { _nxt_force.Y = value; } get { return _nxt_force.Y; } }

        public JellyVertex(JellyVertex jv)
        {
            _force = jv._force;
            _position = jv._position;
            _velocity = jv._velocity;

            _force = jv._force;
            _nxt_force = jv._nxt_force;
        }
        public JellyVertex(double x, double y)
        {
            //_force = new Vector(0, JellyWorld.DefUnitMass * JellyWorld.Gravity);

            _position.X = x;
            _position.Y = y;
        }
        public JellyVertex(Vector pos)
        {
            //_force = new Vector(0, JellyWorld.DefUnitMass * JellyWorld.Gravity);

            _position = pos;
        }

        public override string ToString()
        {
            return _position.ToString();
        }

        public void Integrate(double delay, double mass)
        {
#if _Euler_
            EulerIntegrate(delay, mass);
#endif
#if _RK2_
            RK2Integrate(delay, mass);
#endif

            //clear up
            _nxt_force.X = _nxt_force.Y = 0;
        }

        private void RK2Integrate(double delay, double mass)
        {
            Vector a1 = Force / mass;
            Vector p2 = Position + Velocity * delay;
            Vector v2 = Velocity + a1 * delay;
            Vector a2 = NxtForce / mass;

            Position += (Velocity + v2) * (0.5 * delay);
            Velocity += (a1 + a2) * (0.5 * delay);

            /*double hdelay_div_mass = 0.5 * delay / mass;

            Position += (Velocity + Force * hdelay_div_mass) * delay;
            Velocity += (Force + NxtForce) * hdelay_div_mass;*/

            //Force = NxtForce;
            Force = (Force + NxtForce) * 0.5;
        }

        private void EulerIntegrate(double delay, double mass)
        {
            Velocity += NxtForce * (delay / mass);
            //Limit Veloctiy Maximum
            if (Velocity.LengthSq > JellyWorld.MaximumVelocitySq)
                Velocity.Limit(JellyWorld.MaximumVelocity);
            Position += Velocity * delay;
        }
    }
}
