using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeduShop.Common
{
    public class ConfigHelper
    {
        public static string GetByKey(string key)
        {
            return ConfigurationManager.AppSettings[key].ToString();
        }
        public const string SiteUrl = "http://xiposg.vn";
        public const string SiteName = "xiposg.vn";
        public const string SiteUrlHttps = "https://xiposg.vn";
    }
}
