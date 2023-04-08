using System.Reflection;
using Du.SuperSocket.Cmd;
using Du.SuperSocket.Server;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using SuperSocket;
using SuperSocket.Command;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class SuperSocketExtensions
{
    public static void AddSuperSocket(this IHostBuilder host)
    {
        host.ConfigureAppConfiguration(((context, builder) =>
            {
                builder.AddJsonFile("socket.json", false, false);
            }))
            .AsSuperSocketHostBuilder<Package, SimpleFilter>()
            //.ConfigureSuperSocket(options =>
            //{
            //    options.ClearIdleSessionInterval = 2;
            //    options.IdleSessionTimeOut = 5;
            //    options.AddListener(new ListenOptions
            //    {
            //        Ip = "Any",
            //        Port = 1040
            //    }
            //    );
            //})
            .UseHostedService<SimpleServer<Package>>()
            .UseSession<SimpleSession>()
            .UseCommand(commandOptions =>
            {
                commandOptions.AddCommandAssembly(typeof(Cmd376_01).GetTypeInfo().Assembly);
            })
            .UseClearIdleSession()
            .UseInProcSessionContainer()
            .AsMinimalApiHostBuilder()
            .ConfigureHostBuilder(); 
    }
}