using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;


namespace telegram_bot
{
    public class Message
    {
        public string text;
        public string imgId;
        public MessageType type;
        //public InlineKeyboardMarkup keyBoard;

        public Message(string text, string imgId, MessageType type)
        {
            this.text = text;
            this.imgId = imgId;
            this.type = type;
            
        }

    }
}
