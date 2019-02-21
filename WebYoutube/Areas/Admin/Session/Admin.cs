using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebYoutube.Areas.Admin.Session
{
    public class Admin

    {
        public static string ADMIN_SESSTION = "ADMIN_SESSTION";

        public static string Email { get; set; }
        public static string FullName { get; set; }

        public static int ID { get; set; }
        public static string Position { get; set; }
    }
}