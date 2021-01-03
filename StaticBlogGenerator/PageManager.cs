using System;
using System.Collections.Generic;
using System.Text;

namespace StaticBlogGenerator
{
    public class PageManager : Manager
    {
        private readonly string _basePath = null;
        private readonly string _pagesPath = null;

        public PageManager(string basePath)
        {
            _basePath = basePath;
            _pagesPath = _basePath + @"_pages\";
        }
    }
}