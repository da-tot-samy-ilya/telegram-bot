using telegram_bot.bot.enums;
using telegram_bot.tinder.enums;

namespace telegram_bot.bot
{
    public class Answer : Message
    {
        public bool refreshThePage;
        public int oldMessageId;
        public Dictionary<string, string> keyBoard;
        public int row;
        public int column;

        public Answer(bool refreshThePage, int oldMessageId, int messageId, long userId,
            BotMessageType type, string text = "", string photoId = "", 
            Dictionary<string, string> keyBoard = null, int row = 0, int column = 0)
            : base(messageId, userId, type, text,photoId)
        {
            this.refreshThePage = refreshThePage;
            this.oldMessageId = oldMessageId;
            this.keyBoard = keyBoard;
            this.row = row;
            this.column = column; 
        }

    }
}
