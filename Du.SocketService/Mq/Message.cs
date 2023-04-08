namespace Du.SocketService.Mq
{
    public class Message
    {
        public string SessionId { get; set; }
        public string Sn { get; set; }
        public bool Boardcast { get; set; }
        public int Priority { get; set; }
        public byte[] Data { get; set; }
    }
}
