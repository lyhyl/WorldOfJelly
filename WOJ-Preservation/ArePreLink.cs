using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnionPreColl
{
    public class ArePreLink
    {
        public JellyVertex jva, jvb, jvc;
        public Vector NorAB, NorBC, NorCA;//!
        private float OrgDoubleArea { set; get; }

        public float DoubleArea { get { return (float)JellyMath.Cross(jvb.Position - jva.Position, jvc.Position - jva.Position); } }
        public float KA { set; get; }

        public ArePreLink(JellyVertex a, JellyVertex b, JellyVertex c, float ka, Vector nab, Vector nbc, Vector nca)
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

            Vector forab = NorAB * force;
            Vector forbc = NorBC * force;
            Vector forca = NorCA * force;

            jva.Force += forab + forca;
            jvb.Force += forab + forbc;
            jvc.Force += forbc + forca;
        }
    }
}
