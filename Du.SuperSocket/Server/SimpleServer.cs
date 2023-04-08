using Microsoft.Extensions.Options;
using SuperSocket;
using SuperSocket.Server;

namespace Du.SuperSocket.Server
{
    public class SimpleServer<TReceivePackageInfo> : SuperSocketService<TReceivePackageInfo>
        where TReceivePackageInfo : class
    {
        public SimpleServer(IServiceProvider serviceProvider, IOptions<ServerOptions> serverOptions) : base(serviceProvider, serverOptions)
        {
        }

        protected override async ValueTask OnStartedAsync()
        {
        }

        protected override async ValueTask OnStopAsync()
        {
        }
    }
}
