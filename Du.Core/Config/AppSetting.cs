namespace Du.Core.Config
{
    public class AppSetting
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Du的小服务器";
        public bool UseRedis { get; set; }
    }
}