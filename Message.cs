using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;


namespace telegram_bot
{
    public class Message // мб изменить название на Message
    {
        // TODO: нужно настроить класс
        public int messageId;
        public long userId;
        public string text;
        public string photoId;
        public MessageType type;
        public bool refreshThePage;

        public Message( int mesId, long usId, MessageType userType, string userText = "", 
            string userPhotoId = "", bool userRefreshThePage = false )
        {
            this.messageId = mesId;
            this.userId = usId;
            this.text = userText;
            this.photoId = userPhotoId;
            this.type = userType;
            this.refreshThePage = userRefreshThePage;
        }
    }
}
