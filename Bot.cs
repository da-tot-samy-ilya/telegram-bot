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
        private int chatMessageId; // Id сообщения, которое сейчас на экране
        
        private readonly string _token;
        private TelegramBotClient _botClient;
        private CancellationTokenSource _cts;
        private ReceiverOptions _receiverOptions;

        public TelegramBot(string token)
        {
            _token = token;
        
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
            Console.WriteLine("Типа запустился");
            Console.ReadLine();
            _cts.Cancel();
        }

        private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken )
        {
            // TODO: ответ на сообщения, обработка кликов
            var message = update.Message; 
            
            var userId = message.Chat.Id;
            var messageId = message.MessageId;
            var userName = message.Chat.Username == null ? "" : message.Chat.Username; // TODO: потом убрать
            var user = new BotUser(userId, userName); // TODO: потом убрать

            /*// TODO: если пользователь есть в бд, то возращать его, если его нет, то создавать
            // типа того:
            if (_dbUsers.FindByKey(userId))
            {
                var user = _dbUsers.GetOrCreate(userId);
            }
            else
            {
                var userName = message.Chat.Username == null ? "" : message.Chat.Username;
                var user = new BotUser(userId, userName);
            }*/

            var userMessage = new Message(messageId, userId, MessageType.text); // TODO: определение типа сообщения

            var answer = _tinder.getAnswerByPage(user, userMessage); //message.Photo[0].FileId

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

        async Task HandleTextMessage( ITelegramBotClient botClient, Answer message,
            BotUser user, CancellationToken cancellationToken )
        {
            await botClient.SendTextMessageAsync(
                   chatId: user.id,
                   text: message.text,
                   cancellationToken: cancellationToken);
            
            return;
        }

        async Task HandlePhotoMessage( ITelegramBotClient botClient, Answer message,
            BotUser user, CancellationToken cancellationToken )
        {
            await botClient.SendPhotoAsync(
                chatId: user.id,
                photo: message.photoId,
                cancellationToken: cancellationToken);
            return; //message.Photo[0].FileId так можно сохранить его Id, чтобы потом опять отправить
        }

        async Task HandleCallbackQuery( ITelegramBotClient botClient, Answer message,
            BotUser user, CancellationToken cancellationToken, CallbackQuery callbackQuery )
        {
            if (message.refreshThePage)
            {
                await botClient.DeleteMessageAsync(
                    chatId: user.id,
                    messageId: message.oldMessageId); 


                await botClient.SendTextMessageAsync( // TODO: случай с фотографией отельно обработать
                   chatId: user.id,
                   text: message.text,
                   replyMarkup: , // TODO: keyboard user.onWhichPage
                   cancellationToken: cancellationToken);
                return;
            }
            else
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






