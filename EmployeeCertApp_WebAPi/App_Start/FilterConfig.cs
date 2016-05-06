using System.Web;
using System.Web.Mvc;

namespace EmployeeCertApp_WebAPi
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}