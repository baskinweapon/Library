namespace NightCodeClub.Helpers; 

public static class Log {
    public static void W(string log, ConsoleColor foreground = ConsoleColor.White, ConsoleColor background = ConsoleColor.Black) {
        Console.ForegroundColor = foreground;
        Console.BackgroundColor = background;
        Console.WriteLine(log);
        Console.ForegroundColor = ConsoleColor.White;
        Console.BackgroundColor = ConsoleColor.Black;
    }
}