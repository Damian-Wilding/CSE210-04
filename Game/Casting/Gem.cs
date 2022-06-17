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

        
        public int SetScore(int total)
        {
            total += points;
            return total;
        }
    }
}