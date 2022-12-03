using telegram_bot.bot;
using telegram_bot.bot.enums;
using telegram_bot.data_base;
using telegram_bot.tinder.enums;
using telegram_bot.tinder.pages_classes;

namespace telegram_bot.tinder
{
    public class Tinder
    {
        public Pages pages;
        public UsersDb usersDb;

        public Tinder(Pages pages, UsersDb usersDb)
        {
            this.pages = pages;
            this.usersDb = usersDb;
        }

        public Answer GetAnswer(BotUser user, Message message, int oldMessage)
        {
            /*return new Answer(false, oldMessage, message.messageId, user.id, 
                BotMessageType.text,"Hey","", pages._mainPage.getKeyboard(),
                pages._mainPage.row, pages._mainPage.column);*/
           /* // если пользователь перемещается между страницами
            // зачем тогда ему заходить в switch? 
            // если message не в дб, а message != user.status && message in dbPages
            if (user.onWhichPage.ToString() != message.text) // TODO: добавить бд 
            {
                // TODO: пока это пример, нужно переписать: user.onWhichPage = message.text; 
                refreshThePage = true;
                // TODO: изменить статус пользователя (т.е. его страницу)
            }

            // если просто перход между страницами, то не для каждой страницы нужен getAnswer (ex: main)

            return user.onWhichPage.getAnswer(refreshThePage, user, message, message.Id);*/
           var currentPageEnum = pages.GetPageEnumByCommand(message.text);
           if (currentPageEnum != PagesEnum.not_page)
           {
               user.onWhichPage = currentPageEnum;
               usersDb.Update(user.id, user);
           }

           var currentPage = pages.getPageByEnum(currentPageEnum);

           return currentPage.getAnswer(user, message, oldMessage);

        }
    }
}
