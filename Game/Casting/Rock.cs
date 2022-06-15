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

        
        public void SetScore(int points, int total)
        {
            total -= points;
        }
    }
}