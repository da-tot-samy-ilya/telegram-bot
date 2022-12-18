using telegram_bot.bot;
using telegram_bot.bot.enums;
using telegram_bot.data_base;

namespace telegram_bot.tinder.pages_classes
{
    public class JokesGenerator : Page
    {
        public JokesGenerator(UsersDb usersDb): base(usersDb)
        {
            rowsCount = 1;
            columnsCount = 2;
            text = "In this page u can get random joke for your conversations.";
            keyboard = new Dictionary<string, string>
            {
                ["Main"] = "/main",
                ["New joke"] = "/new_joke",
            };
        }

        public override Answer getAnswer(Message message, int oldMessage)
        {
            if (message.type == BotMessageType.command)
            {
                if (message.text == "/new_joke")
                {
                    var joke = GetJoke().Result;
                    return new Answer(true, true,
                        message.user, BotMessageType.text, joke, "", keyboard, rowsCount, columnsCount);
                }
            }
            return GenerateDefaultPage(message);
        }

        private Answer GenerateDefaultPage(Message message)
        {
            return new Answer(true, true,
                message.user, BotMessageType.text, text, "", keyboard, rowsCount, columnsCount);
        }

        private async Task<String> GetJoke()
        {
            var httpClient = new HttpClient();
            var joke = await httpClient.GetStringAsync("https://geek-jokes.sameerkumar.website/api?format=text");
            return joke.Substring(1, joke.Length-3);
        }
    }
};

