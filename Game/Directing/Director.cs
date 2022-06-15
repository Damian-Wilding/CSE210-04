using System.Collections.Generic;
using Unit04.Game.Casting;
using Unit04.Game.Services;
using System.IO;
using System.Linq;
//using Unit04.Game.Gem;
//using Unit04.Game.Rock;


namespace Unit04.Game.Directing
{
    /// <summary>
    /// <para>A person who directs the game.</para>
    /// <para>
    /// The responsibility of a Director is to control the sequence of play.
    /// </para>
    /// </summary>
    public class Director
    {
        public int score = 0;
        private KeyboardService keyboardService = null;
        private VideoService videoService = null;

        /// <summary>
        /// Constructs a new instance of Director using the given KeyboardService and VideoService.
        /// </summary>
        /// <param name="keyboardService">The given KeyboardService.</param>
        /// <param name="videoService">The given VideoService.</param>
        public Director(KeyboardService keyboardService, VideoService videoService)
        {
            this.keyboardService = keyboardService;
            this.videoService = videoService;
        }

        /// <summary>
        /// Starts the game by running the main game loop for the given cast.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        public void StartGame(Cast cast)
        {
            videoService.OpenWindow();
            while (videoService.IsWindowOpen())
            {
                GetInputs(cast);
                DoUpdates(cast);
                DoOutputs(cast);
            }
            videoService.CloseWindow();
        }

        /// <summary>
        /// Gets directional input from the keyboard and applies it to the robot.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        private void GetInputs(Cast cast)
        {
            Actor player = cast.GetFirstActor("player");
            Point velocity = keyboardService.GetDirection();
            player.SetVelocity(velocity);     
        }

        /// <summary>
        /// Updates the robot's position and resolves any collisions with artifacts.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        private void DoUpdates(Cast cast)
        {
            Actor banner = cast.GetFirstActor("banner");
            Actor player = cast.GetFirstActor("player");
            
            List<Actor> rocks = cast.GetActors("rocks");
            List<Actor> gems = cast.GetActors("gems");

            banner.SetNewScore(score);
            int maxX = videoService.GetWidth();
            int maxY = videoService.GetHeight();
            player.MoveNext(maxX, maxY);
            //rock.Movenext()

            //iterates through rocks
            foreach (Actor actor in rocks)
            {
                //handles the score if the player touches a rock
                if (player.GetPosition().Equals(actor.GetPosition()))
                {
                    Rock rock = (Rock) actor;
                    //int points = rock.GetPoints();
                    this.score = rock.SetScore(this.score, 1);
                    //banner.SetText(message);  
                }
                actor.MoveNext(maxX, maxY);
            }
            
            //iterates though gems
            foreach (Actor actor in gems)
            {
                //handles the score if the player touches a gem
                if (player.GetPosition().Equals(actor.GetPosition()))
                {
                    Gem gem = (Gem) actor;
                    //int points = rock.GetPoints();
                    this.score = gem.SetScore(this.score, 1);
                    //banner.SetText(message);
                }
                actor.MoveNext(maxX, maxY);
            }

            System.Random random = new System.Random();
            for (int i = 0; i < 1; i++)
            {
                // string text = ((char)random.Next(33, 34)).ToString();
                //string message = score;

                int x = random.Next(1, Program.COLS);
                int y = maxY;
                Point position = new Point(x, y);
                position = position.Scale(Program.CELL_SIZE);

                int r = random.Next(0, 256);
                int g = random.Next(0, 256);
                int b = random.Next(0, 256);
                Color color = new Color(r, g, b);

                Rock rock = new Rock();
                rock.SetText("0");
                rock.SetFontSize(Program.FONT_SIZE);
                rock.SetVelocity(new Point(0, 1));
                rock.SetColor(color);
                rock.SetPosition(position);
                // rock.SetMessage(message); //score
                cast.AddActor("rocks", rock);
                
            }
            for (int i = 0; i < 1; i++)
            {
                // string text = ((char)random.Next(33, 34)).ToString();
                //string message = score;

                int x = random.Next(1, Program.COLS);
                int y = maxY;
                Point position = new Point(x, y);
                position = position.Scale(Program.CELL_SIZE);

                int r = random.Next(0, 256);
                int g = random.Next(0, 256);
                int b = random.Next(0, 256);
                Color color = new Color(r, g, b);

                Gem gem = new Gem();
                gem.SetText("*");
                gem.SetFontSize(Program.FONT_SIZE);
                gem.SetVelocity(new Point(0, 1));
                gem.SetColor(color);
                gem.SetPosition(position);
                // rock.SetMessage(message); //score
                cast.AddActor("gems", gem);
            }
            //foreach (Actor actor in rocks)
            //{
            //    MoveNext(maxX, maxY);
            //}
        }

        /// <summary>
        /// Draws the actors on the screen.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        public void DoOutputs(Cast cast)
        {
            List<Actor> actors = cast.GetAllActors();
            videoService.ClearBuffer();
            videoService.DrawActors(actors);
            videoService.FlushBuffer();
        }

    }
}