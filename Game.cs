namespace telegram_bot
{
    public class Game
    {
        public int leftBorder = 0;
        public int rightBorder = 1;
        public int choosedNumber = 0;

        public Game(int LeftBorder, int RightBorder, int Number)
        {
            leftBorder = LeftBorder;
            rightBorder = RightBorder;
            choosedNumber = Number;
        }
    }
};

