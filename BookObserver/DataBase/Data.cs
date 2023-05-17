using Telegram.Bot.Types;
namespace NightCodeClub.DataBase;

public interface IDataBase {
    public void Load();
    public void Save();
    public void AddNewUser(Update update);
    public AppData GetAppData();
    public User GetCurrentUser(long chatId);
}

public class User {
    public long chatID { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<BookInfo> books { get; set; }
}

public struct BookInfo {
    public string name { get; set; }
    public string author { get; set; }
    public string description { get; set; }
    public string year { get; set; }
    public string genre { get; set; }
    
    public string finishDate { get; set; }
}

public class AppData {
    public List<BookInfo> books { get; set; }
    public List<User> users { get; set; }
}


