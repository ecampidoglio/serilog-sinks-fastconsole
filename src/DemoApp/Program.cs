using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.FastConsole;

Random _random = new();

// enable serilog to log out internal messages to console for debugging
Serilog.Debugging.SelfLog.Enable(Console.WriteLine);

// see serilog .net 6 integration: https://blog.datalust.co/using-serilog-in-net-6/
Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateBootstrapLogger();
Log.Information("Starting up");

try
{
    var logsCount = 5000;
    var sinkOptions = new FastConsoleSinkOptions { /*QueueLimit = 10, BlockWhenFull = true*/ };

    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog((ctx, lc) =>
    {
        lc.MinimumLevel.Debug().Enrich.FromLogContext().WriteTo.FastConsole(sinkOptions);
        //.WriteTo.File("log.txt", shared: true, restrictedToMinimumLevel: LogEventLevel.Debug)
        //.WriteTo.Console()
    });

    var app = builder.Build();
    app.UseSerilogRequestLogging();

    app.MapGet("/", async ([FromServices] Microsoft.Extensions.Logging.ILogger<Program> _logger) =>
    {
        var sw = Stopwatch.StartNew();
        var tasks = Enumerable.Range(1, logsCount).Select(t => Task.Run(() => _logger.LogInformation(CreateRandomString(100))));
        await Task.WhenAll(tasks);

        return $"Logging random messages to console" +
        $"\n logs: {logsCount}" +
        $"\n queue limit: {sinkOptions.QueueLimit}" +
        $"\n queue block: {sinkOptions.BlockWhenFull}" +
        $"\n milliseconds: {sw.ElapsedMilliseconds}" +
        $"\n threads: {Process.GetCurrentProcess().Threads.Count}";
    });

    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}


string CreateRandomString(int length)
{
    var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    var stringChars = new char[length];

    for (var i = 0; i < stringChars.Length; i++)
        stringChars[i] = chars[_random.Next(chars.Length)];

    return new string(stringChars);
}
