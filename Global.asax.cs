using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using TravelMaker.Models;

namespace TravelMaker
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private TravelMakerEntities db = new TravelMakerEntities();
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);
            //http://....../?json=true
            //GlobalConfiguration.Configuration.Formatters.JsonFormatter.MediaTypeMappings.Add(new QueryStringMapping("json", "true", "application/json"));

            //http://....../?xml=true
            //GlobalConfiguration.Configuration.Formatters.XmlFormatter.MediaTypeMappings.Add(new QueryStringMapping("xml", "true", "application/xml"));
            var ArractionCount = db.Attraction.Select(p => new
            {
                p.attr_Id,
                p.attractionCount
            });
            foreach (var p in ArractionCount)
            {
                userDictionary.TM_ATTRACTION_COUNT[Convert.ToInt32(p.attr_Id.ToString())] = Convert.ToInt32(p.attractionCount.ToString());
            }
        }
    }
}
