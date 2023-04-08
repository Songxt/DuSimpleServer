using Du.SocketService.Server;
using SuperSocket;
using SuperSocket.Command;

namespace Du.SocketService.Cmd
{
    /// <summary>
    /// 链路接口检测
    /// </summary>
    [Command(Key = "645_01")]
    public class Cmd645_01 : IAsyncCommand<SimpleSession, Package>
    {
        public ValueTask ExecuteAsync(SimpleSession session, Package package)
        {
            return ((IAppSession)session).SendAsync(new byte[] {
                0x01
            });
        }
    }
}