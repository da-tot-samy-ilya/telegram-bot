using telegram_bot.bot.enums;

namespace telegram_bot.bot
{
    public class Answer : Message
    {
        public bool isToUpdateLastMessage;
        public bool isToGenerateKeyboard;
        public int oldMessageId;
        public Dictionary<string, string> keyBoard;
        public int rowsCount;
        public int columnsCount;

        public Answer(bool isToUpdateLastMessage, bool isToGenerateKeyboard, int oldMessageId, int messageId, long userId,
            BotMessageType type, string text = "", string photoId = "", 
            Dictionary<string, string> keyBoard = null, int rowsCount = 0, int columnsCount = 0)
            : base(messageId, userId, type, text, photoId)
        {
            this.isToUpdateLastMessage = isToUpdateLastMessage;
            this.isToGenerateKeyboard = isToGenerateKeyboard;
            this.oldMessageId = oldMessageId;
            this.keyBoard = keyBoard;
            this.rowsCount = rowsCount;
            this.columnsCount = columnsCount;
        }

    }
}
