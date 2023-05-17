using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using NightCodeClub.Helpers;
using Telegram.Bot.Types;
using File = System.IO.File;
namespace NightCodeClub.DataBase;


public class JsonDataBase: IDataBase {
   
    private static readonly string fileName = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent + "/data.json";
    
    private AppData appData;
    public void AddNewUser(Update update) {
        var user = new User {
            chatID = update.Message.Chat.Id,
            UserName = update.Message.Chat.Username,
            FirstName = update.Message.Chat.FirstName,
            LastName = update.Message.Chat.LastName
        };
            
        if (appData.users.Any(s => s.chatID == user.chatID)) {
            Console.WriteLine($"User {user.UserName} already exists");
            return;
        }
            
        appData.users.Add(user);
        Save();
        Console.WriteLine("User added " + appData.users[-1].UserName);
    }
    
    public AppData GetAppData() {
        return appData;
    }

    public User GetCurrentUser(long chatId) {
        return appData.users.First(user => user.chatID == chatId);
    }

    public void RemoveUser(User user) {
        if (appData.users.Any(s => s.chatID == user.chatID)) appData.users.Remove(user);
    }
    
    public void Load() {
        if (!File.Exists(fileName)) {
            File.Create(fileName);
        };
        appData = new AppData {
            users = new List<User>(),
            books = new List<BookInfo>()
        };
        var json = File.ReadAllText(fileName);
        if (json == "") return;
        var load = JsonSerializer.Deserialize<AppData>(json);
        if (load != null) appData = load;
    }

    public void Save() {
        var options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
            WriteIndented = true
        };
        var text = JsonSerializer.Serialize(appData, options: options);
        File.WriteAllText(fileName, text);
        
        Log.W("Save data", ConsoleColor.Green, ConsoleColor.DarkGray);
    }
}