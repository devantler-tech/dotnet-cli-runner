using System.Text;
using CliWrap;
using CliWrap.EventStream;
using Spectre.Console;

namespace Devantler.CLIRunner;

/// <summary>
/// A class to run CLI commands and capture their output.
/// </summary>
public static class CLI
{
  /// <summary>
  /// Run a CLI command and capture its output.
  /// </summary>
  /// <param name="command"></param>
  /// <param name="cancellationToken"></param>
  /// <param name="validation"></param>
  /// <param name="silent"></param>
  /// <returns></returns>
  public static async Task<(int exitCode, string result)> RunAsync(Command command, CancellationToken cancellationToken, CommandResultValidation validation = CommandResultValidation.ZeroExitCode, bool silent = false)
  {
    ArgumentNullException.ThrowIfNull(command);
    bool isFaulty = false;
    StringBuilder result = new();
    try
    {
      await foreach (var cmdEvent in command.WithValidation(validation).ListenAsync(cancellationToken: cancellationToken))
      {
        switch (cmdEvent)
        {
          case StartedCommandEvent started:
            if (System.Diagnostics.Debugger.IsAttached || Environment.GetEnvironmentVariable("DEBUG") is not null)
              AnsiConsole.MarkupLine($"[bold blue]DEBUG[/] Process started: {started.ProcessId}");
            break;
          case StandardOutputCommandEvent stdOut:
            if (!silent)
            {
              Console.WriteLine(stdOut.Text);
            }
            _ = result.AppendLine(stdOut.Text);
            break;
          case StandardErrorCommandEvent stdErr:
            if (!silent)
            {
              Console.WriteLine(stdErr.Text);
            }
            _ = result.AppendLine(stdErr.Text);
            break;
          case ExitedCommandEvent exited:
            if (System.Diagnostics.Debugger.IsAttached || Environment.GetEnvironmentVariable("DEBUG") is not null)
              AnsiConsole.MarkupLine($"[bold blue]DEBUG[/] Process exited with code {exited.ExitCode}");
            break;
          default:
            if ((cmdEvent is not null && System.Diagnostics.Debugger.IsAttached) || Environment.GetEnvironmentVariable("DEBUG") is not null)
              AnsiConsole.MarkupLine($"[bold blue]DEBUG[/] {cmdEvent}");
            break;
        }
      }
    }
#pragma warning disable CA1031 // Do not catch general exception types
    catch
#pragma warning restore CA1031 // Do not catch general exception types
    {
      isFaulty = true;
    }
    return isFaulty ? (1, result.ToString()) : (0, result.ToString());
  }
}
