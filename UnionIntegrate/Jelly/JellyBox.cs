namespace UnionIntegrate
{
    public class JellyBox : JellyObject
    {
        public JellyBox(double mass, double size, Vector pos)
            : base(mass, size, 4)
        {
            //nodes
            _nodes = new JellyVertex[5];
            _nodes[0] = new JellyVertex(new Vector(pos.X + -1 * size, pos.Y + 1 * size));
            _nodes[1] = new JellyVertex(new Vector(pos.X + 1 * size, pos.Y + 1 * size));
            _nodes[2] = new JellyVertex(new Vector(pos.X + 0 * size, pos.Y + 0 * size));
            _nodes[3] = new JellyVertex(new Vector(pos.X + -1 * size, pos.Y + -1 * size));
            _nodes[4] = new JellyVertex(new Vector(pos.X + 1 * size, pos.Y + -1 * size));

            //distance links
            double KS = JellyWorld.KS;
            double KD = JellyWorld.KD;

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
            double KA = JellyWorld.KA;

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
        }
    }
}
