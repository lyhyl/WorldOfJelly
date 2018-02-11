using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace UnionPreColl
{
    public class JellyBaseTriangle
    {
        private JellyVertex va, vb, vc;
        private DisPreLink disprelinkAB, disprelinkBC, disprelinkCA;
        private ArePreLink areprelink;
        public float DoubleArea { get { return areprelink.DoubleArea; } }
        public float Mass { set; get; }

        public JellyBaseTriangle(JellyVertex a, JellyVertex b, JellyVertex c, float mass)
        {
            disprelinkAB = new DisPreLink(a, b, 4000, 500);
            disprelinkBC = new DisPreLink(b, c, 4000, 500);
            disprelinkCA = new DisPreLink(c, a, 4000, 500);
            areprelink = new ArePreLink(a, b, c, 1000000, disprelinkAB.PosNormal, disprelinkBC.PosNormal, disprelinkCA.PosNormal);
            va = a;
            vb = b;
            vc = c;
            Mass = mass;
        }

        public void Update(float delay)
        {
            Gravity();
            Preservation();
            Integrate(delay);
        }

        private void Integrate(float delay)
        {
            va.Velocity += va.Force * (delay / Mass);
            vb.Velocity += vb.Force * (delay / Mass);
            vc.Velocity += vc.Force * (delay / Mass);

            va.FX = va.FY = 0;
            vb.FX = vb.FY = 0;
            vc.FX = vc.FY = 0;

            float frc = 0.9f;

            va.Position += va.Velocity * delay;
            vb.Position += vb.Velocity * delay;
            vc.Position += vc.Velocity * delay;

            if (va.Position.Y < -200)
            {
                va.VY = -va.VY * frc;
                va.Y = -200;
                va.VX *= frc;
            }
            if (vb.Position.Y < -200)
            {
                vb.VY = -vb.VY * frc;
                vb.Y = -200;
                vb.VX *= frc;
            }
            if (vc.Position.Y < -200)
            {
                vc.VY = -vc.VY * frc;
                vc.Y = -200;
                vc.VX *= frc;
            }

            va.Force = new Vector(0, 0);
            vb.Force = new Vector(0, 0);
            vc.Force = new Vector(0, 0);
        }

        private void Preservation()
        {
            DistancePreservation();
            AreaPreservation();
        }

        private void AreaPreservation()
        {
            areprelink.AreaPreservation();
        }

        private void DistancePreservation()
        {
            disprelinkAB.DistancePreservation();
            disprelinkBC.DistancePreservation();
            disprelinkCA.DistancePreservation();
        }

        private void Gravity()
        {
            va.FY += Mass * -200f;
            vb.FY += Mass * -200f;
            vc.FY += Mass * -200f;
        }

        public bool Select(Vector mpos, out int id, out float u, out float v)
        {
            float unused;
            if (JellyMath.PointTriangle(mpos, areprelink.jva.Position, areprelink.jvb.Position, areprelink.jvc.Position, out u, out v, out unused))
            {
                id = 0;
                return true;
            }
            id = int.MaxValue;
            return false;
        }

        public void AddForceAt(int aplid, float u, float v, Vector mpos, float ks)
        {
            if (aplid == 0)
            {
                float w = 1 - u - v;
                Vector target = areprelink.jva.Position * u + areprelink.jvb.Position * v + areprelink.jvc.Position * w;
                Vector diff = mpos - target;
                Vector force = diff * ks;
                areprelink.jva.Force += force * u;
                areprelink.jvb.Force += force * v;
                areprelink.jvc.Force += force * w;
            }
        }

        public void DebugDraw(Graphics g)
        {
            Pen redpen = new Pen(Color.Red);
            Pen bluepen = new Pen(Color.Blue);

            g.DrawEllipse(redpen, va.Position.X - 5, va.Position.Y - 5, 10, 10);
            g.DrawEllipse(redpen, vb.Position.X - 5, vb.Position.Y - 5, 10, 10);
            g.DrawEllipse(redpen, vc.Position.X - 5, vc.Position.Y - 5, 10, 10);

            g.DrawLine(bluepen, va.Position, vb.Position);
            g.DrawLine(bluepen, vb.Position, vc.Position);
            g.DrawLine(bluepen, vc.Position, va.Position);

            Vector mab = (va.Position + vb.Position) * 0.5;
            Vector mbc = (vb.Position + vc.Position) * 0.5;
            Vector mca = (vc.Position + va.Position) * 0.5;

            g.DrawLine(bluepen, mab, mab + disprelinkAB.PosNormal * 10);
            g.DrawLine(bluepen, mbc, mbc + disprelinkBC.PosNormal * 10);
            g.DrawLine(bluepen, mca, mca + disprelinkCA.PosNormal * 10);
        }
    }
}
