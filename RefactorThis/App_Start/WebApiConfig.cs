using refactor_me.Models;
using refactor_me.Xero___Repository;
using System.Web.Http;
using Unity;
using Unity.Lifetime;
using UnityResolving;

namespace refactor_this
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            // Web API configuration and services
            var formatters = GlobalConfiguration.Configuration.Formatters;
            formatters.Remove(formatters.XmlFormatter);
            formatters.JsonFormatter.Indent = true;

            // Web API routes
            config.MapHttpAttributeRoutes();

            var container = new UnityContainer();
            container.RegisterType<IProductRepository<Product>, ProductRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IProductRepository<ProductOptions>, ProductOptionsRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IProductContext, ProductContext>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { controller = "products", id = RouteParameter.Optional }
            );
        }
    }
}