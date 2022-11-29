namespace telegram_bot.tinder.pages_classes
{
    public class MainPage : Page
    {

        public MainPage()
        {


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



        }

        /*public override Request getAnswer( bool refreshThePage, BotUser user, Request message, int messageId ) =>
            new Request(messageId, MessageType.text, text, "", refreshThePage);*/
    }
}
