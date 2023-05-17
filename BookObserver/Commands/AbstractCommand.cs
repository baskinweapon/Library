using Telegram.Bot.Types;

public interface ICommand {
    public void SendMessage(Update update, CancellationToken cancellationToken);
}

public abstract class AbstractCommand {
    protected abstract string CommandName();
    public virtual bool TrueCommand(string command) {
        return CommandName() == command;
    }
    
    public abstract Task SendMessage(Update update, CancellationToken cancellationToken);
}