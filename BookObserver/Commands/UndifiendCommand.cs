using Telegram.Bot;
using Telegram.Bot.Types;

namespace NightCodeClub.tgCommands; 

public class UndifiendCommand: AbstractCommand {
    protected override string CommandName() {
        return "undefined";;
    }
    
    public override async Task SendMessage(Update update, CancellationToken cancellationToken) {
        if (update.Message != null)
            await TelegramAPI.GetInstance().GetBotClient().SendTextMessageAsync(
                chatId: update.Message.Chat.Id,
                text: "undefined command",
                cancellationToken: cancellationToken);
    }
}