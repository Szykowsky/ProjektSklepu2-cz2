using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projekt.Infrastructure
{
    public static class UrlHelpers
    {
        public static string IkonyKategoriiSciezka(this UrlHelper helper, string nazwaIkonyKategorii)
        {
            var IkonyKategoriiFolder = AppConfig.IkonyKategoriiFolderWzgledny;
            var sciezka = Path.Combine(IkonyKategoriiFolder, nazwaIkonyKategorii);
            var sciezkaBezwzgledna = helper.Content(sciezka);

            return sciezkaBezwzgledna;
        }
    }
}