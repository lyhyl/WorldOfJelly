using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WOJ_IdealGas.Spring;

namespace WOJ_IdealGas.JellyObjects
{
    public class JellyBox : JellyObject
    {
        /// <summary>
        /// Only available in construction
        /// </summary>
        private float Size { set; get; }
        public JellyBox(float size, float mass)
            : base()
        {
            _vertexcount = 8;
            Size = size;
            Mass = mass;

            int i;

            int lc = 2;
            float subsize = size / 2;

            _vertices = new JellyVertex[8];
            for (i = 0; i < lc; ++i)
            {
                _vertices[i] = new JellyVertex(size - i * subsize, 0);
                _vertices[i + lc] = new JellyVertex(0, i * subsize);
                _vertices[i + (lc << 1)] = new JellyVertex(i * subsize, size);
                _vertices[i + lc * 3] = new JellyVertex(size, size - i * subsize);
            }

            _edge_springs = new EdgeSpring[8];
            for (i = 0; i < _vertexcount - 1; ++i)
                AddEdgeSpring(i, i, i + 1);
            AddEdgeSpring(i, i, 0);

            _internal_springs = new InternalSpring[2];
            AddInternalSpring(0, 1, 5);
            AddInternalSpring(1, 3, 7);

            Pressure = 10000000f;
            KS = 5000f;
            KD = 200f;
        }
    }
}
