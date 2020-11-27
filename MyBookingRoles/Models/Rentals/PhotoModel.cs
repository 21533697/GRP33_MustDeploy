using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MyBookingRoles.Models.Rentals
{
    public class PhotoModel : List<Photos>
    {
        public PhotoModel(string folder)
        {
            string path = HttpContext.Current.Server.MapPath(folder);
            var di = new DirectoryInfo(path);

            foreach (var file in di.EnumerateFiles("*.jpg", SearchOption.TopDirectoryOnly))
            {
                var p = new Photos(string.Concat(folder, file.Name), Path.GetFileNameWithoutExtension(file.Name));
                Add(p);
            }
        }
    }
}