using Autofac;
using Autofac.Integration.WebApi;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DispatchService
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ConfigureEndpoint();
        }

        private void ConfigureEndpoint()
        {
            var endpointConfiguration = new EndpointConfiguration("FireOnWheels.Order.Endpoint");
            endpointConfiguration.UseTransport<MsmqTransport>();
            endpointConfiguration.UsePersistence<InMemoryPersistence>();
            endpointConfiguration.EnableInstallers();
            endpointConfiguration.SendFailedMessagesTo("error");

            var endpoint = Endpoint.Start(endpointConfiguration).GetAwaiter().GetResult();

            var mvcContainerBuilder = new ContainerBuilder();
            mvcContainerBuilder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            mvcContainerBuilder.RegisterInstance(endpoint);
            var mvcContainer = mvcContainerBuilder.Build();

            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(mvcContainer);

        }

    }
}
