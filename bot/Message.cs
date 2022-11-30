using telegram_bot.tinder.enums;

namespace telegram_bot.bot
{
    public class Message // мб изменить название на Message
    {
        // TODO: нужно настроить класс
        public int messageId;
        public long userId;
        public string text;
        public string photoId;
        public MessageType type;

        public Message(int messageId, long userId, MessageType type, string text = "",
            string photoId = "")
        {
            this.messageId = messageId;
            this.userId = userId;
            this.text = text;
            this.photoId = photoId;
            this.type = type;
        }
    }
}
