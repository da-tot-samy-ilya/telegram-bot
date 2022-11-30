using telegram_bot.tinder.enums;

namespace telegram_bot.bot
{
    public class Answer : Message
    {
        public bool refreshThePage;
        public int oldMessageId;
        public Dictionary<string, string> keyBoard;

        public Answer(bool refreshThePage, int oldMessageId, int messageId, long userId,
            MessageType type, string text = "", string photoId = "", 
            Dictionary<string, string> keyBoard = null ): base(messageId, userId, type, text,photoId)
        {
            this.refreshThePage = refreshThePage;
            this.oldMessageId = oldMessageId;
            this.keyBoard = keyBoard;
        }

    }
}
