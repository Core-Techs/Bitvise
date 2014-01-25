using System.Web.Http;
using CoreTechs.Bitvise.WebService.Infrastructure;
using CoreTechs.Logging;
using Owin;

namespace CoreTechs.Bitvise.WebService
{
    public class WebApiStartup
    {
        // This code configures Web API. The WebApiStartup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            appBuilder.Use<RequestLogger>(LogManager.Global);
            config.DependencyResolver = new DependencyResolver();

            appBuilder.UseWebApi(config);

            
        }
    }
}