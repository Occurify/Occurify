namespace Occurify.Examples;

public interface IExample
{
    string Command { get; }
    string Description { get; }
    void Run();
}