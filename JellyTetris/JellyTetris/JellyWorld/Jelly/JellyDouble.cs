using JellyTetris.JellyWorld.Math;
using JellyTetris.JellyWorld.Physic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JellyTetris.JellyWorld.Jelly
{
    public class JellyDouble : JellyObject
    {
        public JellyDouble(GraphicsDevice gd, float mass, float size, Color color, JellyVector2 pos)
            : base(gd, mass, size, color, 6)
        {
            //nodes
            _nodes = new JellyVertex[8];
            _nodes[0] = new JellyVertex(new JellyVector2(pos.X + -2 * size, pos.Y + 1 * size));
            _nodes[1] = new JellyVertex(new JellyVector2(pos.X + 0 * size, pos.Y + 1 * size));
            _nodes[2] = new JellyVertex(new JellyVector2(pos.X + 2 * size, pos.Y + 1 * size));
            _nodes[3] = new JellyVertex(new JellyVector2(pos.X + -1 * size, pos.Y + 0 * size));
            _nodes[4] = new JellyVertex(new JellyVector2(pos.X + 1 * size, pos.Y + 0 * size));
            _nodes[5] = new JellyVertex(new JellyVector2(pos.X + -2 * size, pos.Y + -1 * size));
            _nodes[6] = new JellyVertex(new JellyVector2(pos.X + 0 * size, pos.Y + -1 * size));
            _nodes[7] = new JellyVertex(new JellyVector2(pos.X + 2 * size, pos.Y + -1 * size));

            InitExceptNode();
        }

        internal JellyDouble(GraphicsDevice gd, float mass, float size, Color col, JellyVector2 a, JellyVector2 b)
            : base(gd, mass, size, col, 6)
        {
            //nodes
            _nodes = new JellyVertex[8];
            JellyVector2 mid = (a + b) * 0.5f;

            _nodes[0] = new JellyVertex(new JellyVector2(mid.X + -2 * size, mid.Y + 1 * size));
            _nodes[1] = new JellyVertex(new JellyVector2(mid.X + 0 * size, mid.Y + 1 * size));
            _nodes[2] = new JellyVertex(new JellyVector2(mid.X + 2 * size, mid.Y + 1 * size));
            _nodes[3] = new JellyVertex(new JellyVector2(mid.X + -1 * size, mid.Y + 0 * size));
            _nodes[4] = new JellyVertex(new JellyVector2(mid.X + 1 * size, mid.Y + 0 * size));
            _nodes[5] = new JellyVertex(new JellyVector2(mid.X + -2 * size, mid.Y + -1 * size));
            _nodes[6] = new JellyVertex(new JellyVector2(mid.X + 0 * size, mid.Y + -1 * size));
            _nodes[7] = new JellyVertex(new JellyVector2(mid.X + 2 * size, mid.Y + -1 * size));

            InitExceptNode();
        }

        private void InitExceptNode()
        {
            //distance links
            float KS = JellyWorld.KS;
            float KD = JellyWorld.KD;

            _disprelinks = new DisPreLink[15];
            _disprelinks[0] = new DisPreLink(_nodes[0], _nodes[1], KS, KD);
            _disprelinks[1] = new DisPreLink(_nodes[1], _nodes[2], KS, KD);
            _disprelinks[2] = new DisPreLink(_nodes[2], _nodes[7], KS, KD);
            _disprelinks[3] = new DisPreLink(_nodes[7], _nodes[6], KS, KD);
            _disprelinks[4] = new DisPreLink(_nodes[6], _nodes[5], KS, KD);
            _disprelinks[5] = new DisPreLink(_nodes[5], _nodes[0], KS, KD);
            _disprelinks[6] = new DisPreLink(_nodes[3], _nodes[0], KS, KD);
            _disprelinks[7] = new DisPreLink(_nodes[3], _nodes[1], KS, KD);
            _disprelinks[8] = new DisPreLink(_nodes[1], _nodes[6], KS, KD);
            _disprelinks[9] = new DisPreLink(_nodes[3], _nodes[6], KS, KD);
            _disprelinks[10] = new DisPreLink(_nodes[3], _nodes[5], KS, KD);
            _disprelinks[11] = new DisPreLink(_nodes[4], _nodes[1], KS, KD);
            _disprelinks[12] = new DisPreLink(_nodes[4], _nodes[2], KS, KD);
            _disprelinks[13] = new DisPreLink(_nodes[4], _nodes[7], KS, KD);
            _disprelinks[14] = new DisPreLink(_nodes[4], _nodes[6], KS, KD);

            //area links
            float KA = JellyWorld.KA;

            _areprelinks = new ArePreLink[8];
            _areprelinks[0] = new ArePreLink(_nodes[3], _nodes[1], _nodes[0], KA, _disprelinks[7].PosNormal, _disprelinks[0].NegNormal, _disprelinks[6].NegNormal);
            _areprelinks[1] = new ArePreLink(_nodes[3], _nodes[6], _nodes[1], KA, _disprelinks[9].PosNormal, _disprelinks[8].NegNormal, _disprelinks[7].NegNormal);
            _areprelinks[2] = new ArePreLink(_nodes[3], _nodes[5], _nodes[6], KA, _disprelinks[10].PosNormal, _disprelinks[4].NegNormal, _disprelinks[9].NegNormal);
            _areprelinks[3] = new ArePreLink(_nodes[3], _nodes[0], _nodes[5], KA, _disprelinks[6].PosNormal, _disprelinks[5].NegNormal, _disprelinks[10].NegNormal);
            _areprelinks[4] = new ArePreLink(_nodes[4], _nodes[2], _nodes[1], KA, _disprelinks[12].PosNormal, _disprelinks[1].NegNormal, _disprelinks[11].NegNormal);
            _areprelinks[5] = new ArePreLink(_nodes[4], _nodes[7], _nodes[2], KA, _disprelinks[13].PosNormal, _disprelinks[2].NegNormal, _disprelinks[12].NegNormal);
            _areprelinks[6] = new ArePreLink(_nodes[4], _nodes[6], _nodes[7], KA, _disprelinks[14].PosNormal, _disprelinks[3].NegNormal, _disprelinks[13].NegNormal);
            _areprelinks[7] = new ArePreLink(_nodes[4], _nodes[1], _nodes[6], KA, _disprelinks[11].PosNormal, _disprelinks[8].PosNormal, _disprelinks[14].NegNormal);

            //edges
            _edge[0] = _nodes[0];
            _edge[1] = _nodes[1];
            _edge[2] = _nodes[2];
            _edge[3] = _nodes[7];
            _edge[4] = _nodes[6];
            _edge[5] = _nodes[5];

            //edges' normals
            _edgenor[0] = _disprelinks[0].NegNormal;
            _edgenor[1] = _disprelinks[1].NegNormal;
            _edgenor[2] = _disprelinks[2].NegNormal;
            _edgenor[3] = _disprelinks[3].NegNormal;
            _edgenor[4] = _disprelinks[4].NegNormal;
            _edgenor[5] = _disprelinks[5].NegNormal;

            _cellNodes = new int[] { 3, 4 };
            _extverid = new int[] { 3, 4 };

            short[] tri =
            {
                ExtVIdx(6, 0), ExtVIdx(6, 1),
                ExtVIdx(6, 1), ExtVIdx(6, 1),
                ExtVIdx(6, 0), ExtVIdx(6, 0)
            };
            short[] e_tri =
            {
                EgeVIdx(1), EgeVIdx(4), ExtVIdx(6, 0),
                EgeVIdx(1), EgeVIdx(4), ExtVIdx(6, 1)
            };

            //XNA
            InitXNA(2, tri, e_tri);

            SetupUV();

            _texture = ResourceManager.LoadImage("Jelly/Jelly_Base_2");

            _face_node_id = 3;
        }

        private void SetupUV()
        {
            SetVertexUV(0, 0.02083334f, 0.9583334f);
            SetVertexUV(1, 0.08333334f, 1f);
            SetVertexUV(2, 0.1666667f, 1f);
            SetVertexUV(3, 0.25f, 1f);
            SetVertexUV(4, 0.3333334f, 1f);
            SetVertexUV(5, 0.4166667f, 1f);
            SetVertexUV(6, 0.5f, 1f);
            SetVertexUV(7, 0.5833334f, 1f);
            SetVertexUV(8, 0.6666667f, 1f);
            SetVertexUV(9, 0.75f, 1f);
            SetVertexUV(10, 0.8333334f, 1f);
            SetVertexUV(11, 0.9166667f, 1f);
            SetVertexUV(12, 0.9791667f, 0.9583333f);
            SetVertexUV(13, 1f, 0.8333333f);
            SetVertexUV(14, 1f, 0.6666666f);
            SetVertexUV(15, 1f, 0.5f);
            SetVertexUV(16, 1f, 0.3333333f);
            SetVertexUV(17, 1f, 0.1666666f);
            SetVertexUV(18, 0.9791666f, 0.04166666f);
            SetVertexUV(19, 0.9166666f, 0f);
            SetVertexUV(20, 0.8333333f, 0f);
            SetVertexUV(21, 0.75f, 0f);
            SetVertexUV(22, 0.6666666f, 0f);
            SetVertexUV(23, 0.5833333f, 0f);
            SetVertexUV(24, 0.5f, 0f);
            SetVertexUV(25, 0.4166666f, 0f);
            SetVertexUV(26, 0.3333333f, 0f);
            SetVertexUV(27, 0.25f, 0f);
            SetVertexUV(28, 0.1666666f, 0f);
            SetVertexUV(29, 0.08333332f, 0f);
            SetVertexUV(30, 0.02083333f, 0.04166667f);
            SetVertexUV(31, 0f, 0.1666667f);
            SetVertexUV(32, 0f, 0.3333334f);
            SetVertexUV(33, 0f, 0.5000001f);
            SetVertexUV(34, 0f, 0.6666667f);
            SetVertexUV(35, 0f, 0.8333334f);
            SetVertexUV(36, 0.25f, 0.5f);
            SetVertexUV(37, 0.75f, 0.5f);
        }

        public override JellyObject[] Convert(int[] cid)
        {
            if (cid.Length == 2)
                return null;
            else
            {
                JellyObject jo = new JellyBox(_graphicsDevice, Mass, Size, _color, _nodes[cid[0] == 3 ? 4 : 3].Position);
                return new JellyObject[1] { jo };
            }
        }
    }
}
