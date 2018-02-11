namespace UnionIntegrate
{
    static public class JellyWorld
    {
        //physic
        public const double Gravity = -400;//-400
        public const double Friciton = 0.9;
        public const double KS = 240;//org : 240 , 160 - 320
        public const double KD = 5;//org : 5 , this value is perfect...others don't work
        public const double KA = 10000;//org : 10000

        public const double DefUnitMass = 0.5;
        //describe
        public static int WorldLeft = -200;
        public static int WorldRight = 200;
        public static int WorldTop = 1000;
        public static int WorldBottom = -300;
        //limit
        public const double MaximumVelocity = 500;
        public const double MaximumVelocitySq = MaximumVelocity * MaximumVelocity;
    }
}
