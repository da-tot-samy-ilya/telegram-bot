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
        public string photoId;
        public MessageType type;
        public bool refreshThePage;

        public Request( MessageType userType, string userText = "", string userPhotoId = 0, bool userRefreshThePage = false )
        {
            text = userText;
            photoId = userPhotoId;
            type = userType;
            refreshThePage = userRefreshThePage;
        }
    }
}
