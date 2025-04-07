using System.CommandLine;

namespace Occurify.Examples.Extensions
{
    internal static class RootCommandExtensions
    {
        public static RootCommand AssignExample<TExample>(this RootCommand rootCommand) where TExample : IExample, new()
        {
            var example = new TExample();
            var command = new Command(example.Command, example.Description);
            command.SetHandler(example.Run);
            rootCommand.AddCommand(command);
            return rootCommand;
        }
    }
}
