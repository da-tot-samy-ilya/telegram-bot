using telegram_bot.tinder.enums;

namespace telegram_bot.bot
{
    public class Answer : Message
    {
        public bool refreshThePage;
        public int oldMessageId;
        //public Dictionary<string, string> keyBoard;

        public Answer(bool userRefreshThePage, int oldMessageId, int messageId, long userId,
            MessageType userType, string userText = "", string userPhotoId = "")
        {
            refreshThePage = userRefreshThePage;
            this.oldMessageId = oldMessageId;
            this.messageId = messageId;
            this.userId = userId;
            text = userText;
            photoId = userPhotoId;
            type = userType;
        }

    }
}
