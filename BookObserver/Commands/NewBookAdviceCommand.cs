using NightCodeClub.AI;
using NightCodeClub.DataBase;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace NightCodeClub.tgCommands; 

public class NewBookAdviceCommand: AbstractCommand {
    protected override string CommandName() {
        return "advice_book";
    }

    public override async Task SendMessage(Update update, CancellationToken cancellationToken) {
        var bookInfo = DataBridge.GetInstance().GetCurrentUser(update.Message.Chat.Id).books;
        
        if (update.Message != null)
            await TelegramAPI.GetInstance().GetBotClient().SendTextMessageAsync(
                chatId: update.Message.Chat.Id,
                text: await ChatAI.GetInstance().GenerateNewBookAdvice(GetNewBook(bookInfo)),
                cancellationToken: cancellationToken);
    }

    private string GetNewBook(List<BookInfo> bookInfo) {
        var input = "";
        
        foreach (var book in bookInfo) {
            input += $"({book.name}-{book.author})."; 
        }
    
        return input;
    }
}