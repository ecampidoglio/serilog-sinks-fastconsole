# Changelog

Readme: https://github.com/manigandham/serilog-sinks-fastconsole/blob/master/README.md

## 2.4.0

-   Fixed bug in waiting for underlying task on dispose.
-   Added `net7.0` target framework.

## 2.3.0

-   Refactored code for more efficient console output and thread blocking when queue is full. Use `IAsyncEnumerable` when available in runtime.
-   Refactored DemoApp to simplify code and display results of sample logging.
-   Dropped `net5.0` target framework.

## 2.2.0

-   Added `BlockWhenFull` option to block when in-memory queue is full instead of dropping new log entries.

## 2.1.0

-   Fixed potential bug with channel close during repeated dispose.
-   Updated nuget references.

## 2.0.0

-   Removed obsolete `CustomJsonWriter` handling.
-   Added `textFormatter` to extension method for Serilog config.

## 1.6.0

-   Added `net6.0` target framework.

## 1.5.0

-   Sink updated to use `ITextFormatter` for any custom formatting, in addition to basic plaintext and JSON formatting included by default.
-   `CustomJsonWriter` (of type `Action<LogEvent, TextWriter>`) is now obsolete. Provide an implementation of `ITextFormatter` instead.

## 1.4.1

-   Added `ConfigureAwait(false)` to avoid SynchronizationContext when used in .NET Framework.

## 1.4.0

-   Updated target frameworks to `netstandard2.0`, `netstandard2.1`, `net5.0`.
-   Added `restrictedToMinimumLevel` and `loggingLevelSwitch` (standard Serilog) options to sink config.
-   Improved performance by using StringBuilder internal buffer directly (only `net5.0` and higher).
-   Enabled nullable reference types for project and updated type definitions for extra safety.
-   Updated nuget references.

## 1.3.2

-   Added `QueueLimit` option added to bound the in-memory queue used for log entries. This is useful for adding back-pressure to avoid out-of-memory issues with high-volume logging.
-   Updated nuget references.

## 1.3.1

-   Added `netstandard2.0` to improve dependency graph in newer solutions. See guidance at https://docs.microsoft.com/en-us/dotnet/standard/library-guidance/cross-platform-targeting

## 1.3.0

-   Fix bug with default JSON object writer ending quote.

## 1.2.0

-   Config options to toggle JSON output with a single sink.
-   Extension methods for Serilog log configuration.
-   Allow custom delegate to replace JSON object writer.

## 1.0.0

-   Initial console sinks for plaintext and JSON.
