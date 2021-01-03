using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace StaticBlogGenerator
{
    /// <summary>
    /// 각 Manager 클래스들을 조합해 실질적인 기능을 만들어 구현한 클래스
    /// </summary>
    public class Generator
    {
        #region Fields

        private readonly string _basePath           = null;
        private readonly string _staticModelsPath   = null;
        private readonly string _configPath         = null;

        private readonly TemplateManager    templateManager = null;
        private readonly PostManager        postManager     = null;
        private readonly PageManager        pageManager     = null;

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="basePath">Blog파일들의 기본경로</param>
        /// <param name="staticModelsPath">Generator에서 사용되는 파일의 경로</param>
        public Generator(string basePath, string staticModelsPath)
        {
            _basePath = basePath;
            _staticModelsPath = staticModelsPath;
            _configPath = _basePath + "_config.json";

            templateManager = new TemplateManager(_basePath);
            postManager = new PostManager(_basePath);
            pageManager = new PageManager(_basePath);
        }

        public void Build()
        {
            string text = PostRender(postManager.GetPost("FirstPost.md"));
            Console.WriteLine(text);
        }


        public string PostRender(string markdown)
        {
            Dictionary<string, string> header = postManager.ParseHeader(markdown);
            string body = postManager.parseBody(markdown);

            string text = templateManager.ApplypostTemplate(postManager.MarkdownToHTML(body), header);
            var result = templateManager.ApplyLayoutTemplate(text);
            return result;
        }
    }
}
