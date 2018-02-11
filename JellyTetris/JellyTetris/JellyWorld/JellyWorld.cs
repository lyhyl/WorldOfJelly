namespace JellyTetris.JellyWorld
{
    static public class JellyWorld
    {
        //physic
        public static float Gravity { get { return -400; } }
        public static float Friciton { get { return 0.8f; } }
        public static float KS { get { return 320; } }//org : 40 , 160 - 320
        public static float KD { get { return 5; } }//org : 5
        public static float KA { get { return 10000; } }
        //describe
        private static int _left = -200;
        private static int _right = 200;
        private static int _top = 450;
        private static int _bottom = -300;
        public static int WorldLeft { get { return _left; } set { _left = value; } }
        public static int WorldRight { get { return _right; } set { _right = value; } }
        public static int WorldTop { get { return _top; } set { _top = value; } }
        public static int WorldBottom { get { return _bottom; } set { _bottom = value; } }
        //limit
        public static float MaximumVelocity { get { return 500; } }
        public static float MaximumVelocitySq { get { return MaximumVelocity * MaximumVelocity; } }
    }
}
