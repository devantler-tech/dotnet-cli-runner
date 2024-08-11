using CliWrap;

namespace Devantler.CLIRunner.Tests.Integration;

/// <summary>
/// Integration tests for the <see cref="CLI.RunAsync"/> method.
/// </summary>
public class RunAsyncTests
{
  /// <summary>
  /// Tests that the <see cref="CLI.RunAsync"/> method returns the expected exit code and stdout result
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task RunAsync_GivenValidCommand_ReturnsZeroExitCodeAndStdout()
  {
    // Arrange
    Environment.SetEnvironmentVariable("DEBUG", "true");
    var command = new Command("echo")
      .WithArguments("Hello, World!");
    var cancellationToken = CancellationToken.None;
    var validation = CommandResultValidation.ZeroExitCode;
    bool silent = false;

    // Act
    var (exitCode, result) = await CLI.RunAsync(command, cancellationToken, validation, silent);

    // Assert
    Assert.Equal(0, exitCode);
    Assert.Contains("Hello, World!", result, StringComparison.Ordinal);

    // Cleanup
    Environment.SetEnvironmentVariable("DEBUG", null);
  }

  /// <summary>
  /// Tests that the <see cref="CLI.RunAsync"/> method returns the expected exit code and stderr result
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task RunAsync_GivenInvalidCommand_ReturnsOneExitCodeAndNoOutput()
  {
    // Arrange
    var command = new Command("ech")
      .WithArguments("Hello, World!");
    var cancellationToken = CancellationToken.None;
    var validation = CommandResultValidation.ZeroExitCode;
    bool silent = false;

    // Act
    var (exitCode, result) = await CLI.RunAsync(command, cancellationToken, validation, silent);

    // Assert
    Assert.Equal(1, exitCode);
    Assert.True(string.IsNullOrEmpty(result));
  }

  /// <summary>
  /// Tests that the <see cref="CLI.RunAsync"/> method returns the expected exit code and stderr result
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task RunAsync_GivenInvalidArgument_ReturnsOneExitCodeAndStderr()
  {
    // Arrange
    var command = new Command("cat")
      .WithArguments("--invalid")
      .WithValidation(CommandResultValidation.ZeroExitCode);
    var cancellationToken = CancellationToken.None;
    bool silent = false;

    // Act
    var (exitCode, result) = await CLI.RunAsync(command, cancellationToken, CommandResultValidation.ZeroExitCode, silent);

    // Assert
    Assert.Equal(1, exitCode);
    Assert.True(!string.IsNullOrEmpty(result));
  }

  /// <summary>
  /// Tests that the <see cref="CLI.RunAsync"/> method throws an <see cref="ArgumentNullException"/> when the command is null
  /// </summary>
  [Fact]
  public async Task RunAsync_GivenNullCommand_ReturnsArgumentNullException()
  {
    // Arrange
    Command command = null!;
    var cancellationToken = CancellationToken.None;
    var validation = CommandResultValidation.ZeroExitCode;
    bool silent = false;

    // Act
    async Task Act() => await CLI.RunAsync(command, cancellationToken, validation, silent).ConfigureAwait(false);

    // Assert
    _ = await Assert.ThrowsAsync<ArgumentNullException>(Act);
  }

}


