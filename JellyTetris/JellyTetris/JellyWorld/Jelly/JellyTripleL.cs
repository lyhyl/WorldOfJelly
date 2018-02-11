using JellyTetris.JellyWorld.Math;
using JellyTetris.JellyWorld.Physic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JellyTetris.JellyWorld.Jelly
{
	public class JellyTripleL : JellyObject
	{
        public JellyTripleL(GraphicsDevice gd, float mass, float size, Color color, JellyVector2 pos)
            : base(gd, mass, size, color, 8)
		{
			//nodes
			_nodes = new JellyVertex[11];
			_nodes[0] = new JellyVertex(new JellyVector2(pos.X + -2 * size, pos.Y + 2 * size));
			_nodes[1] = new JellyVertex(new JellyVector2(pos.X + 0 * size, pos.Y + 2 * size));
			_nodes[2] = new JellyVertex(new JellyVector2(pos.X + 2 * size, pos.Y + 2 * size));
			_nodes[3] = new JellyVertex(new JellyVector2(pos.X + -1 * size, pos.Y + 1 * size));
			_nodes[4] = new JellyVertex(new JellyVector2(pos.X + 1 * size, pos.Y + 1 * size));
			_nodes[5] = new JellyVertex(new JellyVector2(pos.X + -2 * size, pos.Y + 0 * size));
			_nodes[6] = new JellyVertex(new JellyVector2(pos.X + 0 * size, pos.Y + 0 * size));
			_nodes[7] = new JellyVertex(new JellyVector2(pos.X + 2 * size, pos.Y + 0 * size));
			_nodes[8] = new JellyVertex(new JellyVector2(pos.X + 1 * size, pos.Y + -1 * size));
			_nodes[9] = new JellyVertex(new JellyVector2(pos.X + 0 * size, pos.Y + -2 * size));
			_nodes[10] = new JellyVertex(new JellyVector2(pos.X + 2 * size, pos.Y + -2 * size));
			
			//distance links
			float KS = JellyWorld.KS;
            float KD = JellyWorld.KD;
			
			_disprelinks = new DisPreLink[22];
			_disprelinks[0] = new DisPreLink(_nodes[0], _nodes[1], KS, KD);
			_disprelinks[1] = new DisPreLink(_nodes[1], _nodes[2], KS, KD);
			_disprelinks[2] = new DisPreLink(_nodes[2], _nodes[7], KS, KD);
			_disprelinks[3] = new DisPreLink(_nodes[7], _nodes[10], KS, KD);
			_disprelinks[4] = new DisPreLink(_nodes[10], _nodes[9], KS, KD);
			_disprelinks[5] = new DisPreLink(_nodes[9], _nodes[6], KS, KD);
			_disprelinks[6] = new DisPreLink(_nodes[6], _nodes[5], KS, KD);
			_disprelinks[7] = new DisPreLink(_nodes[5], _nodes[0], KS, KD);
			_disprelinks[8] = new DisPreLink(_nodes[1], _nodes[6], KS, KD);
			_disprelinks[9] = new DisPreLink(_nodes[7], _nodes[6], KS, KD);
			_disprelinks[10] = new DisPreLink(_nodes[3], _nodes[0], KS, KD);
			_disprelinks[11] = new DisPreLink(_nodes[3], _nodes[1], KS, KD);
			_disprelinks[12] = new DisPreLink(_nodes[3], _nodes[6], KS, KD);
			_disprelinks[13] = new DisPreLink(_nodes[3], _nodes[5], KS, KD);
			_disprelinks[14] = new DisPreLink(_nodes[4], _nodes[1], KS, KD);
			_disprelinks[15] = new DisPreLink(_nodes[4], _nodes[2], KS, KD);
			_disprelinks[16] = new DisPreLink(_nodes[4], _nodes[7], KS, KD);
			_disprelinks[17] = new DisPreLink(_nodes[4], _nodes[6], KS, KD);
			_disprelinks[18] = new DisPreLink(_nodes[8], _nodes[6], KS, KD);
			_disprelinks[19] = new DisPreLink(_nodes[8], _nodes[7], KS, KD);
			_disprelinks[20] = new DisPreLink(_nodes[8], _nodes[10], KS, KD);
			_disprelinks[21] = new DisPreLink(_nodes[8], _nodes[9], KS, KD);
			
			//area links
            float KA = JellyWorld.KA;
			
			_areprelinks = new ArePreLink[12];
			_areprelinks[0] = new ArePreLink(_nodes[3], _nodes[1], _nodes[0], KA, _disprelinks[11].PosNormal, _disprelinks[0].NegNormal, _disprelinks[10].NegNormal);
			_areprelinks[1] = new ArePreLink(_nodes[3], _nodes[6], _nodes[1], KA, _disprelinks[12].PosNormal, _disprelinks[8].NegNormal, _disprelinks[11].NegNormal);
			_areprelinks[2] = new ArePreLink(_nodes[5], _nodes[3], _nodes[0], KA, _disprelinks[13].NegNormal, _disprelinks[10].PosNormal, _disprelinks[7].NegNormal);
			_areprelinks[3] = new ArePreLink(_nodes[6], _nodes[3], _nodes[5], KA, _disprelinks[12].NegNormal, _disprelinks[13].PosNormal, _disprelinks[6].NegNormal);
			_areprelinks[4] = new ArePreLink(_nodes[6], _nodes[4], _nodes[1], KA, _disprelinks[17].NegNormal, _disprelinks[14].PosNormal, _disprelinks[8].PosNormal);
			_areprelinks[5] = new ArePreLink(_nodes[4], _nodes[2], _nodes[1], KA, _disprelinks[15].PosNormal, _disprelinks[1].NegNormal, _disprelinks[14].NegNormal);
			_areprelinks[6] = new ArePreLink(_nodes[4], _nodes[7], _nodes[2], KA, _disprelinks[16].PosNormal, _disprelinks[2].NegNormal, _disprelinks[15].NegNormal);
			_areprelinks[7] = new ArePreLink(_nodes[4], _nodes[6], _nodes[7], KA, _disprelinks[17].PosNormal, _disprelinks[9].NegNormal, _disprelinks[16].NegNormal);
			_areprelinks[8] = new ArePreLink(_nodes[6], _nodes[8], _nodes[7], KA, _disprelinks[18].NegNormal, _disprelinks[19].PosNormal, _disprelinks[9].PosNormal);
			_areprelinks[9] = new ArePreLink(_nodes[8], _nodes[10], _nodes[7], KA, _disprelinks[20].PosNormal, _disprelinks[3].NegNormal, _disprelinks[19].NegNormal);
			_areprelinks[10] = new ArePreLink(_nodes[8], _nodes[9], _nodes[10], KA, _disprelinks[21].PosNormal, _disprelinks[4].NegNormal, _disprelinks[20].NegNormal);
			_areprelinks[11] = new ArePreLink(_nodes[6], _nodes[9], _nodes[8], KA, _disprelinks[5].NegNormal, _disprelinks[21].NegNormal, _disprelinks[18].PosNormal);
			
			//edges
			_edge[0] = _nodes[0];
			_edge[1] = _nodes[1];
			_edge[2] = _nodes[2];
			_edge[3] = _nodes[7];
			_edge[4] = _nodes[10];
			_edge[5] = _nodes[9];
			_edge[6] = _nodes[6];
			_edge[7] = _nodes[5];
			
			//edges' normals
			_edgenor[0] = _disprelinks[0].NegNormal;
			_edgenor[1] = _disprelinks[1].NegNormal;
			_edgenor[2] = _disprelinks[2].NegNormal;
			_edgenor[3] = _disprelinks[3].NegNormal;
			_edgenor[4] = _disprelinks[4].NegNormal;
			_edgenor[5] = _disprelinks[5].NegNormal;
			_edgenor[6] = _disprelinks[6].NegNormal;
            _edgenor[7] = _disprelinks[7].NegNormal;

            _cellNodes = new int[] { 3, 4, 8 };
            _extverid = new int[] { 3, 4, 8 };

            short[] tri =
            {
                ExtVIdx(8, 0), ExtVIdx(8, 1),
                ExtVIdx(8, 1), ExtVIdx(8, 2),
                ExtVIdx(8, 2), ExtVIdx(8, 2),
                ExtVIdx(8, 0), ExtVIdx(8, 0)
            };
            short[] e_tri =
            { 
                EgeVIdx(1), EgeVIdx(6), ExtVIdx(8, 0),
                EgeVIdx(1), EgeVIdx(6), ExtVIdx(8, 1),
                EgeVIdx(3), EgeVIdx(6), ExtVIdx(8, 1),
                EgeVIdx(3), EgeVIdx(6), ExtVIdx(8, 2)
            };

            //XNA
            InitXNA(3, tri, e_tri);

            SetupUV();

            _texture = ResourceManager.LoadImage("Jelly/Jelly_Base_3");

            _face_node_id = 4;
		}

        private void SetupUV()
        {
            SetVertexUV(0, 0.02083334f, 0.9791667f);
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
            SetVertexUV(12, 0.9791667f, 0.9791666f);
            SetVertexUV(13, 1f, 0.9166666f);
            SetVertexUV(14, 1f, 0.8333333f);
            SetVertexUV(15, 1f, 0.75f);
            SetVertexUV(16, 1f, 0.6666666f);
            SetVertexUV(17, 1f, 0.5833333f);
            SetVertexUV(18, 1f, 0.5f);
            SetVertexUV(19, 1f, 0.4166666f);
            SetVertexUV(20, 1f, 0.3333333f);
            SetVertexUV(21, 1f, 0.25f);
            SetVertexUV(22, 1f, 0.1666666f);
            SetVertexUV(23, 1f, 0.08333332f);
            SetVertexUV(24, 0.9791666f, 0.02083333f);
            SetVertexUV(25, 0.9166666f, 0f);
            SetVertexUV(26, 0.8333333f, 0f);
            SetVertexUV(27, 0.75f, 0f);
            SetVertexUV(28, 0.6666666f, 0f);
            SetVertexUV(29, 0.5833333f, 0f);
            SetVertexUV(30, 0.5208333f, 0.02083334f);
            SetVertexUV(31, 0.5f, 0.08333334f);
            SetVertexUV(32, 0.5f, 0.1666667f);
            SetVertexUV(33, 0.5f, 0.25f);
            SetVertexUV(34, 0.5f, 0.3333334f);
            SetVertexUV(35, 0.5f, 0.4166667f);
            SetVertexUV(36, 0.4791666f, 0.4791667f);
            SetVertexUV(37, 0.4166666f, 0.5f);
            SetVertexUV(38, 0.3333333f, 0.5f);
            SetVertexUV(39, 0.25f, 0.5f);
            SetVertexUV(40, 0.1666666f, 0.5f);
            SetVertexUV(41, 0.08333332f, 0.5f);
            SetVertexUV(42, 0.02083333f, 0.5208334f);
            SetVertexUV(43, 0f, 0.5833334f);
            SetVertexUV(44, 0f, 0.6666667f);
            SetVertexUV(45, 0f, 0.75f);
            SetVertexUV(46, 0f, 0.8333334f);
            SetVertexUV(47, 0f, 0.9166667f);
            SetVertexUV(48, 0.25f, 0.75f);
            SetVertexUV(49, 0.75f, 0.75f);
            SetVertexUV(50, 0.75f, 0.25f);
        }

        public override JellyObject[] Convert(int[] cid)
        {
            switch (cid.Length)
            {
                case 1:
                    int id = cid[0];
                    if (id == 4)
                    {
                        JellyBox jba, jbb;
                        jba = new JellyBox(_graphicsDevice, Mass, Size, _color, _nodes[3].Position);
                        jbb = new JellyBox(_graphicsDevice, Mass, Size, _color, _nodes[8].Position);
                        return new JellyObject[2] { jba, jbb };
                    }
                    else
                    {
                        JellyDouble jd = new JellyDouble(_graphicsDevice, Mass, Size, _color, _nodes[id].Position);
                        return new JellyObject[1] { jd };
                    }
                case 2:
                    {
                        // 3 - 0011
                        // 4 - 0100
                        // 8 - 1000
                        // |
                        // 3 | 4 - 0111
                        // 4 | 8 - 1100
                        // 3 | 8 - 1011
                        // >>
                        // 0111 - 01 - 1
                        // 1100 - 11 - 3
                        // 1011 - 10 - 2
                        int mark = (cid[0] | cid[1]) >> 2;
                        JellyObject jo = null;
                        switch (mark)
                        {
                            case 1://cell 8
                                jo = new JellyBox(_graphicsDevice, Mass, Size, _color, _nodes[8].Position);
                                break;
                            case 2://cell 4
                                jo = new JellyBox(_graphicsDevice, Mass, Size, _color, _nodes[4].Position);
                                break;
                            case 3://cell 3
                                jo = new JellyBox(_graphicsDevice, Mass, Size, _color, _nodes[3].Position);
                                break;
                        }
                        return new JellyObject[1] { jo };
                    }
                default:
                    return null;
            }
        }
    }
}
