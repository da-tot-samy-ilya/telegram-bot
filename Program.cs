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
            var curDir = Directory.GetCurrentDirectory();
            var parent2 = Directory.GetParent(curDir).FullName;
            var parent1 = Directory.GetParent(parent2).FullName;
            var parent = Directory.GetParent(parent1).FullName;
            
            var path = System.IO.Path.Join(parent, "db","token.txt");
            var token = File.ReadAllText(path);
            return token;
        }
    }
};

