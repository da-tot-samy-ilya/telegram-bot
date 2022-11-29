namespace telegram_bot.tinder.pages_classes
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
