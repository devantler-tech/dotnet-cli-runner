# ▶️ .NET CLI Runner

[![License](https://img.shields.io/badge/License-Apache_2.0-blue.svg)](https://opensource.org/licenses/Apache-2.0)
[![Test](https://github.com/devantler-tech/dotnet-cli-runner/actions/workflows/test.yaml/badge.svg)](https://github.com/devantler-tech/dotnet-cli-runner/actions/workflows/test.yaml)
[![codecov](https://codecov.io/gh/devantler-tech/dotnet-cli-runner/graph/badge.svg?token=RhQPb4fE7z)](https://codecov.io/gh/devantler-tech/dotnet-cli-runner)

<details>
  <summary>Show/hide folder structure</summary>

<!-- readme-tree start -->
```
.
├── .github
│   └── workflows
├── Devantler.CLIRunner
└── Devantler.CLIRunner.Tests
    └── CLITests

6 directories
```
<!-- readme-tree end -->

</details>

A simple CLI runner, that can execute CLI commands.

## 🚀 Getting Started

To get started, you can install the package from NuGet.

```bash
dotnet add package Devantler.CliRunner
```

## 📝 Usage

> [!NOTE]
> The template engine uses [CLIWrap](https://github.com/Tyrrrz/CliWrap) under the hood. So to learn more about the API, you can visit the above link.

To run a command, you can use the `CLI` class.

```csharp
using Devantler.CLIRunner;

var command = new Command("echo")
  .WithArguments("Hello, World!");
var cancellationToken = CancellationToken.None;
var validation = CommandResultValidation.ZeroExitCode;
bool silent = false;

var (exitCode, result) = await CLI.RunAsync(command, cancellationToken, validation, silent);

Console.WriteLine(exitCode); // Will output 0, as the command was successful
Console.WriteLine(result); // Will output "Hello, World!", as that is what is printed to stdout
```

You can run all kinds of commands with this library, and it will handle the output and exit code for you, such that you can easily check if the command was successful or not.
