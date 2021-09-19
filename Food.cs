namespace WormsApplication
{
    public class Food
    {
        private readonly int _x;
        private readonly int _y;
        private int _age;

        public Food(int x, int y)
        {
            _x = x;
            _y = y;
            _age = 1;
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
            return _age;
        }

        public void IncreaseAge()
        {
            _age++;
        }
    }
}