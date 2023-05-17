using NightCodeClub.AI;
using NightCodeClub.BotMessage;
using NightCodeClub.DataBase;
using NightCodeClub.Helpers;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace NightCodeClub.tgCommands; 

public class QuoteCommand: AbstractCommand {
    protected override string CommandName() {
        return "quote";
    }

    public override async Task SendMessage(Update update, CancellationToken cancellationToken) {
        var bookInfo = DataBridge.GetInstance().GetCurrentUser(update.Message.Chat.Id).books;
        
        if (update.Message != null)
            await TelegramAPI.GetInstance().GetBotClient().SendTextMessageAsync(
                chatId: update.Message.Chat.Id,
                text: await ChatAI.GetInstance().GenerateQuote(GetQuote(bookInfo)),
                cancellationToken: cancellationToken);
    }

    private string GetQuote(List<BookInfo> bookInfo) {
        var random = Random.Shared.Next(bookInfo.Count);
        var input = bookInfo[random].name + "-" + bookInfo[random].author;
        
        return input;
    }
}