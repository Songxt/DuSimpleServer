using CSRedis;
using Du.Core.Config;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//���õ�ǰ·��
var dllFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
Directory.SetCurrentDirectory(dllFolder);
builder.Configuration.SetBasePath(dllFolder);

//��ȡ����
builder.Services.Configure<AppSetting>(builder.Configuration.GetSection("AppSetting"));
//json��ʽ��
builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});
//������־
builder.Host.ConfigureLogging(
    (hosttingContext, logging) =>
    {
        logging.ClearProviders();
        logging.AddLog4Net();
    });
//���÷���
builder.Host.AddSuperSocket();

var app = builder.Build();

//����Redis
var config = app.Services.GetRequiredService<IOptions<AppSetting>>();
if (Convert.ToBoolean(config.Value.UseRedis))
{
    var csredis = new CSRedisClient(builder.Configuration.GetConnectionString("Redis"));
    RedisHelper.Initialization(csredis);
    //csredis.Subscribe((config.Value.Id + "_Send", args =>
    //{
    //    var msg = JsonConvert.DeserializeObject<Message>(args.Body);
    //    var container = app.Services.GetRequiredService<ISessionContainer>();
    //    if (msg.Boardcast)
    //        foreach (var session in container.GetSessions())
    //            session.SendAsync(msg.Data);
    //    else
    //        container.GetSessionByID(msg.SessionId)?.SendAsync(msg.Data);
    //}
    //));
    //�ݲ�ʹ��IDistributedCache
    //builder.Services.AddSingleton<IDistributedCache>(new Microsoft.Extensions.Caching.Redis.CSRedisCache(RedisHelper.Instance));
}

app.UseRouting();
app.UseAuthorization();
app.MapControllers();
await app.RunAsync();