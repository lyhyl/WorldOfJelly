using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WOJ_IdealGas.Spring
{
    public class EdgeSpring
    {
        public int IndexA { set; get; }
        public int IndexB { set; get; }
        public float Length { set; get; }

        private VectorF _nor_vec = new VectorF(0, 0);
        public VectorF NormalVector { set { _nor_vec = value; } get { return _nor_vec; } }
        public float NVX { set { _nor_vec.X = value; } get { return _nor_vec.X; } }
        public float NVY { set { _nor_vec.Y = value; } get { return _nor_vec.Y; } }
    }
}
