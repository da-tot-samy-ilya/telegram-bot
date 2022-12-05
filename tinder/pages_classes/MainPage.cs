using telegram_bot.bot;
using telegram_bot.bot.enums;

namespace telegram_bot.tinder.pages_classes
{
    public class MainPage : Page
    {
        public MainPage(string imgId = "")
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

        public override Answer getAnswer(BotUser user, Message message, int oldMessage)
        {
            return new Answer(true, true, oldMessage, 0,
                user.id, BotMessageType.text, text, "", keyboard, rowsCount, columnsCount);
        }
    }

}

/*inlineKeyboard = new InlineKeyboardMarkup(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData(text: "Показать людей", callbackData: "/show_people"),
                InlineKeyboardButton.WithCallbackData(text: "Мэтчи", callbackData: "/matches"),
            },

            new[]
            {
                InlineKeyboardButton.WithCallbackData(text: "Отправленные лайки", callbackData: "/sent_likes"),
                InlineKeyboardButton.WithCallbackData(text: "Полученный лайки", callbackData: "/received_likes"),
            },

            new[]
            {
                InlineKeyboardButton.WithCallbackData(text: "Настройки", callbackData: "/settings"),

            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData(text: "Редактировать профиль", callbackData: "/edit_profile"),
            }

        });*/

/*public override Request getAnswer( bool refreshThePage, BotUser user, Request message, int messageId ) =>
           new Request(messageId, MessageType.text, text, "", refreshThePage);*/