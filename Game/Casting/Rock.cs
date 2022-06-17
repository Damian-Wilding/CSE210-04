namespace Unit04.Game.Casting
{
    
    public class Rock : Actor
    {
        private int points = 1;

        
        public Rock()
        {
        }

        
        public int GetPoints()
        {
            return points;
        }

        
        public int SetScore(int total)
        {
            total -= points;
            return total;
        }
    }
}