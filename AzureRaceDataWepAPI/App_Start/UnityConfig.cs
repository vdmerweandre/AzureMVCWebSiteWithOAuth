using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;
using Logging;
using Persistence.repositories;
using AzureRaceDataWebAPI.UnitOfWork;
using AzureRaceDataWebAPI.Models;
using AzureRaceDataWebAPI.Context;
using Persistence.datacontext;

namespace AzureRaceDataWebAPI
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterInstance<ILogger>(new Logger(), new ContainerControlledLifetimeManager());
            container.RegisterType<IDataContext, RaceDataDBContext>(new TransientLifetimeManager());
            container.RegisterType<IRaceDataUnitOfWork, RaceDataUnitOfWork>(new TransientLifetimeManager());
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}