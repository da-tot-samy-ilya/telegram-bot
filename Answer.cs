using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using telegram_bot.enums;

namespace telegram_bot
{
    public class Answer: Message
    {
        public bool refreshThePage;
        public int oldMessageId;
        //public Dictionary<string, string> keyBoard;

        public Answer(bool userRefreshThePage, int oldMessageId, int messageId, long userId, 
            MessageType userType, string userText = "", string userPhotoId = "" )
        {
            this.refreshThePage = userRefreshThePage;
            this.oldMessageId = oldMessageId;
            this.messageId = messageId;
            this.userId = userId;
            this.text = userText;
            this.photoId = userPhotoId;
            this.type = userType;
        }

    }
}
