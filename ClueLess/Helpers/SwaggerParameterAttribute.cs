using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClueLess.Helpers
{
    public class SwaggerParameterAttribute:Attribute
    {
        public SwaggerParameterAttribute(string name, string desciption)
        {
           
        }

        public String Name { get; set; }
        public String Description { get; set; }
        public String Type { get; set; } = "text";
        public bool Required { get; set; } = false;
    }
}