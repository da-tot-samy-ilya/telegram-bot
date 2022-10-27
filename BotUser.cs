using telegram_bot.enums;

namespace telegram_bot
{
    public class BotUser
    {
        public GameStatus GameStatus;
        public readonly long Id;
        public readonly string Name;
        public BotUser(GameStatus status, long id, string name)
        {
            GameStatus = status;
            Id = id;
            Name = name;
        }
    }
};

