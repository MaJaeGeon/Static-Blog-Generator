using Markdig;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace StaticBlogGenerator
{
    /// <summary>
    /// _templates 폴더에서 관리되는 파일들을 처리하는 클래스
    /// </summary>
    public class PostManager : Manager
    {
        private readonly string _basePath   = null;
        private readonly string _postsPath  = null;

        public PostManager(string basePath)
        {
            _basePath = basePath;
            _postsPath = _basePath + @"_posts\";
        }

        /// <summary>
        /// Markdown형식의 문자열을 HTML로 변환한다.
        /// </summary>
        /// <param name="markdownText">HTML 문자열</param>
        /// <returns></returns>
        public string MarkdownToHTML(string markdownText)
        {
            return Markdown.ToHtml(markdownText);
        }

        public string GetPost(string postPath)
        {
            return File.ReadAllText(_postsPath + postPath);
        }
    }
}