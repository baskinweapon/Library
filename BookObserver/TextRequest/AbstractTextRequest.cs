using Telegram.Bot.Types;
namespace NightCodeClub.TextRequest; 

public abstract class AbstractTextRequest {
    public abstract Task SendMessage(string msg, Update update, CancellationToken cancellationToken);
}