using System;
using System.Collections.Generic;
using System.Text;

namespace StaticBlogGenerator.Models.Variables
{
    public class SiteVariableModel
    {
        public string Tags { get; set; }

        public IDictionary<string, object> Config { get; set; }
    }
}
