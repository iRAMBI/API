using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Diagnostics;

namespace BBBAPI2.Hubs
{
    public class NewsfeedHub : Hub
    {
        //trigger updating users when someone posts an article
        public static void triggerNews()
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<NewsfeedHub>();
            hubContext.Clients.All.newNews();
        }

        public void Hello()
        {
            Clients.All.hello("SignalR Says ELLOOOOOOOO");
        }

        //inform users there is a new news item on their feed and to check
        public void GetNewsfeed()
        {
            Clients.All.newNews();

        }
    }
}