using System.Text.Json;

namespace Jibble.Assessment;

internal class JibbleConsole : IConsole
{
    public void Write<T>(T item)
    {
        JsonSerializerOptions options = new() { WriteIndented = true };

        string serializedItem = JsonSerializer.Serialize(item, options);

        Console.ForegroundColor = ConsoleColor.Blue;

        Console.WriteLine(serializedItem);

        Console.ResetColor();
    }

    public void WriteError(Exception exception)
    {
        Console.ForegroundColor = ConsoleColor.Red;

        Console.WriteLine($"{exception.GetType()}: {exception.Message}");

        Console.ForegroundColor = ConsoleColor.Magenta;

        Console.WriteLine($"Stack trace:\n{exception.StackTrace}");

        Console.ResetColor();
    }

    public void WriteLine(string message) => Console.WriteLine(message);
}