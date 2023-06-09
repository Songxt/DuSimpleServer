﻿using System.Net;
using Du.Core.Config;
using Du.SuperSocket.Mq;
using Du.SuperSocket.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SuperSocket;

namespace Du.Core.Controller;

[Route("api/[action]")]
[ApiController]
public class ApiController : ControllerBase
{
    private readonly ISessionContainer _sessions;
    private readonly IOptions<AppSetting> _config;

    public ApiController([FromServices] ISessionContainer sessions, IOptions<AppSetting> config)
    {
        _sessions = sessions;
        _config = config;
    }

    [HttpPost]
    public dynamic Session()
    {
        var sessions = _sessions.GetSessions();
        var query = from item in sessions
                     select new
                     {
                         item.SessionID,
                         item.StartTime,
                         item.LastActiveTime,
                         Ip = ((IPEndPoint)item.RemoteEndPoint).Address.ToString() + ":" + ((IPEndPoint)item.RemoteEndPoint).Port,
                     };
        return query;
        //return sessions.Select(item => new
        //{
        //    item.SessionID,
        //    item.StartTime,
        //    item.LastActiveTime,
        //    naem =((IPEndPoint)item.RemoteEndPoint).Address.ToString(),
        //    ((IPEndPoint)item.RemoteEndPoint).Port
        //});
    }

    [HttpPost]
    public void Publish([FromBody] Message msg)
    {
        if (msg.Boardcast)
        {
            var sessions = _sessions.GetSessions();
            foreach (var session in sessions)
            {
                session?.SendAsync(new byte[] { 0x01 });
            }
        }
        else
        {
            var session = _sessions.GetSessionByID(msg.SessionId);
            session?.SendAsync(new byte[] { 0x01 });
        }
    }

    public void Test()
    {
        IAppSession session = _sessions.GetSessions().FirstOrDefault();
        RedisHelper.Publish(_config.Value.Id + "_Send", JsonConvert.SerializeObject(new Message()
        {
            SessionId = session?.SessionID,
            Sn = "",
            Data = new byte[]
            {
                0x01,0x2,0x3
            }
        }));
    }

    [HttpGet]
    [Route("/{action}/{id:int}")]
    public string Run([FromServices] SimpleServer<Package> server, int id)
    {
        Task task;
        if (id == 1)
        {
            task = server.StartAsync(new CancellationToken());
        }
        else
        {
            task = server.StopAsync(new CancellationToken());
        }

        if (task.IsCompletedSuccessfully)
        {
            return "OK";
        }
        else
        {
            return "False";
        }
    }
}