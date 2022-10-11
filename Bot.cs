using telegram_bot.enums;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace telegram_bot
{
    public class TelegramBot
    {
        private DbUsers dbUsers;
        
        public string token;
        public TelegramBotClient botClient;
        public CancellationTokenSource cts;
        public ReceiverOptions receiverOptions;
        public User me;
        public TelegramBot(string Token)
        {
            token = Token;
        }
        async public void Init()
        {
            dbUsers = new DbUsers("users","json");
            
            botClient = new TelegramBotClient(token);
            cts = new CancellationTokenSource(); 
            receiverOptions = new ReceiverOptions { AllowedUpdates = Array.Empty<UpdateType>() };
            botClient.StartReceiving(
                updateHandler: HandleUpdateAsync, 
                pollingErrorHandler: HandlePollingErrorAsync, 
                receiverOptions: receiverOptions, 
                cancellationToken: cts.Token);
            me = await botClient.GetMeAsync();

            Console.WriteLine($"Start listening for @{me.Username}");
            Console.ReadLine();
            cts.Cancel();
        }
        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken) {
            if (update.Message is not { } message) return;
            if (message.Text is not { } messageText) return;
            
            var userId = message.Chat.Id;
            var userName = message.Chat.Username;

            var user = new BotUser(GameStatus.NotPlaying, userId, userName, new Game(0, 0, 0));
            
            var answer = Logic.GenerateAnswer(dbUsers, userId, user, messageText);
            
            Message sentMessage = await botClient.SendTextMessageAsync(
                chatId: userId,
                text: answer,
                cancellationToken: cancellationToken);
        }
        public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken) {
            var ErrorMessage = exception switch {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
    } 
}






