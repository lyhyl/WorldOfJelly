using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnionPreColl
{
    public class DisPreLink
    {
        public float KS { set; get; }
        public float KD { set; get; }
        public Vector PosNormal = new Vector(0, 0);
        public Vector NegNormal = new Vector(0, 0);

        private float OrgDistance { set; get; }
        private JellyVertex jva, jvb;

        public DisPreLink(JellyVertex a, JellyVertex b, float ks, float kd)
        {
            jva = a;
            jvb = b;
            KS = ks;
            KD = kd;
            OrgDistance = (float)(a.Position - b.Position).Length;
        }

        public void DistancePreservation()
        {
            Vector diff = jva.Position - jvb.Position;

            float dis = (float)diff.Length;
            float invdis = 1.0f / dis;
            float delta = dis - OrgDistance;

            Vector fab = diff * (delta * KS + JellyMath.Dot(jva.Velocity - jvb.Velocity, diff) * KD * invdis) * invdis;

            jva.Force -= fab;
            jvb.Force += fab;

            PosNormal.X = -diff.Y * invdis;
            PosNormal.Y = diff.X * invdis;
            NegNormal.X = -PosNormal.X;
            NegNormal.Y = -PosNormal.Y;
        }
    }
}
