using DotLiquid;
using StaticBlogGenerator.Models;
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
        public string ApplyTemplate(string templateName, TemplateVariablesModel templateVariables, out Dictionary<string, string>templateHeader)
        {
            TemplateModel templateModel = LoadTemplate(templateName);

            Template template = Template.Parse(templateModel.TemplateBody);

            templateHeader = templateModel.TemplateHeader;

            return template.Render(Hash.FromAnonymousObject(new
            {
                site = templateVariables.Site,
                page = templateVariables.Page,
                content = templateVariables.Content,
                template = templateVariables.Template
            }));
        }

        
        
        /// <summary>
        /// 레이아웃 템플릿을 적용한 페이지를 반환한다.
        /// </summary>
        /// <param name="renderContent">레이아웃 템플릿에 적용할 컨텐츠</param>
        /// <returns></returns>
        public string ApplyLayoutTemplate(string renderContext, TemplateVariablesModel templateVariables)
        {
            Dictionary<string, string> layoutHeader = new Dictionary<string, string>();
            return ApplyTemplate("_layout.html", new TemplateVariablesModel
            {
                Content = renderContext,
                Site = templateVariables.Site,
                Template = templateVariables.Template,
            }, out layoutHeader);
        }


        /// <summary>
        /// Template 파일을 읽어, Header 와 Body를 분리한다.
        /// </summary>
        /// <param name="templateName">Template 명</param>
        /// <returns>Template Model</returns>
        public TemplateModel LoadTemplate(string templateName)
        {
            if (!TemplateExits(templateName)) return null;

            string templateContent = File.ReadAllText(_templatesPath + templateName);

            return new TemplateModel
            {
                TemplateHeader = ParseHeader(templateContent),
                TemplateBody = parseBody(templateContent)
            };
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