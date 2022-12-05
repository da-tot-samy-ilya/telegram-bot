using telegram_bot.bot;
using telegram_bot.bot.enums;

namespace telegram_bot.tinder.pages_classes
{
    public class EditProfile : Page
    {
        public EditProfile(string text = "", string imgId = "")
        {
            this.text = text;
            rowsCount = 2;
            columnsCount = 4;

            keyboard = new Dictionary<string, string>
            {
                ["To main"] = "/main",
                ["Name"] = "/set_name",
                ["Description"] = "/set_describtion",
                ["Interests"] = "/set_interests",
                ["Search purpose"] = "/set_search_purpose",
                ["Photo"] = "/set_images",
                ["Town"] = "/set_town",
                ["Age"] = "/set_age",
            };
        }
        public override Answer getAnswer(BotUser user, Message message, int oldMessage)
        {
            if (message.type == BotMessageType.text)
            {
                
            }
            else if (message.type == BotMessageType.img)
            {
                
            }
        }
    }
}

