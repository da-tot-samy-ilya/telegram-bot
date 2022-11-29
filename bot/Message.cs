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

        public Message(int messageId, long userId, MessageType userType, string userText = "",
            string userPhotoId = "")
        {
            this.messageId = messageId;
            this.userId = userId;
            text = userText;
            photoId = userPhotoId;
            type = userType;
        }
    }
}
