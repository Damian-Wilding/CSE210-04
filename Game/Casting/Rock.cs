namespace Unit04.Game.Casting
{
    
    public class Rock : Actor
    {
        private int points = 1;

        
        public Rock()
        {
        }

        
        public string GetPoints()
        {
            return points;
        }

        
        public void SetScore(int points, int total)
        {
            total -= points;
        }
    }
}