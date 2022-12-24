using telegram_bot.bot.enums;

namespace telegram_bot.bot
{
    public class Answer : Message
    {
        public bool isToUpdateLastMessage;
        public bool isToGenerateKeyboard;
        public Dictionary<string, string> keyBoard;
        public int rowsCount;
        public int columnsCount;
        public string[] keyboard;
        public bool isClickCancelButton;

        public Answer(bool isToUpdateLastMessage, bool isToGenerateKeyboard, BotUser user,
            BotMessageType type, string text = "", string photoId = "", 
            Dictionary<string, string> keyBoard = null, int rowsCount = 0, int columnsCount = 0,
            string[] keyboard = null, bool isClickCancelButton = false)
            : base(user, type, text, photoId)
        {
            this.isToUpdateLastMessage = isToUpdateLastMessage;
            this.isToGenerateKeyboard = isToGenerateKeyboard;
            this.keyBoard = keyBoard;
            this.rowsCount = rowsCount;
            this.columnsCount = columnsCount;
            this.keyboard = keyboard;
            this.isClickCancelButton= isClickCancelButton;
        }

    }
}
