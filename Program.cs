using telegram_bot.bot;
using telegram_bot.data_base;
using telegram_bot.tinder;
using telegram_bot.tinder.pages_classes;
using Telegram.Bot;

namespace telegram_bot
{
    public class Program
    {
        public static void Main()
        {
            var db = new UsersDb("users", "json");
            var botClient = new TelegramBotClient(GetToken());
            var cts = new CancellationTokenSource();
            
            var mainPage = new MainPage(db);
            var editProfile = new EditProfile(db);
            var pages = new Pages(mainPage, editProfile);
            var tinder = new Tinder(pages, db);
            
            
            var bot = new TelegramBot(db, tinder, botClient, cts);

            Console.Read();
        }

        public static string GetToken()
        {
            var token = Environment.GetEnvironmentVariable("token");
            return token;
        }
    }
};

