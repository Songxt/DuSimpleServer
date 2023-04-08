using SuperSocket;
using SuperSocket.Channel;
using SuperSocket.Server;

namespace Du.SuperSocket.Server
{
    public class SimpleSession : AppSession
    {
        public string Sn { get; set; }

        protected override async ValueTask OnSessionConnectedAsync()
        {

        }

        protected override async ValueTask OnSessionClosedAsync(CloseEventArgs e)
        {
        }

        /// <summary>
        ///     发送数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ValueTask SendAsync(ReadOnlyMemory<byte> data)
        {
            return ((IAppSession)this).SendAsync(data);
        }
    }
}
