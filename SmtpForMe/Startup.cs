﻿using Owin;
using System.Web.Http;

namespace SmtpForMe
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            app.UseWebApi(config);
            app.MapSignalR();
        }
    }
}
