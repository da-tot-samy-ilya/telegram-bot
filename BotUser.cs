using telegram_bot.enums;

namespace telegram_bot
{
    public class BotUser
    {
        public GameStatus GameStatus;
        public readonly long Id;
        public readonly string Name;
        public readonly Game GameProps;
        public BotUser(GameStatus status, long id, string name, Game gameProps)
        {
            GameStatus = status;
            Id = id;
            Name = name;
            GameProps = gameProps;
        }
    }
};

