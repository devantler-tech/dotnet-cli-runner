using CliWrap;

namespace Devantler.CLIRunner.Tests.Integration;

/// <summary>
/// Integration tests for the <see cref="CLIRunner.RunAsync"/> method.
/// </summary>
public class RunAsyncTests
{
  /// <summary>
  /// Tests that the <see cref="CLIRunner.RunAsync"/> method returns the expected exit code and stdout result
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task RunAsync_WithValidCommand_ReturnsZeroExitCodeAndStdout()
  {
    // Arrange
    Environment.SetEnvironmentVariable("DEBUG", "true");
    var command = new Command("echo")
      .WithArguments("Hello, World!");
    var cancellationToken = CancellationToken.None;
    var validation = CommandResultValidation.ZeroExitCode;
    bool silent = false;

    // Act
    var (exitCode, result) = await CLIRunner.RunAsync(command, cancellationToken, validation, silent).ConfigureAwait(false);

    // Assert
    Assert.Equal(0, exitCode);
    //assert message
    Assert.Contains("Hello, World!", result, StringComparison.Ordinal);
  }

  /// <summary>
  /// Tests that the <see cref="CLIRunner.RunAsync"/> method returns the expected exit code and stderr result
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task RunAsync_WithInvalidCommand_ReturnsOneExitCodeAndNoOutput()
  {
    // Arrange
    var command = new Command("ech")
      .WithArguments("Hello, World!");
    var cancellationToken = CancellationToken.None;
    var validation = CommandResultValidation.ZeroExitCode;
    bool silent = false;

    // Act
    var (exitCode, result) = await CLIRunner.RunAsync(command, cancellationToken, validation, silent).ConfigureAwait(false);

    // Assert
    Assert.Equal(1, exitCode);
    Assert.True(string.IsNullOrEmpty(result));
  }

  /// <summary>
  /// Tests that the <see cref="CLIRunner.RunAsync"/> method returns the expected exit code and stderr result
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task RunAsync_WithInvalidArgument_ReturnsOneExitCodeAndStderr()
  {
    // Arrange
    var command = new Command("cat")
      .WithArguments("--invalid")
      .WithValidation(CommandResultValidation.ZeroExitCode);
    var cancellationToken = CancellationToken.None;
    bool silent = false;

    // Act
    var (exitCode, result) = await CLIRunner.RunAsync(command, cancellationToken, CommandResultValidation.ZeroExitCode, silent).ConfigureAwait(false);

    // Assert
    Assert.Equal(1, exitCode);
    Assert.True(!string.IsNullOrEmpty(result));
  }

  /// <summary>
  /// Tests that the <see cref="CLIRunner.RunAsync"/> method throws an <see cref="ArgumentNullException"/> when the command is null
  /// </summary>
  [Fact]
  public async Task RunAsync_WithNullCommand_ReturnsArgumentNullException()
  {
    // Arrange
    Command command = null!;
    var cancellationToken = CancellationToken.None;
    var validation = CommandResultValidation.ZeroExitCode;
    bool silent = false;

    // Act
    async Task Act() => await CLIRunner.RunAsync(command, cancellationToken, validation, silent).ConfigureAwait(false);

    // Assert
    _ = await Assert.ThrowsAsync<ArgumentNullException>(Act);
  }

}


