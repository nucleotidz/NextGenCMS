using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Web.Http.Cors;
using Newtonsoft.Json;
namespace NextGenCMS.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var json = config.Formatters.JsonFormatter;
            var settings = json.SerializerSettings;
            json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.None;
            config.Formatters.Remove(config.Formatters.XmlFormatter);  
            config.Formatters.Add( new MultipartDataMediaFormatter.FormMultipartEncodedMediaTypeFormatter());
        
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            settings.NullValueHandling = NullValueHandling.Include;
            var corsAttr = new EnableCorsAttribute("*", "*", "*");
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            config.EnableCors(corsAttr);
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
              name: "Parametric",
              routeTemplate: "api/{controller}/{action}/{id1}/{id2}",
              defaults: new
              {
                  id1 = RouteParameter.Optional,
                  id2 = RouteParameter.Optional
              });
        }
    }
}
