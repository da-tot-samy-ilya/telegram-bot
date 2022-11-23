using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace telegram_bot
{
    public class InlineKeyboard
    {
        public Dictionary<string, string> keyboard;

        // TODO: добавить enum, в зависимоти от которого будет разными видеми разсположения кнопок
        public InlineKeyboard(Dictionary<string, string> keyboard)
        {
            this.keyboard = keyboard;
        }
        public InlineKeyboardMarkup[][] GetInlineKeyboard( Dictionary<string, string> keyboard) // TODO: допилить функцию для разных случаев
        {
            var countOfKey = keyboard.Count;

            var keyboardInline = new InlineKeyboardMarkup[1][];
            var keyboardButtons = new InlineKeyboardButton[countOfKey];
            var i = 0;
            foreach (var button in keyboard)
            {
                keyboardInline[0][i] = new[] { InlineKeyboardButton.WithCallbackData(text: button.Key, callbackData: button.Value) };
                i++;
            }
            return keyboardInline;
        }
        
    }
}
