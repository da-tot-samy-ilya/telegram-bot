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

        public Request getAnswerByPage( BotUser user, Request message )
        {
            /*// если пользователь перемещается между страницами
            // зачем тогда ему заходить в switch? 
            // если message не в дб, а message != user.status && message in dbPages
            if (message in dbPages) // TODO: добавить бд 
            {
                // TODO: изменить статус пользователя (т.е. его страницу)
            }

            // если просто перход между страницами, то не для каждой страницы нужен getAnswer (ex: main)
            switch (user.status) // TODO: добавить поля в user
            {
                case "/main": // нужен ли main?
                    return _mainPage.getAnswer();
                case "/edit_profile":
                    return null;
                default:
                    return null;

            }*/
            return null;
        }


    }
}
