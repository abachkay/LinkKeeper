﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(LinkKeeper.API.Startup))]

namespace LinkKeeper.API
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
