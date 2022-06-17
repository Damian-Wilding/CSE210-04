using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unit04.Game.Casting;
using Unit04.Game.Directing;
using Unit04.Game.Services;


namespace Unit04
{
    /// <summary>
    /// The program's entry point.
    /// </summary>
    class Program
    {
        public static int FRAME_RATE = 60;
        public static int MAX_X = 900;
        public static int MAX_Y = 600;
        public static int CELL_SIZE = 15;
        public static int FONT_SIZE = 15;
        public static int COLS = 60;
        public static int ROWS = 40;
        public static string CAPTION = "Greed";
        //private static string DATA_PATH = "Data/messages.txt";
        public static Color WHITE = new Color(255, 255, 255);
        //private static int DEFAULT_ARTIFACTS = 40;


        /// <summary>
        /// Starts the program using the given arguments.
        /// </summary>
        /// <param name="args">The given arguments.</param>
        static void Main(string[] args)
        {
            // create the cast
            Cast cast = new Cast();

            // create the banner
            Actor banner = new Actor();
            banner.SetText("0");
            banner.SetFontSize(FONT_SIZE);
            banner.SetColor(WHITE);
            banner.SetPosition(new Point(CELL_SIZE, 0));
            cast.AddActor("banner", banner);

            // create the player
            Actor player = new Actor();
            player.SetText("#");
            player.SetFontSize(FONT_SIZE);
            player.SetColor(WHITE);
            //not sure why I had to set the y value below to 585. 600 (maxY) just puts it back on the top of the screen.
            player.SetPosition(new Point(MAX_X / 2, 585));
            cast.AddActor("player", player);

            // start the game
            KeyboardService keyboardService = new KeyboardService(CELL_SIZE);
            VideoService videoService 
                = new VideoService(CAPTION, MAX_X, MAX_Y, CELL_SIZE, FRAME_RATE, false);
            Director director = new Director(keyboardService, videoService);
            director.StartGame(cast);
        }
    }
}