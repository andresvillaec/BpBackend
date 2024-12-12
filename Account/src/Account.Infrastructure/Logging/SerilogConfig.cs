using Microsoft.AspNetCore.Builder;
using Serilog;

public static class SerilogConfig
{
    public static void ConfigureSerilog(WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((ctx, lc) => lc
            .WriteTo.Console()
            .WriteTo.File("logs/account_api.txt", rollingInterval: RollingInterval.Day)
            .ReadFrom.Configuration(ctx.Configuration));
    }
}
