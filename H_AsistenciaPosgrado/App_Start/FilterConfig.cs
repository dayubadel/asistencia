﻿using System.Web;
using System.Web.Mvc;

namespace H_AsistenciaPosgrado
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
