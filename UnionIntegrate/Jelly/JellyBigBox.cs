namespace UnionIntegrate
{
    public class JellyBigBox : JellyObject
    {
        public JellyBigBox(double mass, double size, Vector pos)
            : base(mass, size, 8)
        {
            //nodes
            _nodes = new JellyVertex[13];
            _nodes[0] = new JellyVertex(new Vector(pos.X + -2 * size, pos.Y + 2 * size));
            _nodes[1] = new JellyVertex(new Vector(pos.X + 0 * size, pos.Y + 2 * size));
            _nodes[2] = new JellyVertex(new Vector(pos.X + 2 * size, pos.Y + 2 * size));
            _nodes[3] = new JellyVertex(new Vector(pos.X + -1 * size, pos.Y + 1 * size));
            _nodes[4] = new JellyVertex(new Vector(pos.X + 1 * size, pos.Y + 1 * size));
            _nodes[5] = new JellyVertex(new Vector(pos.X + -2 * size, pos.Y + 0 * size));
            _nodes[6] = new JellyVertex(new Vector(pos.X + 0 * size, pos.Y + 0 * size));
            _nodes[7] = new JellyVertex(new Vector(pos.X + 2 * size, pos.Y + 0 * size));
            _nodes[8] = new JellyVertex(new Vector(pos.X + -1 * size, pos.Y + -1 * size));
            _nodes[9] = new JellyVertex(new Vector(pos.X + 1 * size, pos.Y + -1 * size));
            _nodes[10] = new JellyVertex(new Vector(pos.X + -2 * size, pos.Y + -2 * size));
            _nodes[11] = new JellyVertex(new Vector(pos.X + 0 * size, pos.Y + -2 * size));
            _nodes[12] = new JellyVertex(new Vector(pos.X + 2 * size, pos.Y + -2 * size));

            //distance links
            double KS = JellyWorld.KS;
            double KD = JellyWorld.KD;

            _disprelinks = new DisPreLink[28];
            _disprelinks[0] = new DisPreLink(_nodes[0], _nodes[1], KS, KD);
            _disprelinks[1] = new DisPreLink(_nodes[1], _nodes[2], KS, KD);
            _disprelinks[2] = new DisPreLink(_nodes[2], _nodes[7], KS, KD);
            _disprelinks[3] = new DisPreLink(_nodes[7], _nodes[12], KS, KD);
            _disprelinks[4] = new DisPreLink(_nodes[12], _nodes[11], KS, KD);
            _disprelinks[5] = new DisPreLink(_nodes[11], _nodes[10], KS, KD);
            _disprelinks[6] = new DisPreLink(_nodes[10], _nodes[5], KS, KD);
            _disprelinks[7] = new DisPreLink(_nodes[5], _nodes[0], KS, KD);
            _disprelinks[8] = new DisPreLink(_nodes[1], _nodes[6], KS, KD);
            _disprelinks[9] = new DisPreLink(_nodes[6], _nodes[11], KS, KD);
            _disprelinks[10] = new DisPreLink(_nodes[6], _nodes[5], KS, KD);
            _disprelinks[11] = new DisPreLink(_nodes[7], _nodes[6], KS, KD);
            _disprelinks[12] = new DisPreLink(_nodes[3], _nodes[0], KS, KD);
            _disprelinks[13] = new DisPreLink(_nodes[3], _nodes[1], KS, KD);
            _disprelinks[14] = new DisPreLink(_nodes[3], _nodes[6], KS, KD);
            _disprelinks[15] = new DisPreLink(_nodes[3], _nodes[5], KS, KD);
            _disprelinks[16] = new DisPreLink(_nodes[4], _nodes[1], KS, KD);
            _disprelinks[17] = new DisPreLink(_nodes[4], _nodes[2], KS, KD);
            _disprelinks[18] = new DisPreLink(_nodes[4], _nodes[7], KS, KD);
            _disprelinks[19] = new DisPreLink(_nodes[4], _nodes[6], KS, KD);
            _disprelinks[20] = new DisPreLink(_nodes[8], _nodes[5], KS, KD);
            _disprelinks[21] = new DisPreLink(_nodes[8], _nodes[6], KS, KD);
            _disprelinks[22] = new DisPreLink(_nodes[8], _nodes[11], KS, KD);
            _disprelinks[23] = new DisPreLink(_nodes[8], _nodes[10], KS, KD);
            _disprelinks[24] = new DisPreLink(_nodes[9], _nodes[6], KS, KD);
            _disprelinks[25] = new DisPreLink(_nodes[9], _nodes[7], KS, KD);
            _disprelinks[26] = new DisPreLink(_nodes[9], _nodes[12], KS, KD);
            _disprelinks[27] = new DisPreLink(_nodes[9], _nodes[11], KS, KD);

            //area links
            double KA = JellyWorld.KA;

            _areprelinks = new ArePreLink[16];
            _areprelinks[0] = new ArePreLink(_nodes[0], _nodes[3], _nodes[1], KA, _disprelinks[12].NegNormal, _disprelinks[13].PosNormal, _disprelinks[0].NegNormal);
            _areprelinks[1] = new ArePreLink(_nodes[3], _nodes[6], _nodes[1], KA, _disprelinks[14].PosNormal, _disprelinks[8].NegNormal, _disprelinks[13].NegNormal);
            _areprelinks[2] = new ArePreLink(_nodes[6], _nodes[3], _nodes[5], KA, _disprelinks[14].NegNormal, _disprelinks[15].PosNormal, _disprelinks[10].NegNormal);
            _areprelinks[3] = new ArePreLink(_nodes[3], _nodes[0], _nodes[5], KA, _disprelinks[12].PosNormal, _disprelinks[7].NegNormal, _disprelinks[15].NegNormal);
            _areprelinks[4] = new ArePreLink(_nodes[4], _nodes[2], _nodes[1], KA, _disprelinks[17].PosNormal, _disprelinks[1].NegNormal, _disprelinks[16].NegNormal);
            _areprelinks[5] = new ArePreLink(_nodes[4], _nodes[7], _nodes[2], KA, _disprelinks[18].PosNormal, _disprelinks[2].NegNormal, _disprelinks[17].NegNormal);
            _areprelinks[6] = new ArePreLink(_nodes[4], _nodes[6], _nodes[7], KA, _disprelinks[19].PosNormal, _disprelinks[11].NegNormal, _disprelinks[18].NegNormal);
            _areprelinks[7] = new ArePreLink(_nodes[6], _nodes[4], _nodes[1], KA, _disprelinks[19].NegNormal, _disprelinks[16].PosNormal, _disprelinks[8].PosNormal);
            _areprelinks[8] = new ArePreLink(_nodes[8], _nodes[6], _nodes[5], KA, _disprelinks[21].PosNormal, _disprelinks[10].PosNormal, _disprelinks[20].NegNormal);
            _areprelinks[9] = new ArePreLink(_nodes[6], _nodes[8], _nodes[11], KA, _disprelinks[21].NegNormal, _disprelinks[22].PosNormal, _disprelinks[9].NegNormal);
            _areprelinks[10] = new ArePreLink(_nodes[8], _nodes[10], _nodes[11], KA, _disprelinks[23].PosNormal, _disprelinks[5].NegNormal, _disprelinks[22].NegNormal);
            _areprelinks[11] = new ArePreLink(_nodes[8], _nodes[5], _nodes[10], KA, _disprelinks[20].PosNormal, _disprelinks[6].NegNormal, _disprelinks[23].NegNormal);
            _areprelinks[12] = new ArePreLink(_nodes[6], _nodes[9], _nodes[7], KA, _disprelinks[24].NegNormal, _disprelinks[25].PosNormal, _disprelinks[11].PosNormal);
            _areprelinks[13] = new ArePreLink(_nodes[9], _nodes[12], _nodes[7], KA, _disprelinks[26].PosNormal, _disprelinks[3].NegNormal, _disprelinks[25].NegNormal);
            _areprelinks[14] = new ArePreLink(_nodes[9], _nodes[11], _nodes[12], KA, _disprelinks[27].PosNormal, _disprelinks[4].NegNormal, _disprelinks[26].NegNormal);
            _areprelinks[15] = new ArePreLink(_nodes[9], _nodes[6], _nodes[11], KA, _disprelinks[24].PosNormal, _disprelinks[9].PosNormal, _disprelinks[27].NegNormal);

            //edges
            _edge[0] = _nodes[0];
            _edge[1] = _nodes[1];
            _edge[2] = _nodes[2];
            _edge[3] = _nodes[7];
            _edge[4] = _nodes[12];
            _edge[5] = _nodes[11];
            _edge[6] = _nodes[10];
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
        }
    }
}
