using telegram_bot.bot;
using telegram_bot.bot.enums;

namespace telegram_bot.tinder.pages_classes
{
    public class EditProfile : Page
    {
        public EditProfile(string text = "", string imgId = "")
        {
            this.text = text;
            row = 2;
            column = 4;

            keyboard = new Dictionary<string, string>
            {
                ["На главную"] = "main",
                ["Имя"] = "set_name",
                ["Описание"] = "set_describtion",
                ["Интересы"] = "set_interests",
                ["Цель поиска"] = "set_search_purpose",
                ["Фото"] = "set_images",
                ["Город"] = "set_town",
                ["Возраст"] = "set_age",
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

