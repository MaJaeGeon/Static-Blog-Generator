using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StaticBlogGenerator.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        private IDictionary<string, object> _siteVariables = null;

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

        /// <summary>
        /// 정적 사이트 생성기를 사용하기위한 구조를 생성한다.
        /// </summary>
        public void Initialize()
        {
            // Config 파일이 없다면 생성한다.
            if (!ConfigExists()) CreateConfig();

            _siteVariables = ReadConfig();

            // LayoutTemplate을 생성한다.
            //_templateManager.CreateLayoutTemplate();
        }

        public string PostRender(string markdown)
        {
            Dictionary<string, string> postHeader = postManager.ParseHeader(markdown);

            string postBody = postManager.parseBody(markdown);
            Dictionary<string, string> templateHeader = new Dictionary<string, string>();

            string text = templateManager.ApplyTemplate(postHeader["template"], new TemplateVariablesModel
            {
                Site = _siteVariables,
                Page = postHeader,
                Content = postManager.MarkdownToHTML(postBody)
            }, out templateHeader);

            var result = templateManager.ApplyLayoutTemplate(text, new TemplateVariablesModel
            {
                Site = _siteVariables,
                Template = templateHeader,
                Content = text
            });

            return result;
        }



        #region Config

        /// <summary>
        /// _config.json 파일이 존재하는지 알려준다.
        /// </summary>
        /// <returns>_config.json 이 존재하면 ture, 그렇지않다면 false를 반환한다.</returns>
        private bool ConfigExists()
        {
            return File.Exists(_configPath);
        }

        /// <summary>
        /// _config.json 파일을 Dictionary 타입으로 읽어온다.
        /// </summary>
        /// <param name="configPath">_config.json 경로</param>
        /// <returns>_config.json의 Dictionary</returns>
        private IDictionary<string, object> ReadConfig()
        {
            JObject job = JObject.Parse(File.ReadAllText(_configPath));
            job.ToObject<Dictionary<string, object>>();
            
            return JsonConvert.DeserializeObject<IDictionary<string, object>>(File.ReadAllText(_configPath));
        }

        /// <summary>
        /// _config.json 파일을 생성한다.
        /// </summary>
        private void CreateConfig()
        {
            string jsonString = File.ReadAllText(_staticModelsPath + "_config.json");
            File.Create(_configPath).Close();

            File.WriteAllText(_configPath, jsonString);
        }

        #endregion
    }
}