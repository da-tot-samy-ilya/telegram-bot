namespace telegram_bot.bot
{
    public class Program
    {
        public static void Main()
        {
            var token = GetToken();
            var bot = new TelegramBot(token);

            Console.Read();
        }

        public static string GetToken()
        {
            var token = File.ReadAllText(@"..\..\..\data_base\db\token.txt");
            return token;
        }
    }
};

