using System;
using System.Collections.Generic;
using System.Text;

namespace StaticBlogGenerator.Models
{
    public class TemplateVariablesModel
    {
        public Dictionary<string, object> Site { get; set; }
        public Dictionary<string, string> Page { get; set; }
        public Dictionary<string, string> Template { get; set; }
        public string Content { get; set; }
    }
}
