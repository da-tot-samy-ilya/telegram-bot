using telegram_bot.bot;
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
            var currentPageEnum = pages.GetPageEnumByCommand(message.text);
           if (currentPageEnum != PagesEnum.not_page)
           {
               user.onWhichPage = currentPageEnum;
               usersDb.Update(user.id, user);
           }

           var currentPage = pages.GetPageByEnum(currentPageEnum);

           return currentPage.getAnswer(user, message, oldMessage);

        }
    }
}
