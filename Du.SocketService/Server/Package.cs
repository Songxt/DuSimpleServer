using SuperSocket.ProtoBase;

namespace Du.SocketService.Server
{
    public class Package : IKeyedPackageInfo<string>
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        ///   接收数据
        /// </summary>
        public byte[] Data { get; set; }
    }
}
