using Telegram.Bot.Types.ReplyMarkups;

namespace NightCodeClub;

public static class Key {
    private static string[] emojis = { "😀", "😁", "😂", "🤣", "😃", "😄", "😅", "😆", "😉", "😊", "😋", "😎", "😍", "😘", "😗", "😙", "😚", "🙂", "🤗", "🤔", "😐", "😑", "😶", "🙄", "😏", "😣", "😥", "😮", "🤐", "😯", "😪", "😫", "😴", "😌", "😛", "😜", "😝", "🤤", "😒", "😓", "😔", "😕", "🙃", "🤑", "😲", "☹️", "🙁", "😖", "😞", "😟", "😤", "😢", "😭", "😦", "😧", "😨", "😩", "😬", "😰", "😱", "😳", "🤪", "😵", "😷", "🤒", "🤕", "🥶", "🥵", "🥴", "😈", "👿", "💀", "👻", "👽", "🤖", "💩", "👶", "👦", "👧", "👨", "👩", "🧑", "👱‍♀️", "👱‍♂️", "👴", "👵", "🙍‍♂️", "🙍‍♀️", "🙎‍♂️", "🙎‍♀️", "🙅‍♂️", "🙅‍♀️", "🙆‍♂️", "🙆‍♀️", "💁‍♂️", "💁‍♀️", "🙋‍♂️", "🙋‍♀️", "🤦‍♂️", "🤦‍♀️", "🤷‍♂️", "🤷‍♀️", "💆‍♂️", "💆‍♀️", "💇‍♂️", "💇‍♀️", "🚶‍♂️", "🚶‍♀️", "🏃‍♂️", "🏃‍♀️", "💃", "🕺", "👯‍♂️", "👯‍♀️", "🧖‍♂️", "🧖‍♀️", "👩‍❤️‍👩", "👨‍❤️‍👨", "💏", "👩‍❤️‍💋‍👩", "👨‍❤️‍💋‍👨", "💑", "👩‍❤️‍💋‍👨"};

    public static string About() => emojis[0];

}

public static class InlineKeyboard {
    public static readonly InlineKeyboardMarkup StartKeyboardMarkup = new(
        new []
        {
            InlineKeyboardButton.WithCallbackData(text: "About", callbackData: Key.About()),
        });
    
}