using Microsoft.AspNetCore;

namespace TaskManager.ApiGateway
{
public class Program
{
    public static void Main(string[] args)
    {
        BuildWebHost(args).Run();
    }

    public static IWebHost BuildWebHost(string[] args)
    {
        var builder = WebHost.CreateDefaultBuilder(args);

        builder.ConfigureServices(s => s.AddSingleton(builder))
        .UseStartup<Startup>()
        .ConfigureAppConfiguration((hostingContext, config) =>
        {
            config
            .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
            .AddJsonFile("ocelot.json")
            .AddEnvironmentVariables();
        });

        var host = builder.Build();
        return host;
    }
}
}