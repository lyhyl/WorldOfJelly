using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace UnionPreColl
{
    static public class JellyWorld
    {
        //physic
        public static double Gravity { get { return -400; } }//-400
        public static double Friciton { get { return 0.9; } }
        public static double KS { get { return 240; } }//org : 40 , 160 - 320
        public static double KD { get { return 5; } }//org : 5 , this value is perfect...others don't work
        public static double KA { get { return 10000; } }//org : 10000
        //describe
        private static int _left = -200;
        private static int _right = 200;
        private static int _top = 1000;
        private static int _bottom = -300;
        public static int WorldLeft { get { return _left; } set { _left = value; } }
        public static int WorldRight { get { return _right; } set { _right = value; } }
        public static int WorldTop { get { return _top; } set { _top = value; } }
        public static int WorldBottom { get { return _bottom; } set { _bottom = value; } }
        //limit
        public static double MaximumVelocity { get { return 1000; } }
        public static double MaximumVelocitySq { get { return MaximumVelocity * MaximumVelocity; } }
    }
}
