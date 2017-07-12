using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using LightInject;
using Wheather.Services;
using Wheather.Services.Implementations;
using Wheather.Services.Interfaces;

namespace Wheather
{
    using Wheather.Models.Db;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var container = new ServiceContainer();
            container.RegisterControllers();

            container.Register<IWeatherService, WeatherService>(new PerScopeLifetime());
            container.Register<IRequestService, RequestService>(new PerScopeLifetime());
            container.Register<IActionLogger, DbActionLogger>(new PerScopeLifetime());
            container.Register<WeatherDb>(new PerRequestLifeTime());

            container.EnableMvc();
        }
    }
}
