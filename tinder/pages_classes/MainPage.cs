using telegram_bot.bot;
using telegram_bot.bot.enums;
using telegram_bot.data_base;

namespace telegram_bot.tinder.pages_classes
{
    public class MainPage : Page
    {
        public MainPage(UsersDb usersDb, string imgId = ""): base(usersDb)
        {
            this.imgId = imgId;
            rowsCount = 3;
            columnsCount = 2;
            text = "Hi! In that bot u can find people, with who u will be comfortable." +
                   "There are some buttons, u can control the bot by them.";
            keyboard = new Dictionary<string, string>
            {
                ["Show people"] = "/show_people",
                ["Matches"] = "/matches",
                ["Sent likes"] = "/sent_likes",
                ["Received likes"] = "/received_likes",
                ["Settings"] = "/settings",
                ["Edit profile"] = "/edit_profile",
            };
            
        }

        public override Answer getAnswer(Message message, int oldMessage)
        {
            return new Answer(true, true,
                message.user, BotMessageType.text, text, "", keyboard, rowsCount, columnsCount);
        }
    }

}
     