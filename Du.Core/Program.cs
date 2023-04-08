using CSRedis;
using Du.Core.Config;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//设置当前路径
var dllFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
Directory.SetCurrentDirectory(dllFolder);
builder.Configuration.SetBasePath(dllFolder);

//读取参数
builder.Services.Configure<AppSetting>(builder.Configuration.GetSection("AppSetting"));
//json格式化
builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});
//配置日志
builder.Host.ConfigureLogging(
    (hosttingContext, logging) =>
    {
        logging.ClearProviders();
        logging.AddLog4Net();
    });
//调用服务
builder.Host.AddSuperSocket();

var app = builder.Build();

//调用Redis
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
    //暂不使用IDistributedCache
    //builder.Services.AddSingleton<IDistributedCache>(new Microsoft.Extensions.Caching.Redis.CSRedisCache(RedisHelper.Instance));
}

app.UseRouting();
app.UseAuthorization();
app.MapControllers();
await app.RunAsync();