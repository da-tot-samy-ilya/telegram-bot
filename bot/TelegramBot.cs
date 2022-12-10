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
        private readonly UsersDb _dbUsers;
        private readonly Tinder _tinder;
        private readonly TelegramBotClient _botClient;
        private readonly InlineKeyboard _inlineKeyboard;
        public TelegramBot(UsersDb dbUsers, Tinder tinder, TelegramBotClient botClient, CancellationTokenSource cts)
        {
            _dbUsers = dbUsers;
            _tinder = tinder;
            _botClient = botClient;
            _inlineKeyboard = new InlineKeyboard();
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
            var message = GenerateMessage(update);
            var answer = _tinder.GetAnswer(message, message.user.lastMessageId);
            await SendAnswer(answer, cancellationToken);
        }

        private Message GenerateMessage(Update update)
        {
            switch (update.Type)
            {
                case UpdateType.CallbackQuery:
                    var callback = update.CallbackQuery;
                    var command = callback.Data;
                    var userId = callback.From.Id;
                    var userName = callback.From.Username;
                    var user = _dbUsers.GetOrCreate(userId, userName, 0);
                    
                    return new Message(user, BotMessageType.command, command);
                case UpdateType.Message:
                    var message = update.Message;
                    userId = message.Chat.Id;
                    userName = message.Chat.Username == null ? "" : message.Chat.Username;
                    user = _dbUsers.GetOrCreate(userId, userName, 0);
                    
                    if (message.Photo != null)
                    {
                        return new Message(user, BotMessageType.img, "", message.Photo[0].FileId);
                    }
                    if (message.Text != null)
                    {
                        return new Message(user, BotMessageType.text, message.Text);
                    }
                    return new Message(user, BotMessageType.incorrectType, "");
                default:
                    return new Message( null, BotMessageType.incorrectType, "");
            }
        }
        private async Task SendAnswer(Answer answer, CancellationToken cancellationToken)
        {
            var message = new Telegram.Bot.Types.Message();
            if (answer.isToUpdateLastMessage)
            {
                try
                {
                    await _botClient.DeleteMessageAsync(chatId: answer.user.id, 
                        messageId: answer.user.lastMessageId, 
                        cancellationToken: cancellationToken);
                }
                catch (Exception e)
                {
                    Console.WriteLine("There is no such message to delete");
                }
            }
            if (answer.isToGenerateKeyboard)
            {
                switch (answer.type)
                {
                    case BotMessageType.img:
                        message = await _botClient.SendPhotoAsync(
                            chatId: answer.user.id,
                            photo: answer.photoId,
                            caption: answer.text,
                            replyMarkup: _inlineKeyboard.GenerateKeyboard(answer.rowsCount, answer.columnsCount,
                                answer.keyBoard),
                            cancellationToken: cancellationToken);
                        break;
                    case BotMessageType.text:
                        try
                        {
                            message = await _botClient.SendTextMessageAsync(
                                chatId: answer.user.id,
                                text: answer.text,
                                replyMarkup: _inlineKeyboard.GenerateKeyboard(answer.rowsCount, answer.columnsCount,
                                    answer.keyBoard),
                                cancellationToken: cancellationToken);
                            
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                        break;
                }
            }
            else
            {
                switch (answer.type)
                {
                    case BotMessageType.img:
                        message = await _botClient.SendPhotoAsync(
                            chatId: answer.user.id,
                            photo: answer.photoId,
                            caption: answer.text,
                            cancellationToken: cancellationToken);
                        break;
                    case BotMessageType.text:
                        message = await _botClient.SendTextMessageAsync(
                            chatId: answer.user.id,
                            text: answer.text,
                            cancellationToken: cancellationToken);
                        break;
                }
            }
            answer.user.lastMessageId = message.MessageId;
            _dbUsers.Update(answer.user.id, answer.user);
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