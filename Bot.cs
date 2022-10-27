using telegram_bot.enums;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Microsoft.VisualBasic;
using System.Threading;

namespace telegram_bot
{
    public class TelegramBot
    {
        private DbUsers _dbUsers;
        private DbKeyWords _dbKeyWords;
        private Logic _logic;
        private MainPage _mainPage;
        
        private readonly string _token;
        private TelegramBotClient _botClient;
        private CancellationTokenSource _cts;
        private ReceiverOptions _receiverOptions;
        private User _me;
        public TelegramBot(string token)
        {
            _token = token;
        }

        public async void Init()
        {
            _dbUsers = new DbUsers("users","json");
            _dbKeyWords = new DbKeyWords("keyWords", "json");
            _logic = new Logic(_dbUsers, _dbKeyWords);
            _mainPage = new MainPage();


            _botClient = new TelegramBotClient(_token);
            _cts = new CancellationTokenSource(); 
            _receiverOptions = new ReceiverOptions { AllowedUpdates = Array.Empty<UpdateType>() };
            _botClient.StartReceiving(
                updateHandler: HandleUpdateAsync, 
                pollingErrorHandler: HandlePollingErrorAsync, 
                receiverOptions: _receiverOptions, 
                cancellationToken: _cts.Token
                );
            _me = await _botClient.GetMeAsync();
            Console.WriteLine($"Start listening for @{_me.Username}");
            Console.ReadLine();
            _cts.Cancel();
        }


        private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {

            if (update.Type == UpdateType.CallbackQuery)
            {
                await HandleCallbackQuery(botClient, update.CallbackQuery);
                return;
            }

            if (update.Type == UpdateType.Message && update?.Message?.Text != null)
            {
                if (update.Message is not { } message) return;
                if (message.Text is not { } messageText) return;

                var userId = message.Chat.Id;
                var userName = message.Chat.Username == null ? "" : message.Chat.Username;

                var user = new BotUser(GameStatus.NotPlaying, userId, userName);

                await botClient.SendTextMessageAsync(
                    chatId: userId,
                    text: "Hey",
                    replyMarkup: _mainPage.inlineKeyboard,
                    cancellationToken: cancellationToken);
                return;
            }    
        }

        async Task HandleMessage(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            return;
        }

        async Task HandleCallbackQuery(ITelegramBotClient botClient, CallbackQuery callbackQuery)
        {
            await botClient.AnswerCallbackQueryAsync(callbackQuery.Id, "hey", true);
        }

        private Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken) 
        {
            var errorMessage = exception switch 
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(errorMessage);
            return Task.CompletedTask;
        }
    } 
}






