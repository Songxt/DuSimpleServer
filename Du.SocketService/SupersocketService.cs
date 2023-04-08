using System.Reflection;
using Du.SocketService.Cmd;
using Du.SocketService.Server;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SuperSocket;
using SuperSocket.Command;

namespace Du.SocketService
{
    public class SocketService : IHostedService
    {
        private readonly ILogger<SocketService> _logger;
        private IHost host;

        public SocketService(ILogger<SocketService> logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            host = SuperSocketHostBuilder.Create<Package, SimpleFilter>()
                //.UseHostedService<SimpleServer<Package>>()
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
                .UseSession<SimpleSession>()
                .UseCommand(commandOptions =>
                {
                    commandOptions.AddCommandAssembly(typeof(Cmd376_01).GetTypeInfo().Assembly);
                })
                .UseClearIdleSession()
                .UseInProcSessionContainer()
                .Build();
            _logger.LogInformation("正在启动");
            host.RunAsync();

            //return host.RunAsync();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            var ret =host.StopAsync();
            if (ret.IsCompleted)
            {
                return Task.CompletedTask;
            }
            return Task.FromException(new Exception());

        }
    }
}