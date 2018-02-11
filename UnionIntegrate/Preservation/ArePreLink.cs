namespace UnionIntegrate
{
    public class ArePreLink
    {
        public JellyVertex jva, jvb, jvc;
        public Vector NorAB, NorBC, NorCA;//!
        private double OrgDoubleArea { set; get; }

        public double DoubleArea { get { return (double)JMath.Cross(jvb.Position - jva.Position, jvc.Position - jva.Position); } }
        public double KA { set; get; }

        public ArePreLink(JellyVertex a, JellyVertex b, JellyVertex c, double ka, Vector nab, Vector nbc, Vector nca)
        {
            jva = a;
            jvb = b;
            jvc = c;
            KA = ka;
            NorAB = nab;
            NorBC = nbc;
            NorCA = nca;
            OrgDoubleArea = DoubleArea;
        }

        public void AreaPreservation()
        {
            double delta = OrgDoubleArea - DoubleArea;
            double force = delta / OrgDoubleArea * KA;

            Vector forab = NorAB * force;
            Vector forbc = NorBC * force;
            Vector forca = NorCA * force;

            jva.NxtForce += forab + forca;
            jvb.NxtForce += forab + forbc;
            jvc.NxtForce += forbc + forca;
        }
    }
}
