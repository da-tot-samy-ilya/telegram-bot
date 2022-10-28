using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Requests;

namespace telegram_bot
{
    public class Tinder
    {
        public MainPage _mainPage;// TODO: добавить остальные страницы сюда

        public Tinder()
        {
            _mainPage = new MainPage();
            // TODO: добавить страницы в конструктор
        }

        public Message getAnswerByPage(BotUser user, Message message)
        {
            // если пользователь перемещается между страницами
            if (message in dbPages) // TODO: добавить бд 
            {
                // TODO: изменить статус пользователя (т.е. его страницу)
            }

            switch (user.status) // TODO: добавить поля в user
            {
                case "/main":
                    return _mainPage.getAnswer();
                case "/edit_profile":
                    return null;
                default:
                    return null;

            }
        }


    }
}
