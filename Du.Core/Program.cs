using CSRedis;
using Du.Core.Config;
using Du.SocketService;
using Du.SocketService.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);
//builder.Configuration.AddJsonFile("socket.json", false, false);
builder.Services.Configure<AppSetting>(builder.Configuration.GetSection("AppSetting"));
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});

builder.Host.ConfigureLogging(
    (hosttingContext, logging) =>
    {
        logging.ClearProviders();
        logging.AddLog4Net();
    });

builder.Host.AddSuperSocket();

var app = builder.Build();
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
    //‘›≤ª π”√IDistributedCache
    //builder.Services.AddSingleton<IDistributedCache>(new Microsoft.Extensions.Caching.Redis.CSRedisCache(RedisHelper.Instance));
}

app.MapControllers();
app.MapGet("/run/{id:int}", ([FromServices] SimpleServer<Package> server, int id) =>
{
    if (id == 1)
    {
        var result = server.StopAsync(new CancellationToken());
    }
    else
    {
        var result = server.StartAsync(new CancellationToken());
    }
    return "ok";
});

app.MapGet("/", () => { return "OK"; });

await app.RunAsync();