using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SiteRecover
{
    public class Vertex
    {
        public Vector Position { set; get; }

        public Vertex(Vector pos)
        {
            Position = pos;
        }
        public Vertex(float posx,float posy)
        {
            Position = new Vector(posx, posy);
        }
    }
}
