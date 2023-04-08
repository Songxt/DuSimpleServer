using System.Reflection;
using Du.SocketService.Cmd;
using Du.SocketService.Server;
using Microsoft.Extensions.Hosting;
using SuperSocket;
using SuperSocket.Command;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class SuperSocketExtensions
{
    public static void AddSuperSocket(this IHostBuilder host)
    {
        host.AsSuperSocketHostBuilder<Package, SimpleFilter>()
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