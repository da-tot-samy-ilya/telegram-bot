using telegram_bot.bot;
using telegram_bot.data_base;

namespace telegram_bot.tinder.pages_classes
{
    public abstract class Page
    {
        protected Dictionary<string, string> keyboard;
        protected UsersDb _usersDb;
        protected string text;
        protected string imgId;
        public int rowsCount { get; protected set; }
        public int columnsCount { get; protected set; }

        public Dictionary<string, string> getKeyboard()
        {
            return keyboard;
        }

        public Page(UsersDb usersDb)
        {
            _usersDb = usersDb;
        }

        public abstract Answer getAnswer(BotUser user, Message message, int oldMessage);
    }
}
