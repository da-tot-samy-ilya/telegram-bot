using telegram_bot;
using telegram_bot.enums;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

public class TelegramBot
{
    public List<BotUser> storage = new List<BotUser>();
    public string token;
    
    public TelegramBot(string Token)
    {
        token = Token;
    }
    public BotUser SearchUserInStorageById(long userId, List<BotUser> storage)
    {
        foreach (var user in storage)
        {
            if (userId == user.id)
            {
                return user;
            }
        }
        return null;
    }
    
    async public void Init()
    {
        var botClient = new TelegramBotClient(token);
        using var cts = new CancellationTokenSource();
        var receiverOptions = new ReceiverOptions { AllowedUpdates = Array.Empty<UpdateType>() };
        botClient.StartReceiving(updateHandler: HandleUpdateAsync, pollingErrorHandler: HandlePollingErrorAsync, receiverOptions: receiverOptions, cancellationToken: cts.Token);
        var me = await botClient.GetMeAsync();

        Console.WriteLine($"Start listening for @{me.Username}");
        Console.ReadLine();
        cts.Cancel();
    }
    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken) {
        if (update.Message is not { } message) return;
        if (message.Text is not { } messageText) return;
        
        var chatId = message.Chat.Id;
        var user = SearchUserInStorageById(chatId, storage);
        
        if (user is null)
        {
            user = new BotUser(GameStatus.NotPlaying, chatId, message.Chat.Username, null);
            storage.Add(user);
        }

        var answer = Logic.GenerateAnswer(user, messageText);
        
        Message sentMessage = await botClient.SendTextMessageAsync(
            chatId: chatId,
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





