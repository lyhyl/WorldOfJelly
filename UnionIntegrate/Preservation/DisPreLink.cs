namespace UnionIntegrate
{
    public class DisPreLink
    {
        public double KS { set; get; }
        public double KD { set; get; }
        public Vector PosNormal = new Vector(0, 0);
        public Vector NegNormal = new Vector(0, 0);

        private double OrgDistance { set; get; }
        private JellyVertex jva, jvb;

        public DisPreLink(JellyVertex a, JellyVertex b, double ks, double kd)
        {
            jva = a;
            jvb = b;
            KS = ks;
            KD = kd;
            OrgDistance = (double)(a.Position - b.Position).Length;
        }

        public void DistancePreservation()
        {
            Vector diff = jva.Position - jvb.Position;

            double dis = (double)diff.Length;
            double invdis = dis == 0 ? 1 : 1.0 / dis;
            double delta = dis - OrgDistance;

            Vector fab = diff * (delta * KS + JMath.Dot(jva.Velocity - jvb.Velocity, diff) * KD * invdis) * invdis;

            jva.NxtForce -= fab;
            jvb.NxtForce += fab;

            PosNormal.X = -diff.Y * invdis;
            PosNormal.Y = diff.X * invdis;
            NegNormal.X = -PosNormal.X;
            NegNormal.Y = -PosNormal.Y;
        }
    }
}
