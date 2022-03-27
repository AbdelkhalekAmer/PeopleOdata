namespace Jibble.Assessment;

internal interface IConsole
{
    void Write<T>(T item);
    void WriteLine(string message);
    void WriteError(Exception exception);
}