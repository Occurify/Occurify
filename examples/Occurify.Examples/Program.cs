using System.CommandLine;
using Occurify.Astro;
using Occurify.Examples.Extensions;

// Setting coordinates for all examples
Coordinates.Local = new Coordinates(48.8584, 2.2945, 330); // Top point of the Eiffel Tower

await new RootCommand("Example app for Occurify")
    .AssignExamples()
    .Parse(args)
    .InvokeAsync();