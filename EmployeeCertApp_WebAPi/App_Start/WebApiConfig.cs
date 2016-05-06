using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace EmployeeCertApp_WebAPi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
          
            config.MapHttpAttributeRoutes();
            config.EnableCors();
            
        }
    }
}
