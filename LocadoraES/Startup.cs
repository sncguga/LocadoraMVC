﻿using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using System.Web.Helpers;

[assembly: OwinStartup(typeof(LocadoraES.Startup))]

namespace LocadoraES
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/Autenticacao/Login")
            });

            AntiForgeryConfig.UniqueClaimTypeIdentifier = "Login";

        }
    }
}
