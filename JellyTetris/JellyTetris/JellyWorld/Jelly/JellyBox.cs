using JellyTetris.JellyWorld.Math;
using JellyTetris.JellyWorld.Physic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JellyTetris.JellyWorld.Jelly
{
    public class JellyBox : JellyObject
    {
        public JellyBox(GraphicsDevice gd, float mass, float size, Color color, JellyVector2 pos)
            : base(gd, mass, size, color, 4)
        {
            //nodes
            _nodes = new JellyVertex[5];
            _nodes[0] = new JellyVertex(new JellyVector2(pos.X + -1 * size, pos.Y + 1 * size));
            _nodes[1] = new JellyVertex(new JellyVector2(pos.X + 1 * size, pos.Y + 1 * size));
            _nodes[2] = new JellyVertex(new JellyVector2(pos.X + 0 * size, pos.Y + 0 * size));
            _nodes[3] = new JellyVertex(new JellyVector2(pos.X + -1 * size, pos.Y + -1 * size));
            _nodes[4] = new JellyVertex(new JellyVector2(pos.X + 1 * size, pos.Y + -1 * size));

            //distance links
            float KS = JellyWorld.KS;
            float KD = JellyWorld.KD;

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
            float KA = JellyWorld.KA;

            _areprelinks = new ArePreLink[4];
            _areprelinks[0] = new ArePreLink(_nodes[2], _nodes[1], _nodes[0], KA, _disprelinks[5].PosNormal, _disprelinks[0].NegNormal, _disprelinks[4].NegNormal);
            _areprelinks[1] = new ArePreLink(_nodes[2], _nodes[4], _nodes[1], KA, _disprelinks[6].PosNormal, _disprelinks[1].NegNormal, _disprelinks[5].NegNormal);
            _areprelinks[2] = new ArePreLink(_nodes[2], _nodes[3], _nodes[4], KA, _disprelinks[7].PosNormal, _disprelinks[2].NegNormal, _disprelinks[6].NegNormal);
            _areprelinks[3] = new ArePreLink(_nodes[2], _nodes[0], _nodes[3], KA, _disprelinks[4].PosNormal, _disprelinks[3].NegNormal, _disprelinks[7].NegNormal);

            //edges
            _edge[0] = _nodes[0];
            _edge[1] = _nodes[1];
            _edge[2] = _nodes[4];
            _edge[3] = _nodes[3];

            //edges' normals
            _edgenor[0] = _disprelinks[0].NegNormal;
            _edgenor[1] = _disprelinks[1].NegNormal;
            _edgenor[2] = _disprelinks[2].NegNormal;
            _edgenor[3] = _disprelinks[3].NegNormal;

            _cellNodes = new int[] { 2 };
            _extverid = new int[] { 2 };

            short[] tri =
            {
                ExtVIdx(4, 0), ExtVIdx(4, 0),
                ExtVIdx(4, 0), ExtVIdx(4, 0)
            };
            short[] e_tri = { };

            //XNA
            InitXNA(1, tri, e_tri);

            SetupUV();

            _texture = ResourceManager.LoadImage("Jelly/Jelly_Base_1");

            _face_node_id = 2;
        }

        private void SetupUV()
        {
            SetVertexUV(0, 0.04166667f, 0.9583334f);
            SetVertexUV(1, 0.1666667f, 1f);
            SetVertexUV(2, 0.3333333f, 1f);
            SetVertexUV(3, 0.5f, 1f);
            SetVertexUV(4, 0.6666667f, 1f);
            SetVertexUV(5, 0.8333334f, 1f);
            SetVertexUV(6, 0.9583334f, 0.9583333f);
            SetVertexUV(7, 1f, 0.8333333f);
            SetVertexUV(8, 1f, 0.6666666f);
            SetVertexUV(9, 1f, 0.5f);
            SetVertexUV(10, 1f, 0.3333333f);
            SetVertexUV(11, 1f, 0.1666667f);
            SetVertexUV(12, 0.9583333f, 0.04166666f);
            SetVertexUV(13, 0.8333333f, 0f);
            SetVertexUV(14, 0.6666666f, 0f);
            SetVertexUV(15, 0.5f, 0f);
            SetVertexUV(16, 0.3333333f, 0f);
            SetVertexUV(17, 0.1666667f, 0f);
            SetVertexUV(18, 0.04166666f, 0.04166667f);
            SetVertexUV(19, 0f, 0.1666667f);
            SetVertexUV(20, 0f, 0.3333333f);
            SetVertexUV(21, 0f, 0.5f);
            SetVertexUV(22, 0f, 0.6666667f);
            SetVertexUV(23, 0f, 0.8333334f);
            SetVertexUV(24, 0.5f, 0.5f);
        }

        public override JellyObject[] Convert(int[] cid)
        {
            return null;
        }
    }
}
