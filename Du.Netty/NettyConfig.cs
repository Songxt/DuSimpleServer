namespace Du.Netty;

public class NettyConfig
{
    public string Name { get; set; }

    public List<ListenOptions> Listeners { get; set; }

    public int ClearIdleSessionInterval { get; set; } = 10;

    public int IdleSessionTimeOut { get; set; } = 120;

    public int BackLog { get; set; }

    public bool NoDelay { get; set; }

    public class ListenOptions
    {
        public string Ip { get; set; }

        public int Port { get; set; }
    }
}