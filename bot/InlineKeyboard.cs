using Telegram.Bot.Types.ReplyMarkups;

namespace telegram_bot.bot
{
    public class InlineKeyboard
    {
        public InlineKeyboardMarkup GenerateKeyboard(int rows, int columns, Dictionary<string, string> keyboard)
        {
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
            return new InlineKeyboardMarkup(rowsButton);
        }
    }
}
