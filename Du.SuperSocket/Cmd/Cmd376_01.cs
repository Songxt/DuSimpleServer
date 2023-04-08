using Du.SuperSocket.Server;
using SuperSocket;
using SuperSocket.Command;

namespace Du.SuperSocket.Cmd
{
    /// <summary>
    /// 链路接口检测
    /// </summary>
    [Command(Key = "376_01")]
    public class Cmd376_01 : IAsyncCommand<SimpleSession, Package>
    {
        public ValueTask ExecuteAsync(SimpleSession session, Package package)
        {
           return ((IAppSession)session).SendAsync(new byte[] {
                0x01
            });
        }
    }
}
