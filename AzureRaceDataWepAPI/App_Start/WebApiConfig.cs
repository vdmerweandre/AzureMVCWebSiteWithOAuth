using System;
using System.Collections.Generic;
using System.Linq;
//using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Web.Http.OData.Builder;
using AzureRaceDataWebAPI.Models;

namespace AzureRaceDataWebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            config.Filters.Add(new AzureRaceDataWebAPI.Filters.ValidateHttpAntiForgeryTokenAttribute());

            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Meeting>("Meetings");
            builder.EntitySet<RaceStartItem>("RaceStartItem");
            builder.EntitySet<CoverageItem>("CoverageItem");

            config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());

            //Use JSON formatting by default
            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            config.EnableCors();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
