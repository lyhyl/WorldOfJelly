using System;
using System.Collections.Generic;
using JellyTetris.JellyWorld.Graphics;
using JellyTetris.JellyWorld.Math;
using JellyTetris.JellyWorld.Physic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace JellyTetris.JellyWorld.Jelly
{
    public abstract class JellyObject : JellyGraphics.JellyXNADraw
    {
        public float Mass { set; get; }
        public float Size { set; get; }

        private JellyFace _face;
        protected int _face_node_id;

        private Effect _jellyeffect;

        protected List<SoundEffectInstance> sounds = new List<SoundEffectInstance>();
        protected int lastPlayId = -1;
        protected bool speaking = false;

        protected DisPreLink[] _disprelinks;
        protected ArePreLink[] _areprelinks;
        protected JellyVertex[] _nodes;
        public JellyVertex[] Nodes { get { return _nodes; } }

        protected JellyVertex[] _edge;
        public JellyVertex[] Edge { get { return _edge; } }
        protected JellyVector2[] _edgenor;
        public JellyVector2[] EdgeNormal { get { return _edgenor; } }

        private JellyVector2[] _extedgebuffer;
        private JellyVector2[] _divedgebuffer;
        private JellyVector2[] _crvedgebuffer;

        protected int[] _extverid;

        protected int[] _cellNodes;
        public int[] Cells { get { return _cellNodes; } }
        protected GraphicsDevice _graphicsDevice;

        protected static Random random = new Random();

        protected JellyObject(GraphicsDevice gd, float mass, float size, Color color, int edge_vtx_count)
            : base(gd, color)
        {
            Mass = mass;
            Size = size;
            _graphicsDevice = gd;

            _color = color;
            _jellyeffect = ResourceManager.LoadSetupEffect("Effect/JellyColor");
            _jellyeffect.Parameters[1].SetValue(1);

            _edge = new JellyVertex[edge_vtx_count];
            _edgenor = new JellyVector2[edge_vtx_count];

            _extedgebuffer = new JellyVector2[edge_vtx_count];
            _divedgebuffer = new JellyVector2[edge_vtx_count * SegDivCount];
            _crvedgebuffer = new JellyVector2[edge_vtx_count * SegDivCount * DivideIter];

            _face = new JellyFace("Jelly/JellyFaceA", 64);
            _face.Frame = 0;

            LoadSounds();
        }

        private void LoadSounds()
        {
            sounds.Add(ResourceManager.LoadVoice("Sound/JellyVoice/Laugh"));
            sounds.Add(ResourceManager.LoadVoice("Sound/JellyVoice/OhBaBa"));
        }

        public void PlayVoice()
        {
            if (!speaking)
            {
                speaking = true;
                sounds[lastPlayId = random.Next(sounds.Count)].Play();
            }
            else if(sounds[lastPlayId].State==SoundState.Stopped)
            {
                lastPlayId = -1;
                speaking = false;
            }
        }

        public void Gravity()
        {
            foreach (JellyVertex v in _nodes)
                v.FY += Mass * JellyWorld.Gravity;
        }

        public void Preservation()
        {
            foreach (DisPreLink dpl in _disprelinks)
                dpl.DistancePreservation();
            foreach (ArePreLink apl in _areprelinks)
                apl.AreaPreservation();
        }

        public void Integrate(float delay)
        {
            foreach (JellyVertex v in _nodes)
                UpdateVertex(v, delay);
        }

        private void UpdateVertex(JellyVertex jv, float delay)
        {
            jv.Integrate(delay, Mass);
            jv.Restrict();
        }

        public bool Select(JellyVector2 mpos, out int id, out float u, out float v, out float w)
        {
            id = 0;
            foreach (ArePreLink apl in _areprelinks)
                if (JellyMath.PointTriangle(mpos, apl.jva.Position, apl.jvb.Position, apl.jvc.Position, out u, out v, out w))
                {
                    _face.Frame = 1;
                    return true;
                }
                else
                    ++id;
            u = -1;
            v = -1;
            w = -1;
            id = int.MaxValue;

            _face.Frame = 0;
            return false;
        }

        public void AddForceAt(int aplid, float u, float v, JellyVector2 mpos, float ks)
        {
            if (aplid < _areprelinks.Length)
            {
                float w = 1 - u - v;
                JellyVector2 target = _areprelinks[aplid].jva.Position * u + _areprelinks[aplid].jvb.Position * v + _areprelinks[aplid].jvc.Position * w;
                JellyVector2 diff = new JellyVector2(mpos) - target;
                JellyVector2 force = diff * ks;
                _areprelinks[aplid].jva.Force += force;
                _areprelinks[aplid].jvb.Force += force;
                _areprelinks[aplid].jvc.Force += force;
            }
        }

        public void Catch(int aplid, float u, float v, float w, JellyVector2 mpos)
        {
            if (aplid < _areprelinks.Length)
            {
                JellyVector2 target = _areprelinks[aplid].jva.Position * u + _areprelinks[aplid].jvb.Position * v + _areprelinks[aplid].jvc.Position * w;
                JellyVector2 diff = new JellyVector2(mpos) - target;
                JellyVector2 tdiff = diff * 40;
                JellyVector2 hdiff = diff * 0.5f;

                /*foreach (JellyVertex jv in _nodes)
                {
                    jv.Position += hdiff;
                    jv.Velocity += tdiff;
                    jv.Force += tdiff;
                    jv.Velocity *= JellyWorld.Friciton;
                }*/

                /*_areprelinks[aplid].jva.Position += hdiff;
                _areprelinks[aplid].jvb.Position += hdiff;
                _areprelinks[aplid].jvc.Position += hdiff;*/

                _areprelinks[aplid].jva.Force += tdiff;
                _areprelinks[aplid].jvb.Force += tdiff;
                _areprelinks[aplid].jvc.Force += tdiff;

                _areprelinks[aplid].jva.Velocity *= JellyWorld.Friciton;
                _areprelinks[aplid].jvb.Velocity *= JellyWorld.Friciton;
                _areprelinks[aplid].jvc.Velocity *= JellyWorld.Friciton;

                PlayVoice();

                _face.Frame = 2;
            }
        }

        public void UpdateDraw()
        {
            CreateExtendEdgeBuffer();

            CreateDivEdgeBuffer();

            CreateCurveEdgeBuffer();

            UpdateXNAVertex();
        }

        private void CreateExtendEdgeBuffer()
        {
            int vc;
            JellyVector2 diff;
            for (vc = 1; vc < _edgenor.Length; ++vc)
            {
                diff = _edgenor[vc - 1] + _edgenor[vc];
                diff.Normalize();
                _extedgebuffer[vc] = diff * NorSize;
            }
            diff = _edgenor[vc - 1] + _edgenor[0];
            diff.Normalize();
            _extedgebuffer[0] = diff * NorSize;

            for (vc = 0; vc < _edgenor.Length; ++vc)
                _extedgebuffer[vc] += _edge[vc].Position;
        }

        private void CreateDivEdgeBuffer()
        {
            const float thr = 1.0f / 3.0f;
            const float dthr = 2.0f / 3.0f;

            JellyVector2 diff, frt, snd;
            int eec = 0;
            while (eec < _extedgebuffer.Length - 1)
            {
                diff = _extedgebuffer[eec + 1] - _extedgebuffer[eec];
                frt = diff * thr + _extedgebuffer[eec];
                snd = diff * dthr + _extedgebuffer[eec];
                _divedgebuffer[eec * SegDivCount] = _extedgebuffer[eec];
                _divedgebuffer[eec * SegDivCount + 1] = frt;
                _divedgebuffer[eec * SegDivCount + 2] = snd;
                ++eec;
            }
            diff = _extedgebuffer[0] - _extedgebuffer[eec];
            frt = diff * thr + _extedgebuffer[eec];
            snd = diff * dthr + _extedgebuffer[eec];
            _divedgebuffer[eec * SegDivCount] = _extedgebuffer[eec];
            _divedgebuffer[eec * SegDivCount + 1] = frt;
            _divedgebuffer[eec * SegDivCount + 2] = snd;
        }

        private void CreateCurveEdgeBuffer()
        {
            int cvc = 0;
            int startindex = 0;
            while (cvc < _divedgebuffer.Length - 2)
            {
                JellyMath.ComputeBezierPoint(_divedgebuffer[cvc], _divedgebuffer[cvc + 1], _divedgebuffer[cvc + 2], ref _crvedgebuffer, ref startindex, DivideIter);
                ++cvc;
            }
            JellyMath.ComputeBezierPoint(_divedgebuffer[cvc], _divedgebuffer[cvc + 1], _divedgebuffer[0], ref _crvedgebuffer, ref startindex, DivideIter);
            ++cvc;
            JellyMath.ComputeBezierPoint(_divedgebuffer[cvc], _divedgebuffer[0], _divedgebuffer[1], ref _crvedgebuffer, ref startindex, DivideIter);
        }

        public void Draw()
        {
            _jellyeffect.Parameters[2].SetValue(_color.ToVector3());
            base.Draw(_jellyeffect);

            _face.Position = new Vector2(
                _nodes[_face_node_id].Position.X + (ControlSystem.SpaceWidth >> 1),
                -_nodes[_face_node_id].Position.Y + (ControlSystem.SpaceHeight >> 1));
            _face.Draw();
        }

        private void UpdateXNAVertex()
        {
            int i = 0;
            for (i = 1; i < _crvedgebuffer.Length; ++i)
                SetVertexPosition(i, _crvedgebuffer[i - 1]);
            SetVertexPosition(0, _crvedgebuffer[i - 1]);
            foreach (int id in _extverid)
                SetVertexPosition(i++, _nodes[id].Position);

            FlushVertex();
        }

#if DEBUG
        /*
         * For Develop
         * Remove In Release
         * */
        protected void GetTransformUV(float w, float h)
        {
            CreateExtendEdgeBuffer();
            CreateDivEdgeBuffer();
            CreateCurveEdgeBuffer();
            JellyVector2[] tuv = new JellyVector2[_buffervertexcount];
            int i = 1;
            for (i = 1; i < _crvedgebuffer.Length; ++i)
                tuv[i] = _crvedgebuffer[i - 1];
            tuv[0] = _crvedgebuffer[i - 1];
            foreach (int id in _extverid)
                tuv[i++] = _nodes[id].Position;
            for (int j = 0; j < tuv.Length; ++j)
            {
                tuv[j].X /= w;
                tuv[j].Y /= h;
            }
            string s = "";
            int idi = 0;
            foreach (JellyVector2 jv in tuv)
                s += "SetVertexUV(" + idi++ + "," + jv.X + "f," + jv.Y + "f);\n";
        }
#endif

        public abstract JellyObject[] Convert(int[] cid);
    }
}
