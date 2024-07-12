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
  public async Task RunAsync_WithValidCommand_ReturnsZeroExitCodeAndCorrectResult()
  {
    // Arrange
    Environment.SetEnvironmentVariable("DEBUG", "true");
    var command = new Command("echo")
      .WithArguments("Hello, World!");
    var cancellationToken = CancellationToken.None;
    var validation = CommandResultValidation.ZeroExitCode;
    bool silent = false;

    // Act
    var (exitCode, result) = await CLIRunner.RunAsync(command, cancellationToken, validation, silent);

    // Assert
    Assert.Equal(0, exitCode);
    //assert message
    Assert.Contains("Hello, World!", result);
  }

  /// <summary>
  /// Tests that the <see cref="CLIRunner.RunAsync"/> method returns the expected exit code
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task RunAsync_WithInvalidCommand_ReturnsNonZeroExitCode()
  {
    // Arrange
    var command = new Command("ech")
      .WithArguments("Hello, World!");
    var cancellationToken = CancellationToken.None;
    var validation = CommandResultValidation.ZeroExitCode;
    bool silent = false;

    // Act
    var (exitCode, _) = await CLIRunner.RunAsync(command, cancellationToken, validation, silent);

    // Assert
    Assert.Equal(1, exitCode);
  }

  /// <summary>
  /// Tests that the <see cref="CLIRunner.RunAsync"/> method returns the expected exit code and stderr result
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task RunAsync_WithValidCommand_ReturnsZeroExitCodeAndCorrectError()
  {
    // Arrange
    var command = new Command("cat")
      .WithArguments("--invalid")
      .WithValidation(CommandResultValidation.ZeroExitCode);
    var cancellationToken = CancellationToken.None;
    bool silent = false;

    // Act
    var (exitCode, result) = await CLIRunner.RunAsync(command, cancellationToken, CommandResultValidation.ZeroExitCode, silent);

    // Assert
    Assert.Equal(1, exitCode);
    Assert.NotEmpty(result);
  }
}


