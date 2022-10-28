using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;


namespace telegram_bot
{
    public class Request: Message // мб изменить название
    {
        // TODO: нужно настроить класс
        Message message = new Message();

        public Request()
        {
            // message.Text
            // add photoId
            // add typeOfMessage
            // add refreshThePage / обновить страницу?
        }
    }
}
