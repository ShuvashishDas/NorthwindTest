using System;
using System.Web;

namespace Shuvashish.Models
{
    public class PagingHelper
    {
        public static int PageNo
        {
            get
            {
                if (HttpContext.Current.Session["PageNo"] == null)
                    HttpContext.Current.Session["PageNo"] = 1;
                return Convert.ToInt32(HttpContext.Current.Session["PageNo"]);
            }
            set { HttpContext.Current.Session["PageNo"] = value; }
        }

        public static int PageSize
        {
            get
            {
                if (HttpContext.Current.Session["PageSize"] == null)
                    HttpContext.Current.Session["PageSize"] = 1;
                return Convert.ToInt32(HttpContext.Current.Session["PageSize"]);
            }
            set { HttpContext.Current.Session["PageSize"] = value; }
        }

        public static int SortColumn
        {
            get
            {
                if (HttpContext.Current.Session["SortColumn"] == null)
                    HttpContext.Current.Session["SortColumn"] = 1;
                return Convert.ToInt32(HttpContext.Current.Session["SortColumn"]);
            }
            set { HttpContext.Current.Session["SortColumn"] = value; }
        }

        public static bool IsDescending
        {
            get
            {
                if (HttpContext.Current.Session["IsDescending"] == null)
                    HttpContext.Current.Session["IsDescending"] = false;
                return Convert.ToBoolean(HttpContext.Current.Session["PageNo"]);
            }
            set { HttpContext.Current.Session["PageNo"] = value; }
        }

        public static int PageCount
        {
            get
            {
                if (HttpContext.Current.Session["PageCount"] == null)
                    HttpContext.Current.Session["PageCount"] = 1;
                return Convert.ToInt32(HttpContext.Current.Session["PageCount"]);
            }
            set { HttpContext.Current.Session["PageCount"] = value; }
        }

        public static int FilterCriteria
        {
            get
            {
                if (HttpContext.Current.Session["FilterCriteria"] == null)
                    HttpContext.Current.Session["FilterCriteria"] = 0;
                return Convert.ToInt32(HttpContext.Current.Session["FilterCriteria"]);
            }
            set { HttpContext.Current.Session["FilterCriteria"] = value; }
        }

        public static int FilterValue
        {
            get
            {
                if (HttpContext.Current.Session["FilterValue"] == null)
                    HttpContext.Current.Session["FilterValue"] = 0;
                return Convert.ToInt32(HttpContext.Current.Session["FilterValue"]);
            }
            set { HttpContext.Current.Session["FilterValue"] = value; }
        }

        public static string FilterValueString
        {
            get
            {
                if (HttpContext.Current.Session["FilterValueString"] == null)
                    HttpContext.Current.Session["FilterValueString"] = string.Empty;
                return HttpContext.Current.Session["PageNo"].ToString();
            }
            set { HttpContext.Current.Session["PageNo"] = value; }
        }
    }
}