using telegram_bot.enums;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Microsoft.VisualBasic;
using System.Threading;
using Telegram.Bot.Types.ReplyMarkups;

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
            if (update.Message is not { } message) return;

            var userId = message.Chat.Id;
            var userName = message.Chat.Username == null ? "" : message.Chat.Username;

            // TODO: нужно ли так делать? Либо обрабатывать сразу нахождение user в БД?
            var user = new BotUser(userId, userName, message.Photo[0].FileId);

            // TODO: дописать парамеры Request, стоит ли заходить в if для этого?
            var answer = _tinder.getAnswerByPage(user, new Request());

            // TODO: получение сообщения от функции getAnswerByPage
            if (update.Type == UpdateType.CallbackQuery)
            {
                // для обработки нажатий на кнопки
                await HandleCallbackQuery(botClient, answer, user, cancellationToken, update.CallbackQuery);
                return;
            }

            if (update.Type == UpdateType.Message && update?.Message?.Photo != null)
            {
                await HandlePhotoMessage(botClient, answer, user, cancellationToken);
                return;
            }

            if (update.Type == UpdateType.Message && update?.Message?.Text != null)
            {
                await HandleTextMessage(botClient, answer, user, cancellationToken);
                return;
            }    
        }

        async Task HandleTextMessage(ITelegramBotClient botClient, Request message, 
            BotUser user, CancellationToken cancellationToken)
        {
            await botClient.SendTextMessageAsync(
                   chatId: user.id,
                   text: message.text,
                   cancellationToken: cancellationToken);
            return;
        }

        async Task HandlePhotoMessage( ITelegramBotClient botClient, Request message,
            BotUser user, CancellationToken cancellationToken ) 
        {
            await botClient.SendPhotoAsync(
                chatId: user.id,
                photo: message.photoId,
                cancellationToken: cancellationToken);
            return; //message.Photo[0].FileId так можно сохранить его Id, чтобы потом опять отправить
        }

        async Task HandleCallbackQuery( ITelegramBotClient botClient, Request message,
            BotUser user, CancellationToken cancellationToken, CallbackQuery callbackQuery)
        {
            if (message.refreshThePage)
            {
                await botClient.DeleteMessageAsync(
                    chatId: user.id,
                    messageId: ); // TODO: message ID

                
                await botClient.SendTextMessageAsync( // TODO: случай с фоотографией отельно обработать
                   chatId: user.id,
                   text: message.text,
                   replyMarkup: , // TODO: keyboard
                   cancellationToken: cancellationToken);
                return;
            }
            await HandleTextMessage(botClient, message, user, cancellationToken); // если нужно отправить текстовый ответ
            return;
        } //await botClient.AnswerCallbackQueryAsync(callbackQuery.Id, "hey", true);

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






