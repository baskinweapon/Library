using NightCodeClub.BotMessage;
using NightCodeClub.DataBase;
using NightCodeClub.Helpers;
using Telegram.Bot;
using Telegram.Bot.Types;
namespace NightCodeClub.tgCommands;

public class StartCommand: AbstractCommand {
    protected override string CommandName() {
        return "start";
    }

    public override bool TrueCommand(string command) {
        return CommandName() == command;
    }

    public override async Task SendMessage(Update update, CancellationToken cancellationToken) {
        if (update.Message != null) DataBridge.GetInstance().AddNewUser(update);

        if (update.Message != null)
            await TelegramAPI.GetInstance().GetBotClient().SendTextMessageAsync(
                chatId: update.Message.Chat.Id,
                text: BotInputData.StartMessage,
                replyMarkup: InlineKeyboard.StartKeyboardMarkup,
                cancellationToken: cancellationToken);
    }
}