using System.CommandLine;

namespace Occurify.Examples.Extensions
{
    internal static class RootCommandExtensions
    {
        public static RootCommand AssignExamples(this RootCommand rootCommand)
        {
            ArgumentNullException.ThrowIfNull(rootCommand);

            var exampleType = typeof(IExample);
            var exampleTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => exampleType.IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract);

            foreach (var type in exampleTypes)
            {
                if (Activator.CreateInstance(type) is not IExample example)
                {
                    continue;
                }
                var command = new Command(example.Command);
                command.SetAction(_ => example.Run());
                rootCommand.Add(command);
            }

            return rootCommand;
        }
    }
}
