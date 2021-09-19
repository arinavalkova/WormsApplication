namespace WormsApplication
{
    public class Worm
    {
        private readonly string _name;
        private int _x;
        private int _y;
        private int _vitality;

        public Worm(string name)
        {
            _name = name;
            _x = 0;
            _y = 0;
            _vitality = 10;
        }

        public Worm(string name, int x, int y)
        {
            _name = name;
            _x = x;
            _y = y;
            _vitality = 10;
        }

        public Worm(string name, int x, int y, int vitality)
        {
            _name = name;
            _x = x;
            _y = y;
            _vitality = vitality;
        }

        public int GetX()
        {
            return _x;
        }

        public int GetY()
        {
            return _y;
        }

        public string GetName()
        {
            return _name;
        }

        public void SetX(int x)
        {
            _x = x;
        }

        public void SetY(int y)
        {
            _y = y;
        }

        public int GetVitality()
        {
            return _vitality;
        }

        public void IncreaseVitality(int vitality)
        {
            _vitality += vitality;
        }

        public void DecreaseVitality(int vitality)
        {
            _vitality -= vitality;
        }
    }
}