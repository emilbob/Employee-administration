using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using Unity;
using EvidencijaZaposlenih.Interfaces;
using EvidencijaZaposlenih.Repository;
using EvidencijaZaposlenih.Resolver;
using Unity.Lifetime;
using System.Web.Http.Cors;

namespace EvidencijaZaposlenih
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

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // CORS
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

           ;

            // Unity
            var container = new UnityContainer();
            container.RegisterType<IKompanijaRepository, KompanijaRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IZaposlenRepository, ZaposleniRepository>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);
        }
    }
}
