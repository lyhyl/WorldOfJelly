using JellyTetris.JellyWorld.Math;
using Microsoft.Xna.Framework;

namespace JellyTetris.JellyWorld.Physic
{
    public class DisPreLink
    {
        public float KS { set; get; }
        public float KD { set; get; }
        public JellyVector2 PosNormal = JellyVector2.Zero;
        public JellyVector2 NegNormal = JellyVector2.Zero;

        private float OrgDistance { set; get; }
        private JellyVertex jva, jvb;

        public DisPreLink(JellyVertex a, JellyVertex b, float ks, float kd)
        {
            jva = a;
            jvb = b;
            KS = ks;
            KD = kd;
            OrgDistance = (a.Position - b.Position).Length();
        }

        public void DistancePreservation()
        {
            JellyVector2 diff = jva.Position - jvb.Position;

            float dis = diff.Length();
            float invdis = dis == 0 ? 1 : 1.0f / dis;
            float delta = dis - OrgDistance;

            JellyVector2 fab = diff * ((delta * KS + Vector2.Dot(jva.Velocity - jvb.Velocity, diff) * KD * invdis) * invdis);

            jva.Force -= fab;
            jvb.Force += fab;

            PosNormal.X = -diff.Y * invdis;
            PosNormal.Y = diff.X * invdis;
            NegNormal.X = -PosNormal.X;
            NegNormal.Y = -PosNormal.Y;
        }
    }
}
