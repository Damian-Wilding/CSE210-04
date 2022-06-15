namespace Unit04.Game.Casting
{
    
    public class Gem : Actor
    {
        private int points = 1;

        
        public Gem()
        {
        }

        
        public int GetPoints()
        {
            return points;
        }

        
        public void SetScore(int points, int total)
        {
            total += points;
        }
    }
}