using Telegram.Bot.Types.ReplyMarkups;

namespace telegram_bot.bot
{
    public class InlineKeyboard
    {
        public Dictionary<string, string> keyboard;

        public InlineKeyboardMarkup inlineKeyboard;

        // TODO: добавить enum, в зависимоти от которого будет разными видеми разсположения кнопок
        public InlineKeyboard(Dictionary<string, string> keyboard)
        {
            this.keyboard = keyboard;
            //var countOfKey = keyboard.Count;
        }
        public InlineKeyboardMarkup GetInlineKeyboard()
        {
            return inlineKeyboard;
        }

        public void GenerateKeyboard(int rows, int columns)
        {
            // TODO: возможно стоит перенести в другую функцию
            var rowsButton = new List<List<InlineKeyboardButton>>();

            int iterator = 0;
            var keys = keyboard.Keys.ToArray();

            for (var i = 0; i < rows; i++)
            {
                var buttons = new List<InlineKeyboardButton>();
                for (var j = 0; j < columns; j++)
                {
                    var button = keys[iterator];
                    buttons.Add(InlineKeyboardButton.WithCallbackData(text: button, callbackData: keyboard[button]));
                    iterator++;
                }
                rowsButton.Add(buttons);
            }
            inlineKeyboard = new InlineKeyboardMarkup(rowsButton);
        }
    }
}
