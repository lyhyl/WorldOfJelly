using JellyTetris.JellyWorld.Math;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JellyTetris.JellyWorld.JellyGraphics
{
    public class JellyXNADraw
    {
        protected const float NorSize = 4;
        protected const int DivideIter = 2;
        protected const int SegDivCount = 3;
        private const int DmS = DivideIter * SegDivCount;

        protected int _vertexcount, _extendcount, _trianglecount, _buffervertexcount;

        private GraphicsDevice _graphicsdevice;
        private VertexPositionTexture[] _vertexlist;
        private DynamicVertexBuffer _vertexbuffer;
        private IndexBuffer _indexbuffer;

        protected Color _color;
        protected Texture2D _texture;

        protected JellyXNADraw(GraphicsDevice gd, Color color)
        {
            _graphicsdevice = gd;
            _color = color;
        }

        /*
         * Extend Vertex Index
         */
        protected short ExtVIdx(int basevc, int id)
        {
            return (short)(basevc * DmS + id);
        }

        /*
         * Edge Vertex Index
         */
        protected short EgeVIdx(short id)
        {
            return (short)(id * DmS);
        }

        protected void InitXNA(int ext_vc, short[] tri_fin_vid_list, short[] ext_tri_list)
        {
            _vertexcount = tri_fin_vid_list.Length;
            _extendcount = ext_vc;

            _trianglecount = _vertexcount * DmS + ext_tri_list.Length / 3;

            _buffervertexcount = _vertexcount * DmS + _extendcount;

            //init vertex buffer
            _vertexlist = new VertexPositionTexture[_buffervertexcount];
            _vertexbuffer = new DynamicVertexBuffer(_graphicsdevice, typeof(VertexPositionTexture), _buffervertexcount, BufferUsage.WriteOnly);

            //init each one
            for (int i = 0; i < _buffervertexcount; ++i)
                _vertexlist[i] = new VertexPositionTexture(Vector3.Zero, Vector2.Zero);

            //init index buffer
            _indexbuffer = new IndexBuffer(_graphicsdevice, IndexElementSize.SixteenBits, _trianglecount * 3, BufferUsage.WriteOnly);

            short[] indexs = new short[_trianglecount * 3];

            int w_p = 0;//current write position
            int gvc;
            //base
            for (gvc = 0; gvc < _vertexcount; ++gvc)
                for (int subgvc = 0; subgvc < DmS; ++subgvc)
                {
                    indexs[w_p++] = (short)(gvc * DmS + subgvc);
                    indexs[w_p++] = (short)(gvc * DmS + subgvc + 1);
                    indexs[w_p++] = tri_fin_vid_list[gvc];
                }
            indexs[w_p - 2] = 0;//cycle
            //extend
            gvc = 0;
            while (gvc < ext_tri_list.Length)
            {
                /*indexs[w_p++] = ext_tri_list[gvc++];
                indexs[w_p++] = ext_tri_list[gvc++];*/
                indexs[w_p++] = ext_tri_list[gvc++];
            }

            _indexbuffer.SetData(indexs);
        }

        protected void SetVertexPosition(int id, JellyVector2 pos)
        {
            _vertexlist[id].Position.X = pos.X;
            _vertexlist[id].Position.Y = pos.Y;
        }

        protected void SetVertexUV(int id, float u, float v)
        {
            _vertexlist[id].TextureCoordinate.X = u;
            _vertexlist[id].TextureCoordinate.Y = v;
        }

        protected void FlushVertex()
        {
            _vertexbuffer.SetData(_vertexlist, 0, _vertexlist.Length, SetDataOptions.NoOverwrite);
        }

        protected void Draw(Effect effect)
        {
            _graphicsdevice.Textures[0] = _texture;
            _graphicsdevice.SetVertexBuffer(_vertexbuffer);
            _graphicsdevice.Indices = _indexbuffer;
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                _graphicsdevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, _buffervertexcount, 0, _trianglecount);
            }
        }
    }
}
