using NightCodeClub.AI;
using NightCodeClub.DataBase;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace NightCodeClub.TextRequest; 

public class AddBookRequest: AbstractTextRequest {
    
    public override async Task SendMessage(string msg, Update update, CancellationToken cancellationToken) {
        var book = await ChatAI.GetInstance().NewBook(msg, update.Message.Chat.Id);
        var text = book == null ? "Я не нашел такую книгу" : AddBook((BookInfo)book, update.Message.Chat.Id);
        
        await TelegramAPI.GetInstance().GetBotClient().SendTextMessageAsync(
            chatId: update.Message.Chat.Id,
            text: text,
            cancellationToken: cancellationToken);
    }

    private string AddBook(BookInfo book, long id) {
        if (DataBridge.GetInstance().GetAppData().books.All(b => b.name != book.name)) {
            DataBridge.GetInstance().GetAppData().books.Add(book);
        }
        
        var user = DataBridge.GetInstance().GetCurrentUser(id); 
        user.books ??= new List<BookInfo>(); 
        if (user.books.Any(b => b.name == book.name)) {
            return "You already read this book";
        }
        user.books.Add(book);
        DataBridge.GetInstance().Save();
        return book.description;
    }
}