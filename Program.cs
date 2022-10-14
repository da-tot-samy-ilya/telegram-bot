namespace telegram_bot
{
    public class Program
    {
        public static void Main()
        {
            var token = GetToken();
            var bot = new TelegramBot(token);
        
            bot.Init();
            Console.Read();
        }

        public static string GetToken()
        {
            var token = File.ReadAllText(@"..\..\..\db\token.txt");
            return token;
        }
    }
};

