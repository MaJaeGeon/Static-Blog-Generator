using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace StaticBlogGenerator
{
    /// <summary>
    /// 모든 Manager 클래스에서 사용될 메서드를 모아놓은 클래스
    /// </summary>
    public class Manager
    {
        /// <summary>
        /// Markdown 파일에서 Header 부분을 가져온다.
        /// </summary>
        /// <param name="text">markdown 파일 내용</param>
        /// <returns>header 의 key 와 value</returns>
        public Dictionary<string, string> ParseHeader(string text)
        {
            var match = Regex.Match(text, "(\\+{3})([\\s|\\S]+?)\\1").Groups[2].Value;

            if (string.IsNullOrEmpty(match)) return null;

            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (Match item in Regex.Matches(match, "[^\r\n]+"))
            {
                var keyAndValue = Regex.Match(item.Value, "(.+?):(.+)");
                if (keyAndValue.Success)
                {
                    string key = Regex.Replace(keyAndValue.Groups[1].Value, "/\\s/g", "").TrimStart().TrimEnd();
                    string value = Regex.Replace(keyAndValue.Groups[2].Value, "/['\"]/g", "").TrimStart().TrimEnd();

                    result.Add(key, value);
                }
            }

            return result;
        }

        /// <summary>
        /// Markdown 파일에서 Body 부분을 가져온다.
        /// </summary>
        /// <param name="text">markdown 파일 내용</param>
        /// <returns>Body</returns>
        public string parseBody(string text)
        {
            return Regex.Replace(text, "(\\+{3})([\\s|\\S]+?)\\1", "");
        }
    }
}
