using telegram_bot.bot.enums;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using telegram_bot.data_base;
using telegram_bot.tinder;

namespace telegram_bot.bot
{
    public class TelegramBot
    {
        private UsersDb _dbUsers;
        private Tinder _tinder;
        private int lastMessageId; // Id сообщения, которое сейчас на экране
        private TelegramBotClient _botClient;
        public TelegramBot(UsersDb dbUsers, Tinder tinder, TelegramBotClient botClient, CancellationTokenSource cts)
        {
            _dbUsers = dbUsers;
            _tinder = tinder;
            _botClient = botClient;
            var receiverOptions = new ReceiverOptions { AllowedUpdates = Array.Empty<UpdateType>() };
            _botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cts.Token
                );
            Console.WriteLine("Типа запустился");
            Console.ReadLine();
            cts.Cancel();
        }

        private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var (message, user) = GenerateMessage(update);
            
            var answer = _tinder.GetAnswer(user, message, lastMessageId);

            await SendAnswer(answer, user, cancellationToken);
        }

        private Tuple<Message, BotUser> GenerateMessage(Update update)
        {
            switch (update.Type)
            {
                case UpdateType.CallbackQuery:
                    var callback = update.CallbackQuery;
                    var command = callback.Data;
                    var userId = callback.From.Id;
                    var userName = callback.From.Username;
                    var user = _dbUsers.GetOrCreate(userId, userName);
                    
                    return new Tuple<Message, BotUser>(new Message(0, userId, BotMessageType.command, command), user);
                    break;
                case UpdateType.Message:
                    var message = update.Message;
                    userId = message.Chat.Id;
                    var messageId = message.MessageId;
                    userName = message.Chat.Username == null ? "" : message.Chat.Username;
                    user = _dbUsers.GetOrCreate(userId, userName);
                    
                    if (message.Photo != null)
                    {
                        return new Tuple<Message, BotUser>(new Message(messageId, userId, BotMessageType.img, "", message.Photo[0].FileId), user);
                    }
                    if (message.Text != null)
                    {
                        return new Tuple<Message, BotUser>(new Message(messageId, userId, BotMessageType.text, message.Text), user);
                    }
                    return new Tuple<Message, BotUser>(new Message(messageId, userId, BotMessageType.text, "/help"), user);
                default:
                    return new Tuple<Message, BotUser>(new Message(0, 0, BotMessageType.text, "/help"), null);
            }
        }
        private async Task SendAnswer(Answer answer, BotUser user, CancellationToken cancellationToken)
        {
            if (answer.isToUpdateLastMessage)
            {
                await _botClient.DeleteMessageAsync(chatId: user.id, 
                    messageId: answer.oldMessageId, 
                    cancellationToken: cancellationToken);
            }
            if (answer.isToGenerateKeyboard)
            {
                if (answer.type == BotMessageType.img)
                {
                    
                }
                else if (answer.type == BotMessageType.text)
                {
                    
                }
            }
            else
            {
                if (answer.type == BotMessageType.img)
                {
                    await _botClient.SendPhotoAsync(
                        chatId: user.id,
                        photo: answer.photoId,
                        caption: answer.text,
                        cancellationToken: cancellationToken);
                }
                else if (answer.type == BotMessageType.text)
                {
                    await _botClient.SendTextMessageAsync(
                        chatId: user.id,
                        text: answer.text,
                        cancellationToken: cancellationToken);
                }
            }
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