﻿using System.Web;
using System.Web.Mvc;

namespace Search_as_a_service_azure
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
