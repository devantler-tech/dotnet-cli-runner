namespace Devantler.CLIRunner;

/// <summary>
/// An exception thrown when a CLI command fails.
/// </summary>
public class CLIException : Exception
{
  /// <summary>
  /// Initializes a new instance of the <see cref="CLIException"/> class.
  /// </summary>
  public CLIException() { }

  /// <summary>
  /// Initializes a new instance of the <see cref="CLIException"/> class with a specified error message.
  /// </summary>
  /// <param name="message"></param>
  public CLIException(string message) : base(message) { }

  /// <summary>
  /// Initializes a new instance of the <see cref="CLIException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
  /// </summary>
  /// <param name="message"></param>
  /// <param name="innerException"></param>
  public CLIException(string message, Exception innerException) : base(message, innerException) { }
}

