using NightCodeClub.AI;
using NightCodeClub.Helpers;
using NightCodeClub.Keys;
using NightCodeClub.TextRequest;
using NightCodeClub.tgCommands;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace NightCodeClub; 

public class TelegramAPI {
    private static TelegramAPI Instance { get; } = new();
    
    private TelegramBotClient botClient;
    private CancellationTokenSource cts = new ();
    private ReceiverOptions receiverOptions = new () {AllowedUpdates = Array.Empty<UpdateType>()};

    public static  TelegramAPI GetInstance() => Instance;
    public TelegramBotClient GetBotClient() => botClient;
    private TelegramAPI() {
        botClient = new TelegramBotClient(PublicKeys.TelegramBotKey);
        
        botClient.StartReceiving(
            updateHandler: HandleUpdateAsync,
            pollingErrorHandler: HandlePollingErrorAsync,
            receiverOptions: receiverOptions,
            cancellationToken: cts.Token
            );
    }
    
    public async void GetMeAsync() => await botClient.GetMeAsync();
    
    //Main handle from Telegram bot
    async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken) {
        if (update.Type == UpdateType.CallbackQuery) HandleCallbackQuery(update, cancellationToken);
        if (update.Message is not { Text: { } messageText}) return;
        if (update.Message.Type == MessageType.Contact) Console.WriteLine(update.Message.Type);
        if (messageText.StartsWith("/")) {
            CommandDistribution(messageText, update, cancellationToken);
            return;
        }
        
        if (update.Type == UpdateType.Message)
            TextDistribution(messageText, update, cancellationToken);
           
    }

    private void CommandDistribution(string messageText, Update update, CancellationToken cancellationToken) {
        messageText = messageText.Replace("/", "");
        var commands = new AbstractCommand[] { new StartCommand(), new UndifiendCommand(), new QuoteCommand(),
                                                new StatisticCommand(), new PartCommand(), new NewBookAdviceCommand()};
        foreach (var t in commands) {
            if (!t.TrueCommand(messageText)) continue;
            t.SendMessage(update, cancellationToken);
            return;
        }
    }

    private void TextDistribution(string messageText, Update update, CancellationToken cancellationToken) {
        var commands = new AbstractTextRequest[] {  new AddBookRequest() };
        foreach (var t in commands) {
            t.SendMessage(messageText, update, cancellationToken);
            return;
        }
    }
    
    async void HandleCallbackQuery(Update update, CancellationToken cancellationToken) {
        var callbackQuery = update.CallbackQuery;
        await botClient.AnswerCallbackQueryAsync(callbackQuery.Id, cancellationToken: cancellationToken);

        var query = new Query();
        query.CallbackData(callbackQuery, botClient, cancellationToken);
    
    }

    private Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken) {
        var errorMessage = exception switch {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };
        Console.WriteLine(errorMessage);
        return Task.CompletedTask;
    }
    
    public async void StartListening() {
        var me = await botClient.GetMeAsync();
        Log.W($"HI @{me.Username} IH", ConsoleColor.Yellow, ConsoleColor.Blue);
    }
    
    public void StopBot() => cts.Cancel();
}
