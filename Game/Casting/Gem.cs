namespace Unit04.Game.Casting
{
    
    public class Gem : Actor
    {
        private int points = 1;

        
        public Gem()
        {
        }

        
        public string GetPoints()
        {
            return points;
        }

        
        public void SetScore(int points)
        {
            score += points;
        }
    }
}