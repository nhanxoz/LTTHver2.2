using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace LTTHver2._2
{
    public class notification : Hub
    {
        public class NotifyMessage
        {
            public string Message { get; set; }
        }
        public async Task SendMessage(NotifyMessage message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}