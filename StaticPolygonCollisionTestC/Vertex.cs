using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace StaticPolygonCollisionTestC
{
    public class Vertex
    {
        static public Vector CreateOffest { set; get; }
        public Vector Position { set; get; }

        public Vertex(Vector pos)
        {
            Position = pos + new Vector(CreateOffest);
        }
        public Vertex(float posx,float posy)
        {
            Position = new Vector(posx, posy) + new Vector(CreateOffest);
        }
    }
}
