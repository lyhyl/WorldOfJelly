using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace UnionIntegrate
{
    abstract public class JellyObject
    {
        public static bool DrawBase { set; get; }
        public static bool DrawExtend { set; get; }
        public static bool DrawCurve { set; get; }

        public double Mass { set; get; }
        public double Size { set; get; }
        protected DisPreLink[] _disprelinks;
        protected ArePreLink[] _areprelinks;
        protected JellyVertex[] _nodes;

        protected JellyVertex[] _edge;
        public JellyVertex[] Edge { get { return _edge; } }
        protected Vector[] _edgenor;
        public Vector[] EdgeNormal { get { return _edgenor; } }

        private Vector[] _extendedge;

        protected JellyObject(double mass, double size, uint edgevc)
        {
            Mass = mass;
            Size = size;
            _edge = new JellyVertex[edgevc];
            _edgenor = new Vector[edgevc];
            _extendedge = new Vector[edgevc];
        }

        private void IntegrateVertex(JellyVertex jv, double delay)
        {
            jv.Integrate(delay, Mass);

            if (jv.Position.Y < JellyWorld.WorldBottom)
            {
                jv.VY = -jv.VY * JellyWorld.Friciton;
                jv.Y = JellyWorld.WorldBottom;
                jv.VX *= JellyWorld.Friciton;
            }
            if (jv.Position.Y > JellyWorld.WorldTop)
            {
                jv.VY = -jv.VY * JellyWorld.Friciton;
                jv.Y = JellyWorld.WorldTop;
                jv.VX *= JellyWorld.Friciton;
            }
            if (jv.Position.X < JellyWorld.WorldLeft)
            {
                jv.VX = -jv.VX * JellyWorld.Friciton;
                jv.X = JellyWorld.WorldLeft;
                jv.VY *= JellyWorld.Friciton;
            }
            if (jv.Position.X > JellyWorld.WorldRight)
            {
                jv.VX = -jv.VX * JellyWorld.Friciton;
                jv.X = JellyWorld.WorldRight;
                jv.VY *= JellyWorld.Friciton;
            }
        }

        public void Preservation()
        {
            foreach (JellyVertex v in _nodes)
                v.NxtFY += Mass * JellyWorld.Gravity;
            foreach (DisPreLink dpl in _disprelinks)
                dpl.DistancePreservation();
            foreach (ArePreLink apl in _areprelinks)
                apl.AreaPreservation();
        }

        public void Integrate(double delay)
        {
            foreach (JellyVertex v in _nodes)
                IntegrateVertex(v, delay);
        }

        public bool Select(Vector mpos, out int id, out double u, out double v)
        {
            double unused;
            id = 0;
            foreach (ArePreLink apl in _areprelinks)
                if (JMath.PointTriangle(mpos, apl.jva.Position, apl.jvb.Position, apl.jvc.Position, out u, out v, out unused))
                    return true;
                else
                    ++id;
            u = -1;
            v = -1;
            id = int.MaxValue;
            return false;
        }

        public void AddForceAt(int aplid, double u, double v, Vector mpos, double ks)
        {
            if (aplid < _areprelinks.Length)
            {
                double w = 1 - u - v;
                Vector target = _areprelinks[aplid].jva.Position * u + _areprelinks[aplid].jvb.Position * v + _areprelinks[aplid].jvc.Position * w;
                Vector diff = mpos - target;
                Vector force = diff * ks;
                _areprelinks[aplid].jva.NxtForce += force;
                _areprelinks[aplid].jvb.NxtForce += force;
                _areprelinks[aplid].jvc.NxtForce += force;
            }
        }

        public void DebugDraw(Graphics g)
        {
            Pen redpen = new Pen(Color.Red);
            Pen bluepen = new Pen(Color.Blue);
            Pen greenpen = new Pen(Color.Green);

            const double NorSize = 4;

            CreateExtendEdgeBuffer(NorSize);

            DrawBaseEdge(g, bluepen, greenpen, NorSize);
            DrawExtendEdge(g, greenpen, NorSize);
            DrawCurveEdge(g, greenpen);
        }

        private void CreateExtendEdgeBuffer(double NorSize)
        {
            int vc = 1;
            while (vc < _edgenor.Length)
            {
                _extendedge[vc] = (_edgenor[vc - 1] + _edgenor[vc]) * NorSize;
                ++vc;
            }
            _extendedge[0] = (_edgenor[vc - 1] + _edgenor[0]) * NorSize;

            vc = 0;
            while (vc < _edgenor.Length)
            {
                _extendedge[vc] += _edge[vc].Position;
                ++vc;
            }
        }

        private void DrawCurveEdge(Graphics g, Pen greenpen)
        {
            if (DrawCurve)
            {
                const double thr = 1.0 / 3.0;
                const double dthr = 2.0 / 3.0;

                Vector[] divv = new Vector[_extendedge.Length * 3];

                Vector diff, frt, snd;
                int i = 0;
                while (i < _extendedge.Length - 1)
                {
                    diff = _extendedge[i + 1] - _extendedge[i];
                    frt = diff * thr + _extendedge[i];
                    snd = diff * dthr + _extendedge[i];
                    divv[i * 3] = _extendedge[i];
                    divv[i * 3 + 1] = frt;
                    divv[i * 3 + 2] = snd;
                    ++i;
                }
                diff = _extendedge[0] - _extendedge[i];
                frt = diff * thr + _extendedge[i];
                snd = diff * dthr + _extendedge[i];
                divv[i * 3] = _extendedge[i];
                divv[i * 3 + 1] = frt;
                divv[i * 3 + 2] = snd;

                const int iter = 3;

                List<Vector> ckv = new List<Vector>();
                int j = 0;
                while (j < divv.Length - 2)
                {
                    JMath.ComputeBezierPoint(divv[j], divv[j + 1], divv[j + 2], ref ckv, iter);
                    ++j;
                }
                JMath.ComputeBezierPoint(divv[j], divv[j + 1], divv[0], ref ckv, iter);
                ++j;
                JMath.ComputeBezierPoint(divv[j], divv[0], divv[1], ref ckv, iter);

                int k = 0;
                while (k < ckv.Count - 1)
                {
                    g.DrawLine(greenpen, ckv[k], ckv[k + 1]);
                    ++k;
                }
                g.DrawLine(greenpen, ckv[k], ckv[0]);
            }
        }

        private void DrawExtendEdge(Graphics g, Pen greenpen, double NorSize)
        {
            if (DrawExtend)
            {
                int vc = 0;
                while (vc < _edgenor.Length - 1)
                {
                    g.DrawLine(greenpen, _extendedge[vc], _extendedge[vc + 1]);
                    ++vc;
                }
                g.DrawLine(greenpen, _extendedge[vc], _extendedge[0]);
            }
        }

        private void DrawBaseEdge(Graphics g, Pen bluepen, Pen greenpen, double NorSize)
        {
            if (DrawBase)
            {
                int vc = 0;
                Vector mid;
                while (vc < _edge.Length - 1)
                {
                    mid = (_edge[vc].Position + _edge[vc + 1].Position) * 0.5;
                    g.DrawLine(bluepen, mid, mid + _edgenor[vc] * NorSize);
                    ++vc;
                }
                mid = (_edge[vc].Position + _edge[0].Position) * 0.5;
                g.DrawLine(bluepen, mid, mid + _edgenor[vc] * NorSize);

                int i = 0;
                while (i < _edge.Length - 1)
                {
                    g.DrawLine(greenpen, _edge[i].Position, _edge[i + 1].Position);
                    ++i;
                }
                g.DrawLine(greenpen, _edge[i].Position, _edge[0].Position);
            }
        }
    }
}
