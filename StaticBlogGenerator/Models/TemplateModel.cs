using System.Collections.Generic;

namespace StaticBlogGenerator.Models
{
    public class TemplateModel
    {
        public Dictionary<string, string> TemplateHeader { get; set; }
        public string TemplateBody { get; set; }
    }
}