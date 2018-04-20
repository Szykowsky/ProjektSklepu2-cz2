using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Projekt.Infrastructure
{
    public class AppConfig
    {
        public static string _ikonyKategoriiFolderWzgledny = ConfigurationManager.AppSettings["IkonyKategoriiFolder"];

        public static string IkonyKategoriiFolderWzgledny
        {
            get
            {
                return _ikonyKategoriiFolderWzgledny;
            }
        }

    }
}