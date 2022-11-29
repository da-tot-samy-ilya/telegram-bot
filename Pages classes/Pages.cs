using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;
using telegram_bot.Pages_classes;

namespace telegram_bot
{
    public class Pages
    {
        public MainPage _mainPage;

        public Pages()
        {
            _mainPage = new MainPage();
            // TODO: добавить страницы в конструктор
        }
    }
}