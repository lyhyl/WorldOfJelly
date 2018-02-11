using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace UnionPreColl
{
    public class JellyBox : JellyObject
    {
        public float Size { set; get; }

        public JellyBox(float size, Vector pos, float mass)
            : base(mass)
        {
            Size = size;

            /*_nodes = new JellyVertex[5];
            Vector xoff = new Vector(size * 0.5f, 0);
            Vector yoff = new Vector(0, size * 0.5f);
            _nodes[0] = new JellyVertex(pos - xoff + yoff);
            _nodes[1] = new JellyVertex(pos + xoff + yoff);
            _nodes[2] = new JellyVertex(pos);
            _nodes[3] = new JellyVertex(pos - xoff - yoff);
            _nodes[4] = new JellyVertex(pos + xoff - yoff);

            float KS = 4000;
            float KD = 500;

            _disprelinks = new DisPreLink[8];
            _disprelinks[0] = new DisPreLink(_nodes[0], _nodes[1], KS, KD);
            _disprelinks[1] = new DisPreLink(_nodes[1], _nodes[2], KS, KD);
            _disprelinks[2] = new DisPreLink(_nodes[2], _nodes[0], KS, KD);

            _disprelinks[3] = new DisPreLink(_nodes[4], _nodes[3], KS, KD);
            _disprelinks[4] = new DisPreLink(_nodes[3], _nodes[2], KS, KD);
            _disprelinks[5] = new DisPreLink(_nodes[2], _nodes[4], KS, KD);

            _disprelinks[6] = new DisPreLink(_nodes[1], _nodes[4], KS, KD);
            _disprelinks[7] = new DisPreLink(_nodes[3], _nodes[0], KS, KD);

            float KA = 1000000;

            _areprelinks = new ArePreLink[4];
            _areprelinks[0] = new ArePreLink(_nodes[0], _nodes[1], _nodes[2], KA, _disprelinks[0].NegNormal, _disprelinks[1].NegNormal, _disprelinks[2].NegNormal);
            _areprelinks[1] = new ArePreLink(_nodes[4], _nodes[3], _nodes[2], KA, _disprelinks[3].NegNormal, _disprelinks[4].NegNormal, _disprelinks[5].NegNormal);

            _areprelinks[2] = new ArePreLink(_nodes[1], _nodes[4], _nodes[2], KA, _disprelinks[6].NegNormal, _disprelinks[5].PosNormal, _disprelinks[1].PosNormal);
            _areprelinks[3] = new ArePreLink(_nodes[3], _nodes[0], _nodes[2], KA, _disprelinks[7].NegNormal, _disprelinks[2].PosNormal, _disprelinks[4].PosNormal);*/
            //nodes
            _nodes = new JellyVertex[5];
            _nodes[0] = new JellyVertex(new Vector(pos.X + -1 * size, pos.Y + 1 * size));
            _nodes[1] = new JellyVertex(new Vector(pos.X + 1 * size, pos.Y + 1 * size));
            _nodes[2] = new JellyVertex(new Vector(pos.X + 0 * size, pos.Y + 0 * size));
            _nodes[3] = new JellyVertex(new Vector(pos.X + -1 * size, pos.Y + -1 * size));
            _nodes[4] = new JellyVertex(new Vector(pos.X + 1 * size, pos.Y + -1 * size));

            //distance links
            float KS = 4000;
            float KD = 500;

            _disprelinks = new DisPreLink[8];
            _disprelinks[0] = new DisPreLink(_nodes[0], _nodes[1], KS, KD);
            _disprelinks[1] = new DisPreLink(_nodes[1], _nodes[4], KS, KD);
            _disprelinks[2] = new DisPreLink(_nodes[4], _nodes[3], KS, KD);
            _disprelinks[3] = new DisPreLink(_nodes[3], _nodes[0], KS, KD);
            _disprelinks[4] = new DisPreLink(_nodes[2], _nodes[0], KS, KD);
            _disprelinks[5] = new DisPreLink(_nodes[2], _nodes[1], KS, KD);
            _disprelinks[6] = new DisPreLink(_nodes[2], _nodes[4], KS, KD);
            _disprelinks[7] = new DisPreLink(_nodes[2], _nodes[3], KS, KD);

            //area links
            float KA = 1000000;

            _areprelinks = new ArePreLink[4];
            _areprelinks[0] = new ArePreLink(_nodes[2], _nodes[1], _nodes[0], KA, _disprelinks[5].PosNormal, _disprelinks[0].NegNormal, _disprelinks[4].NegNormal);
            _areprelinks[1] = new ArePreLink(_nodes[2], _nodes[4], _nodes[1], KA, _disprelinks[6].PosNormal, _disprelinks[1].NegNormal, _disprelinks[5].NegNormal);
            _areprelinks[2] = new ArePreLink(_nodes[2], _nodes[3], _nodes[4], KA, _disprelinks[7].PosNormal, _disprelinks[2].NegNormal, _disprelinks[6].NegNormal);
            _areprelinks[3] = new ArePreLink(_nodes[2], _nodes[0], _nodes[3], KA, _disprelinks[4].PosNormal, _disprelinks[3].NegNormal, _disprelinks[7].NegNormal);
        }

        protected override void DebugDrawEdge(Graphics g)
        {
            Pen pen = new Pen(Color.Green);
            g.DrawLine(pen, _nodes[0].Position, _nodes[1].Position);
            g.DrawLine(pen, _nodes[1].Position, _nodes[4].Position);
            g.DrawLine(pen, _nodes[4].Position, _nodes[3].Position);
            g.DrawLine(pen, _nodes[3].Position, _nodes[0].Position);
        }
    }
}
