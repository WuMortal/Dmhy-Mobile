using Dmhy.Models;
using Dmhy.Service;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dmhy.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            PostService postService = new PostService();

            //PostModel[] models = postService.GetTopsDataByPageIndex(1);
            //PostModel[] models = postService.GetTopsDataByKeyWord("东京", 1);
            //PostModel[] models = postService.GetTopsDataByCategoryId(2, 2);
            //PostModel[] models = postService.GetTopsDataByTeamId(710, 2);

            //DetailedService detailedService = new DetailedService();

            //DetailedModel model = detailedService.GetDetailed("492079_87_1080P");

            DramaService dramaService = new DramaService();
            dramaService.GetDramaData();

            Console.ReadKey();
        }

        #region 测试
        static void Main2(string[] args)
        {
            string content = File.ReadAllText(@"E:\ProgramStudy\ASP.NET\Web API\Dmhy\数据.html");
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(content);

            HtmlNodeCollection trNodes = doc.DocumentNode.SelectNodes("//tbody/tr");

            foreach (var tr in trNodes)
            {
                HtmlNodeCollection tdNodes = tr.SelectNodes(".//td");

                //时间
                string time = tdNodes[0].FirstChild.InnerText.Trim();
                //string datetime = tdNodes[0].SelectSingleNode("./span").InnerText.Trim();

                //分类
                string category = tdNodes[1].SelectSingleNode("./a/font").InnerText.Trim();
                string categoryHref = tdNodes[1].SelectSingleNode("./a").Attributes["href"].Value;
                string categoryId = categoryHref.Substring(categoryHref.LastIndexOf("/")).Replace("/", "");

                //团队
                HtmlNode teamNode = tdNodes[2].SelectSingleNode("./span/a");
                if (teamNode != null)
                {
                    string team = teamNode.InnerText.Trim();

                    string teamHref = teamNode.Attributes["href"].Value.Trim();
                    string teamId = teamHref.Substring(teamHref.LastIndexOf("/")).Replace("/", "");
                }

                //番剧相关
                string title = tdNodes[2].SelectSingleNode("./a").InnerText.Trim();
                string downloadArrow = tdNodes[3].SelectSingleNode("./a").Attributes["href"].Value;
                string size = tdNodes[4].InnerText;

            }
        }
        #endregion
    }
}
