using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;

namespace BBBAPI2
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute("PostAuthentication", "api/auth", new { controller = "users", action = "postauthenticate"});

            config.Routes.MapHttpRoute("PostAppleToken", "api/auth/{userid}/appletoken/{appleToken}/{token}", new { controller = "users", action = "postsettoken" });

            config.Routes.MapHttpRoute("GetCriticalNews", "api/newsfeed/critical", new { controller = "news", action = "getcriticalnews" });

            config.Routes.MapHttpRoute("GetStandardNews", "api/newsfeed/{userid}/standard/{token}", new { controller = "news", action = "getstandardnews"});

            config.Routes.MapHttpRoute("GetNewsArticle", "api/newsfeed/{userid}/article/{newsid}/{token}", new { controller = "news", action = "getarticle" });

            config.Routes.MapHttpRoute("GetContacts", "api/contacts/{userid}/{token}", new { controller = "contact", action = "getcontact" });

            config.Routes.MapHttpRoute("GetMyCourses", "api/mycourses/{userid}/{token}", new { controller = "courses", action = "getcourses" });

            

           /* config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );*/


            
        }
    }
}
