using Telegram.Bot;
using Telegram.Bot.Types;

namespace NightCodeClub; 

public class Query {
    public async void CallbackData(CallbackQuery? callbackQuery, TelegramBotClient botClient, CancellationToken cancellationToken) {
        if (callbackQuery?.Data == Key.About()) {
            if (callbackQuery.Message != null)
                await botClient.SendTextMessageAsync(
                    chatId: callbackQuery.Message.Chat.Id,
                    text: "About Callback",
                    cancellationToken: cancellationToken
                );
        }
    }
}