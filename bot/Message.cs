using telegram_bot.bot.enums;

namespace telegram_bot.bot
{
    public class Message
    {
        public BotUser user;
        public string text;
        public string photoId;
        public BotMessageType type;

        public Message(BotUser user, BotMessageType type, string text = "",
            string photoId = "")
        {
            this.user = user;
            this.text = text;
            this.photoId = photoId;
            this.type = type;
        }
    }
}