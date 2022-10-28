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
        private Tinder _tinder;
        
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
            _tinder = new Tinder();

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
            // TODO: получение сообщения от функции getAnswerByPage
            if (update.Type == UpdateType.CallbackQuery)
            {
                // для обработки нажатий на кнопки
                await HandleCallbackQuery(botClient, update.CallbackQuery);
                return;
            }


            if (update.Type == UpdateType.Message && update?.Message?.Photo != null)
            {
                await HandleCallbackQuery(botClient, );
                return;
            }

            if (update.Type == UpdateType.Message && update?.Message?.Text != null)
            {
                await HandleTextMessage(botClient, )

               return;
            }    
        }

        async Task HandleTextMessage(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            /*if (update.Message is not { } message) return;
            if (message.Text is not { } messageText) return;*/

            var userId = message.Chat.Id;
            var userName = message.Chat.Username == null ? "" : message.Chat.Username;

            await botClient.SendTextMessageAsync(
                   chatId: userId,
                   text: "Hey", // TODO: только нужный текст
                   cancellationToken: cancellationToken);
            return;
        }

        async Task HandlePhotoMessage(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken ) 
        {
            await botClient.SendPhotoAsync(
                chatId: message.Chat.Id,
                photo: message.Photo[0].FileId, // так можно сохранить его Id, чтобы потом опять отправить
                cancellationToken: cancellationToken);
            return;
        }

        async Task HandleCallbackQuery(ITelegramBotClient botClient, CallbackQuery callbackQuery)
        {
            if () // TODO: если нужно обновить страницу, посмотреть по флагу в Message
            {
                // сначала удалить старую, затем создать новую
            }
            else
                HandleTextMessage(); // если нужно отправить текстовый ответ
            //await botClient.AnswerCallbackQueryAsync(callbackQuery.Id, "hey", true);
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






