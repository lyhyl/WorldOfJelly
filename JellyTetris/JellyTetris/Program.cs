namespace JellyTetris
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (JellyTetris game = new JellyTetris())
            {
                game.Run();
            }
        }
    }
#endif
}

