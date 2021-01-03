using DotLiquid;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;

namespace StaticBlogGenerator
{
    public class TemplateManager : Manager
    {
        private readonly string _basePath       = null;
        private readonly string _templatesPath   = null;

        public TemplateManager(string basePath)
        {
            _basePath = basePath;
            _templatesPath = _basePath + @"_templates\";
        }

        /// <summary>
        /// 템플릿을 적용한 페이지를 반환한다.
        /// </summary>
        /// <param name="templateName">템플릿 명</param>
        /// <param name="renderContent">템플릿에 적용할 컨텐츠</param>
        /// <returns></returns>
        public string ApplyTemplate(string templateName, string renderContent)
        {
            string templateContent = LoadTemplate(templateName);
            if (string.IsNullOrEmpty(templateContent)) return null;

            Template template = Template.Parse(parseBody(templateContent));

            var hash = Hash.FromAnonymousObject(new
            {
                content = renderContent
            });

            return template.Render(hash);
        }

        public string ApplyTemplate(string templateName, Hash hash)
        {
            string templateContent = LoadTemplate(templateName);
            if (string.IsNullOrEmpty(templateContent)) return null;

            Template template = Template.Parse(parseBody(templateContent));

            return template.Render(hash);
        }


        /// <summary>
        /// 레이아웃 템플릿을 적용한 페이지를 반환한다.
        /// </summary>
        /// <param name="renderContent">레이아웃 템플릿에 적용할 컨텐츠</param>
        /// <returns></returns>
        public string ApplyLayoutTemplate(string renderContent)
        {
            return ApplyTemplate("_layout.html", renderContent);
        }

        public string ApplypostTemplate(string renderContext, Dictionary<string, string>header)
        {
            dynamic eo = header.Aggregate(
                new ExpandoObject() as IDictionary<string, object>,
                (a, p) => { a.Add(p.Key, p.Value); return a; }
            );

            return ApplyTemplate("_post.md", Hash.FromAnonymousObject(new
            {
                content = renderContext,
                page = eo.Property 
            }));
        }


        /// <summary>
        /// Template 파일을 읽어온다.
        /// </summary>
        /// <param name="templateName">Template 명</param>
        /// <returns>Template 을 문자열로 반환한다.</returns>
        private string LoadTemplate(string templateName)
        {
            if (!TemplateExits(templateName)) return null;

            return File.ReadAllText(_templatesPath + templateName);
        }

        /// <summary>
        /// Template 파일들이 존재하는지 알려준다.
        /// </summary>
        /// <returns>파일들이 존재한다면 ture, 그렇지않다면 false를 반환한다.</returns>
        private bool TemplateExits(string templateName)
        {
            return File.Exists(_templatesPath + templateName);
        }
    }
}