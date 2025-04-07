using System.CommandLine;
using Occurify.Examples.Examples.Scheduling;
using Occurify.Examples.Extensions;

await new RootCommand("Example app for Occurify")
    .AssignExample<SchedulingExample>()
    .InvokeAsync(args);