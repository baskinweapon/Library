using System.Text.Json;
using NightCodeClub.DataBase;
using NightCodeClub.Helpers;
using OpenAI_API;
using File = System.IO.File;

namespace NightCodeClub.AI; 
using Keys;

public class ChatAI {
    private static readonly ChatAI Instance = new();
    private static readonly string RequestPath = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent +
                                                 "/AI/Requests/GPTRequest.txt";
    private static readonly string ExampleOutputPath = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent +
                                                 "/AI/Requests/ExampleAnswerAI.txt";

    private OpenAIAPI api;
    public static ChatAI GetInstance() => Instance;
    
    private ChatAI() {
        api = new OpenAIAPI(PublicKeys.ApiKey);
        api.Chat.DefaultChatRequestArgs.Temperature = 0;
    }
    
    
    public async Task<BookInfo?> NewBook(string msg, long id) {
        var chat = api.Chat.CreateConversation();
        chat.AppendSystemMessage(
            "read this text - @{msg}, and if in this text have a real book name, find this book information and write information in this json format " +
            $"{typeof(BookInfo)} very important fill all fields correctly and truly" +
            $"if in this text have a date, use this date as a finishDate, if not, use current date in format - {DateTime.Now.ToString("dd.MM.yyyy")} in finishDate." +
            $"dont write anything else, only json, if you cant find this book, write only - null.");
        
        var output = await File.ReadAllTextAsync(ExampleOutputPath);
        chat.AppendUserInput("цветы для элджeрона");
        chat.AppendExampleChatbotOutput(output);
        
        chat.AppendUserInput(msg);
        
        var response = await chat.GetResponseFromChatbotAsync();
        Log.W(response, ConsoleColor.Green);
        if (response.Contains("null")) return null;
        BookInfo book;
        try {
            book = JsonSerializer.Deserialize<BookInfo>(response);
        }
        catch (Exception e) {
            Log.W("cant deserialize book", ConsoleColor.Red);
            return null;
        }
        return book;
    }
    
    public async Task<string> GenerateStatistic(string input) {
        var chat = api.Chat.CreateConversation();
        chat.AppendSystemMessage("Ты библиотекарь и собираешь статистику прочитанных книг" +
                                        "бери дынные только из data, не используй другие данные, указывай точные данные которые есть в data"
        );
        
        chat.AppendUserInput($"data - {input}. в data собраны название книги ее автор и когда книгу закончили." +
                             $"size=колличество книг, укажи это колличество" +
                             $"finish = дата когда книгу закончили" +
                             $"на основе этих данных напиши сколько книг и в каких годах были прочитанны и статистику" +
                             $"напиши жанры книг" +
                             $"делай пункты с emoji");
        var response = await chat.GetResponseFromChatbotAsync();
        return response;
    }
    
    public async Task<string> GenerateQuote(string books) {
        var chat = api.Chat.CreateConversation();
        chat.AppendSystemMessage("Ты библиотекарь и записываешь все книги," +
                                 " которые я прочитаю, отвечать на вопрос по литературному и вежливо," +
                                 " старайся использовать фразы из книг, которые я уже прочитал"
        );
        
        chat.AppendUserInput($"Напиши цитату из этой книги - {books}. В ответе укажи только автора, название книги и цитату, используй emojies");
        var response = await chat.GetResponseFromChatbotAsync();
        return response;
    }
    
    public async Task<string> GeneratePartText(string books) {
        var chat = api.Chat.CreateConversation();
        chat.AppendSystemMessage("Ты библиотекарь и записываешь все книги," +
                                 " которые я прочитаю, отвечать на вопрос по литературному и вежливо," +
                                 " старайся использовать фразы из книг, которые я уже прочитал"
        );
        
        chat.AppendUserInput($"Напиши отрывок из этой книги - {books}. В ответе укажи только автора, название книги и отрывок, используй emojies");
        var response = await chat.GetResponseFromChatbotAsync();
        return response;
    }
    
    public async Task<string> GenerateNewBookAdvice(string books) {
        var chat = api.Chat.CreateConversation();
        chat.AppendSystemMessage("Ты библиотекарь и записываешь все книги," +
                                 " которые я прочитаю, отвечать на вопрос по литературному и вежливо," +
                                 " старайся использовать фразы из книг, которые я уже прочитал"
        );
        
        chat.AppendUserInput($"Посоветуй новую книгу которые мне подойдут. Вот список уже прочитанных книг - {books}. используй emojies");
        var response = await chat.GetResponseFromChatbotAsync();
        return response;
    }
}