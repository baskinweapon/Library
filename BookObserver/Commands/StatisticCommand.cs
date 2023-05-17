using System.Diagnostics;
using System.Runtime.InteropServices.JavaScript;
using NightCodeClub.AI;
using NightCodeClub.DataBase;
using NightCodeClub.Helpers;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace NightCodeClub.tgCommands; 

public class StatisticCommand: AbstractCommand {
    protected override string CommandName() {
        return "statistic";
    }

    public override async Task SendMessage(Update update, CancellationToken cancellationToken) {
        var bookInfo = DataBridge.GetInstance().GetCurrentUser(update.Message.Chat.Id).books;
        
        if (update.Message != null)
            await TelegramAPI.GetInstance().GetBotClient().SendTextMessageAsync(
                chatId: update.Message.Chat.Id,
                text: await ChatAI.GetInstance().GenerateStatistic(GetStatistic(bookInfo)),
                cancellationToken: cancellationToken);
    }
    
    
    private string GetStatistic(List<BookInfo> bookInfo) {
        for (int i = 0; i < bookInfo.Count; i++) {
            try {
                DateTime.TryParse(bookInfo[i].finishDate, out var date);
                bookInfo[i] = new BookInfo {
                    name = bookInfo[i].name,
                    author = bookInfo[i].author,
                    description = bookInfo[i].description,
                    finishDate = date.Date.ToShortDateString()
                };
            }
            catch (Exception e) {
                Log.W(e.Message);
            }
        }
        
        var input = "";
        
        input += "size=" + (bookInfo.Count) + ".";
        foreach (var book in bookInfo) {
            input += $"({book.name}-{book.author}).finish={book.finishDate}."; 
        }
        
        return input;
    }
}
