namespace Occurify.Examples;

public interface IExample
{
    string Command { get; }
    void Run();
}