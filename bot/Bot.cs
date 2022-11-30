using System;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using telegram_bot.data_base;
using telegram_bot.tinder;
using telegram_bot.tinder.enums;

namespace telegram_bot.bot
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

            _dbUsers = new DbUsers("users", "json");
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

        private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.CallbackQuery != null && Enum.IsDefined(typeof(PagesEnum), update.CallbackQuery.Data.Substring(1)))
            {
                Console.WriteLine(update.CallbackQuery.Data);
                return; 
            }
                
            // TODO: ответ на сообщения, обработка кликов
            /*var message = update.Message;

            var userId = message.Chat.Id;
            var messageId = message.MessageId;
            var userName = message.Chat.Username == null ? "" : message.Chat.Username; // TODO: потом убрать
            
            var user = _dbUsers.GetOrCreate(userId, new BotUser(userId, userName));// TODO: исправить это
            
            
            var userMessage = new Message(messageId, userId, tinder.enums.MessageType.text); // TODO: определение типа сообщения

            var answer = _tinder.getAnswerByPage(user, userMessage, chatMessageId); //message.Photo[0].FileId

            // TODO: получение сообщения от функции getAnswerByPage
            if (update.Type == UpdateType.Message && update?.Message?.Text == "/start")
            {
                await SendIlineKeyboard(botClient, answer, user, cancellationToken);
            }

            else if (update.Type == UpdateType.CallbackQuery)
            {
                // для обработки нажатий на кнопки
                await HandleCallbackQuery(botClient, answer, user, cancellationToken, update.CallbackQuery);
                return;
            }

            else if(update.Type == UpdateType.Message && update?.Message?.Photo != null)
            {
                await HandlePhotoMessage(botClient, answer, user, cancellationToken);
                return;
            }

            else if(update.Type == UpdateType.Message && update?.Message?.Text != null)
            {
                await HandleTextMessage(botClient, answer, user, cancellationToken);
                return;
            }*/
        }

        async Task HandleTextMessage(ITelegramBotClient botClient, Answer answer,
            BotUser user, CancellationToken cancellationToken)
        {
            await botClient.SendTextMessageAsync(
                   chatId: user.id,
                   text: answer.text,
                   cancellationToken: cancellationToken);

            return;
        }

        async Task HandlePhotoMessage(ITelegramBotClient botClient, Answer answer,
            BotUser user, CancellationToken cancellationToken)
        {
            await botClient.SendPhotoAsync(
                chatId: user.id,
                photo: answer.photoId,
                cancellationToken: cancellationToken);
            return; //message.Photo[0].FileId так можно сохранить его Id, чтобы потом опять отправить
        }

        async Task HandleCallbackQuery(ITelegramBotClient botClient, Answer answer,
            BotUser user, CancellationToken cancellationToken, CallbackQuery callbackQuery)
        {
            if (answer.refreshThePage)
            {
                await botClient.DeleteMessageAsync(
                    chatId: user.id,
                    messageId: answer.oldMessageId);

                await SendIlineKeyboard(botClient, answer, user, cancellationToken);

                return;
            }
            else if (callbackQuery != null)
                await botClient.AnswerCallbackQueryAsync(callbackQuery.Id, "hey", true);
            else
                await HandleTextMessage(botClient, answer, user, cancellationToken); // если нужно отправить текстовый ответ
            return;
        } //await botClient.AnswerCallbackQueryAsync(callbackQuery.Id, "hey", true);

        async Task SendIlineKeyboard( ITelegramBotClient botClient, Answer answer,
            BotUser user, CancellationToken cancellationToken )
        {
            var inlineKeyboard = new InlineKeyboard(answer.keyBoard);
            inlineKeyboard.GenerateKeyboard(answer.row, answer.column);

            await botClient.SendTextMessageAsync( // TODO: случай с фотографией отельно обработать
                   chatId: user.id,
                   text: answer.text,
                   replyMarkup: inlineKeyboard.GetInlineKeyboard(), // TODO: keyboard user.onWhichPage
                   cancellationToken: cancellationToken);

            chatMessageId = answer.messageId;
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






