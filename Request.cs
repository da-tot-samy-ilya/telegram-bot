using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;


namespace telegram_bot
{
    public class Request // мб изменить название
    {
        // TODO: нужно настроить класс
        public string text;
        public long photoId;
        public MessageType type;
        public bool refreshThePage;

        public Request( string userText, MessageType userType, long userPhotoId = 0, bool userRefreshThePage = false )
        {
            text = userText;
            photoId = userPhotoId;
            type = userType;
            refreshThePage = userRefreshThePage;
        }
    }
}
