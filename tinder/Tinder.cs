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
            //если отправил вообще левый кринж типа геолокации или гс, сразу отправляем на текущую страницу
            if (message.type == BotMessageType.incorrectType)
            {
                return pages.GetPageByEnum(user.onWhichPage).getAnswer(user, message, oldMessage);
            }
            var currentPageEnum = pages.GetPageEnumByCommand(message.text);
           if (currentPageEnum != PagesEnum.not_page)
           {
               user.onWhichPage = currentPageEnum;
               user.localStatus = pages.GetUserLocalStatusEnumByEnum(currentPageEnum);
               usersDb.Update(user.id, user);
           }

           var currentPage = pages.GetPageByEnum(currentPageEnum);

           return currentPage.getAnswer(user, message, oldMessage);

        }
    }
}
