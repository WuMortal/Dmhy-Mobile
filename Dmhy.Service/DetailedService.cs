using Dmhy.IService;
using Dmhy.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dmhy.Service
{
    public class DetailedService : IDetailedService
    {
        private BaseService _baseService = new BaseService();

        public DetailedModel GetDetailed(string Id)
        {
            string url = $"https://share.dmhy.org/topics/view/{Id}.html";

            string html = _baseService.DownloadHtml(url);

            return ToModel(html);
        }

        private DetailedModel ToModel(string html)
        {
            DetailedModel model = new DetailedModel();


            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            HtmlNode mainNodes = doc.DocumentNode.SelectSingleNode("//div[@class='topic-main']");

            if (mainNodes == null)
            {
                return null;
            }
            //标题相关
            HtmlNode tiltleNode = mainNodes.SelectSingleNode("./div[@class]");

            string title = tiltleNode.SelectSingleNode("./h3").InnerText;

            //分类
            HtmlNode categoryNode = tiltleNode.SelectSingleNode("./div/ul/li/span/a[last()]");
            string category = categoryNode.InnerText.Trim();
            string categoryHref = categoryNode.Attributes["href"].Value;
            string categoryId = categoryHref.Substring(categoryHref.LastIndexOf("/")).Replace("/", "");

            string datetime = tiltleNode.SelectSingleNode("./div/ul/li[2]/span").InnerText.Trim();
            string size = tiltleNode.SelectSingleNode("./div/ul/li[4]/span").InnerText.Trim();

            //文章内容
            string content = mainNodes.SelectSingleNode("./div[2]").InnerHtml.Trim().Replace("<strong>簡介:&nbsp;</strong><br>", "");

            //BT链接
            //Dictionary<string, string> btDic = new Dictionary<string, string>();

            List<object> btList = new List<object>();

            var btNodes = mainNodes.SelectNodes("./div[4]/div[@id]//strong");

            foreach (var btNode in btNodes)
            {
                var aNode = btNode.SelectSingleNode("./following-sibling::a");
                string name = aNode.InnerText;
                string href = aNode.Attributes["href"].Value == "#" ? name : aNode.Attributes["href"].Value;
                btList.Add(new { Name = name, Href = href });
                //btDic[name] = href;
            }

            //BT 详细
            List<object> btContentDict = new List<object>();
            var btContentNodes = mainNodes.SelectNodes("./div[4]/div[@id]/div[@class]/ul/li");

            if (btContentNodes.Count() > 0)
            {
                foreach (var btContentNode in btContentNodes)
                {
                    string name = btContentNode.SelectSingleNode("./img").NextSibling.InnerText.Trim();
                    string btSize = btContentNode.SelectSingleNode("./span").InnerText.Trim();
                    btContentDict.Add(new { Name = name, FileSize = btSize });
                    //btContentDict[name] = btSize;
                }

            }

            model = new DetailedModel()
            {
                Id = Guid.NewGuid().ToString(),
                Category = category,
                CategoryId = long.Parse(categoryId),
                DateTime = datetime,
                FileSize = size,
                Title = title,
                Content = content,
                BTDict = btList.ToArray(),
                BTContentDict = btContentDict.ToArray()
            };

            return model;
        }
    }
}
