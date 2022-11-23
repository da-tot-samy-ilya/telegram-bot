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

        public Message(int messageId, long userId, MessageType userType, string userText = "", 
            string userPhotoId = "")
        {
            this.messageId = messageId;
            this.userId = userId;
            this.text = userText;
            this.photoId = userPhotoId;
            this.type = userType;
        }
    }
}
