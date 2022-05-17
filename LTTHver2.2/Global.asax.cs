using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace LTTHver2._2
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            var context = HttpContext.Current;
            var response = context.Response;

            response.AddHeader("Access-Control-Allow-Origin", "http://localhost:3000");
            response.AddHeader("Access-Control-Allow-Credentials", "true");
            response.AddHeader("X-Frame-Options", "ALLOW-FROM *");
            response.AddHeader("Access-Control-Allow-Methods", "GET, POST, DELETE, PATCH, PUT");
            response.AddHeader("Access-Control-Allow-Headers", "Access-Control-Allow-Headers, Origin,Authorization,Accept, X-Requested-With, Content-Type, Access-Control-Request-Method, Access-Control-Request-Headers");
            response.AddHeader("Access-Control-Max-Age", "1000000");
            
            if (context.Request.HttpMethod == "OPTIONS")
            {
                
                
                response.End();
            }
        }
    }

}

