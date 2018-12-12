﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ClueLess
{
    public static class WebApiConfig
    {
        public static string UrlPrefix { get { return "api"; } }
        public static string UrlPrefixRelative { get { return "~/api"; } }

        public static void Register(HttpConfiguration config)
        {
            
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: UrlPrefix+"/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

           // config.Routes.MapHttpRoute(name: "ApiByAction", routeTemplate: "api/{controller}/{action}");
        }
    }
}
