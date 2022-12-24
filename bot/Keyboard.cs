using Telegram.Bot.Types.ReplyMarkups;

namespace telegram_bot.bot
{
    public class Keyboard
    {
        public ReplyKeyboardMarkup GenerateKeyboard( int rows, int columns, string[] keyboard )
        {
            var rowsButton = new List<List<KeyboardButton>>();
            int iterator = 0;

            for (var i = 0; i < rows; i++)
            {
                var buttons = new List<KeyboardButton>();
                for (var j = 0; j < columns; j++)
                {
                    var button = keyboard[iterator];
                    buttons.Add(new KeyboardButton(button));
                    iterator++;
                }
                rowsButton.Add(buttons);
            }
            return new ReplyKeyboardMarkup(rowsButton);
        }
    }
}
