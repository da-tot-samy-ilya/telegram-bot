using Telegram.Bot.Types.ReplyMarkups;

namespace telegram_bot.bot
{
    public class InlineKeyboard
    {
        public Dictionary<string, string> keyboard;

        InlineKeyboardMarkup[][] inlineKeyboard;

        // TODO: добавить enum, в зависимоти от которого будет разными видеми разсположения кнопок
        public InlineKeyboard(Dictionary<string, string> keyboard)
        {
            this.keyboard = keyboard;
            //var countOfKey = keyboard.Count;
        }
        public InlineKeyboardMarkup[][] GetInlineKeyboard()
        {
            return inlineKeyboard;
        }

        public InlineKeyboardMarkup[][] GenerateKeyboard()
        {
            // TODO: возможно стоит перенести в другую функцию
            var keyboardInline = new InlineKeyboardMarkup[1][]; // TODO: допилить функцию для разных случаев
            //var keyboardButtons = new InlineKeyboardButton[countOfKey];
            var i = 0;
            foreach (var button in keyboard)
            {
                keyboardInline[0][i] = new[] { InlineKeyboardButton.WithCallbackData(text: button.Key, callbackData: button.Value) };
                i++;
            }
            inlineKeyboard = keyboardInline;
            return inlineKeyboard;
        }

        // TODO: мб стоит сюда добавить функцию для вывода клавиатуры пользователю?
    }
}
