using JellyTetris.JellyWorld.Math;

namespace JellyTetris.JellyWorld.Physic
{
    public class ArePreLink
    {
        public JellyVertex jva, jvb, jvc;
        public JellyVector2 NorAB, NorBC, NorCA;
        private float OrgDoubleArea { set; get; }

        public float DoubleArea { get { return JellyMath.Cross(jvb.Position - jva.Position, jvc.Position - jva.Position); } }
        public float KA;

        public ArePreLink(JellyVertex a, JellyVertex b, JellyVertex c, float ka, JellyVector2 nab, JellyVector2 nbc, JellyVector2 nca)
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
            float delta = OrgDoubleArea - DoubleArea;
            float force = delta / OrgDoubleArea * KA;

            JellyVector2 forab = NorAB * force;
            JellyVector2 forbc = NorBC * force;
            JellyVector2 forca = NorCA * force;

            jva.Force += forab + forca;
            jvb.Force += forab + forbc;
            jvc.Force += forbc + forca;
        }
    }
}
