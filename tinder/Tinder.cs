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

        public Answer GetAnswer(Message message, int oldMessage)
        {
            var currentPage = pages.GetPageByEnum(message.user.onWhichPage);
            if (message.type == BotMessageType.incorrectType)
            {
                return pages.GetPageByEnum(message.user.onWhichPage).getAnswer(message, oldMessage);
            }
            var currentPageEnum = pages.GetPageEnumByCommand(message.text);

            if (currentPageEnum != PagesEnum.not_page)
            {
                message.user.onWhichPage = currentPageEnum;
                message.user.localStatus = pages.GetUserLocalStatusEnumByEnum(currentPageEnum);
                usersDb.Update(message.user.id, message.user);
               
                currentPage = pages.GetPageByEnum(currentPageEnum);
            }
           
            return currentPage.getAnswer(message, oldMessage);
        }
    }
}
