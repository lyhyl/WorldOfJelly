using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace UnionPreColl
{
    abstract public class JellyObject
    {
        protected float CG = -200;

        public float Mass { set; get; }
        protected DisPreLink[] _disprelinks;
        protected ArePreLink[] _areprelinks;
        protected JellyVertex[] _nodes;

        protected JellyObject(float mass) { Mass = mass; }

        private void AddPureGravity()
        {
            foreach(JellyVertex v in _nodes)
                v.FY += Mass * -200;
        }
        private void Integrate(JellyVertex jv, float delay)
        {
            jv.Velocity += jv.Force * (delay / Mass);
            jv.Position += jv.Velocity * delay;

            float frc = 0.9f;

            if (jv.Position.Y < -200)
            {
                jv.VY = -jv.VY * frc;
                jv.Y = -200;
                jv.VX *= frc;
            }

            jv.FX = jv.FY = 0;
        }

        public void Update(float delay)
        {
            AddPureGravity();
            foreach (DisPreLink dpl in _disprelinks)
                dpl.DistancePreservation();
            foreach (ArePreLink apl in _areprelinks)
                apl.AreaPreservation();
            foreach (JellyVertex v in _nodes)
                Integrate(v, delay);
        }

        public bool Select(Vector mpos, out int id, out float u, out float v)
        {
            float unused;
            foreach (ArePreLink apl in _areprelinks)
                if (JellyMath.PointTriangle(mpos, apl.jva.Position, apl.jvb.Position, apl.jvc.Position, out u, out v, out unused))
                {
                    id = 0;
                    return true;
                }
            u = -1;
            v = -1;
            id = int.MaxValue;
            return false;
        }

        public void AddForceAt(int aplid, float u, float v, Vector mpos, float ks)
        {
            if (aplid < _areprelinks.Length)
            {
                float w = 1 - u - v;
                Vector target = _areprelinks[aplid].jva.Position * u + _areprelinks[aplid].jvb.Position * v + _areprelinks[aplid].jvc.Position * w;
                Vector diff = mpos - target;
                Vector force = diff * ks;
                _areprelinks[aplid].jva.Force += force;
                _areprelinks[aplid].jvb.Force += force;
                _areprelinks[aplid].jvc.Force += force;
            }
        }

        abstract protected void DebugDrawEdge(Graphics g);

        public void DebugDraw(Graphics g)
        {
            Pen redpen = new Pen(Color.Red);
            Pen bluepen = new Pen(Color.Blue, 1.5f);

            foreach (JellyVertex v in _nodes)
                g.DrawEllipse(redpen, v.X - 5, v.Y - 5, 10, 10);

            foreach (ArePreLink apl in _areprelinks)
            {
                Vector mab = (apl.jva.Position + apl.jvb.Position) * 0.5f;
                Vector mbc = (apl.jvb.Position + apl.jvc.Position) * 0.5f;
                Vector mca = (apl.jvc.Position + apl.jva.Position) * 0.5f;
                g.DrawLine(bluepen, mab, mab + apl.NorAB * 10);
                g.DrawLine(bluepen, mbc, mbc + apl.NorBC * 10);
                g.DrawLine(bluepen, mca, mca + apl.NorCA * 10);
            }

            DebugDrawEdge(g);
        }
    }
}
