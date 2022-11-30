using telegram_bot.bot;

namespace telegram_bot.tinder.pages_classes
{
    public abstract class Page
    {
        protected Dictionary<string, string> keyboard;
        protected string text;
        protected string imgId;
        public int row { get; protected set; }
        public int column { get; protected set; }

        public Dictionary<string, string> getKeyboard()
        {
            return keyboard;
        }

        public abstract Answer getAnswer();
    }
}
