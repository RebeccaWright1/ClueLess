using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClueLess.Models
{
    public class Configuration
    {
        public static void CreateConfiguration()
        {

        }

        public static void SaveConfiguration()
        {

        }

        public  int ID { get; set; }
        public  int UserID { get; set; }
        public  string Name { get; set; }
        public  bool IsShared { get; set; }

    }
}