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
        private void moveGemsandRocks(Cast cast)
        {
            List<Actor> rocks = cast.GetActors("rocks");
            List<Actor> gems = cast.GetActors("gems");
            int maxX = videoService.GetWidth();
            int maxY = videoService.GetHeight();
            foreach (Actor actor in rocks)
            {
                actor.MoveNext(maxX, maxY);
            }
            
            foreach (Actor actor in gems)
            {
                actor.MoveNext(maxX, maxY);
            }
        }
        private void HandleCollision(Cast cast)
        {
            List<Actor> rocks = cast.GetActors("rocks");
            List<Actor> gems = cast.GetActors("gems");
            Actor player = cast.GetFirstActor("player");
            Actor banner = cast.GetFirstActor("banner");
            foreach (Actor actor in rocks)
            {
                //handles the score if the player touches a rock
                if (player.GetPosition().Equals(actor.GetPosition()))
                {
                    //I'm sure this could be simplified by frick it, I've done enough.
                    Rock rock = (Rock) actor;
                    System.Console.WriteLine(banner.GetText());
                    int BannerAsINT = int.Parse(banner.GetText());
                    int NewTotal = rock.SetScore(BannerAsINT);
                    string NewTotalAsString = NewTotal.ToString();
                    banner.SetText(NewTotalAsString);
                    cast.RemoveActor("rocks", actor);
                }
            }
            foreach (Actor actor in gems)
            {
                //handles the score if the player touches a gem
                if (player.GetPosition().Equals(actor.GetPosition()))
                {
                    Gem gem = (Gem) actor;
                    System.Console.WriteLine(banner.GetText());
                    int BannerAsINT = int.Parse(banner.GetText());
                    int NewTotal = gem.SetScore(BannerAsINT);
                    string NewTotalAsString = NewTotal.ToString();
                    banner.SetText(NewTotalAsString);
                    cast.RemoveActor("gems", actor);
                }
            }
        }
        private void HandleOutOfScreenActors(Cast cast)
        {
            List<Actor> rocks = cast.GetActors("rocks");
            List<Actor> gems = cast.GetActors("gems");
            int maxY = videoService.GetHeight();
            foreach (Actor actor in rocks)
            {
                
                if (actor.GetPosition().GetY() > maxY)
                {
                    cast.RemoveActor("rocks", actor);
                }
            }
            foreach (Actor actor in gems)
            {
                
                if (actor.GetPosition().GetY() > maxY)
                {
                    cast.RemoveActor("gems", actor);
                }
            }
        }

        private void SpawnNewRocksAndGems(Cast cast)
        {
            
            System.Random random = new System.Random();
            // I don't think that it's good to have the game make a new rock and gem each game loop
            // So I'm just gonna make it a new random value that determines if we are going to generate rocks and gems or not
            int randoNumber = random.Next(0, 8);
            if (randoNumber == 1)
            {
                for (int i = 0; i < 1; i++)
                {
                    int x = random.Next(1, Program.COLS);
                    int y = 0;
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
                    cast.AddActor("rocks", rock);

                }
            }

            if (randoNumber == 2)
            {
                for (int i = 0; i < 1; i++)
                {
                    int x = random.Next(1, Program.COLS);
                    int y = 0;
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
                    cast.AddActor("gems", gem);
                }
            }
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
            //Actor banner = cast.GetFirstActor("banner");
            Actor player = cast.GetFirstActor("player");
            
            List<Actor> rocks = cast.GetActors("rocks");
            List<Actor> gems = cast.GetActors("gems");

            //banner.SetNewScore(score, );
            int maxX = videoService.GetWidth();
            int maxY = videoService.GetHeight();
            player.MoveNext(maxX, maxY);
           
            moveGemsandRocks(cast);
            HandleCollision(cast);
            HandleOutOfScreenActors(cast);
            SpawnNewRocksAndGems(cast);
            //System.Console.WriteLine(score);
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