namespace WormsApplication
{
    public class Food
    {
        private readonly int _x;
        private readonly int _y;
        private int _expiresIn;
        private static readonly int MaxAge = 10;

        public Food(int x, int y)
        {
            _x = x;
            _y = y;
            _expiresIn = MaxAge;
        }
        public int GetX()
        {
            return _x;
        }

        public int GetY()
        {
            return _y;
        }

        public int GetAge()
        {
            return MaxAge - _expiresIn;
        }

        public void IncreaseAge()
        {
            _expiresIn--;
        }

        public int GetExpiresIn()
        {
            return _expiresIn;
        }
    }
}