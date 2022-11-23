using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace telegram_bot
{
    public abstract class Page
    {
        private Dictionary<string, string> keyboard { get; set; }
        private string text { get; set; }
        private string imgId { get; set; }


        /*public InlineKeyboardMarkup getKeyBoard()
        {
            return this.inlineKeyboard;
        }*/

        //public abstract Request getAnswer( bool refreshThePage, BotUser user, Request message, int messageId ); 
    }
}
