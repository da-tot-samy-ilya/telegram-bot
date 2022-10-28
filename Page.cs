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
        protected InlineKeyboardMarkup inlineKeyboard;
        protected string text;

        public InlineKeyboardMarkup getKeyBoard()
        {
            return this.inlineKeyboard;
        }

        public abstract Request getAnswer( bool refreshThePage, BotUser user, Request message, int messageId ); 
    }
}
