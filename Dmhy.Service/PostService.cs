using Dmhy.IService;
using Dmhy.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;

namespace Dmhy.Service
{
    public class PostService : IPostService
    {
        private BaseService _baseService = new BaseService();

        public PostModel[] GetTopsDataByCategoryId(long categoryId, long pageIndex)
        {
            string url = "https://share.dmhy.org/topics/list/sort_id/" + categoryId + "/page/" + pageIndex;

            string html = _baseService.DownloadHtml(url);

            return ToModels(html);

        }

        public PostModel[] GetTopsDataByKeyWord(string keyWord, long pageIndex)
        {
            string url = "https://share.dmhy.org//topics/list/page/" + pageIndex + $"?keyword={keyWord}";

            string html = _baseService.DownloadHtml(url);

            return ToModels(html);
        }

        public PostModel[] GetTopsDataByPageIndex(long pageIndex)
        {
            string url = "https://share.dmhy.org//topics/list/page/" + pageIndex;

            string html = _baseService.DownloadHtml(url);

            return ToModels(html);
        }

        public PostModel[] GetTopsDataByTeamId(long teamId, long pageIndex)
        {
            string url = $"https://share.dmhy.org/topics/list/team_id/{teamId}/page/{pageIndex}";
            string html = _baseService.DownloadHtml(url);

            return ToModels(html);
        }

        private PostModel[] ToModels(string html)
        {
            List<PostModel> models = new List<PostModel>();



            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            HtmlNodeCollection trNodes = doc.DocumentNode.SelectNodes("//tbody/tr");

            if (trNodes == null)
            {
                return null;
            }

            foreach (var tr in trNodes)
            {
                HtmlNodeCollection tdNodes = tr.SelectNodes(".//td");

                //时间
                string time = tdNodes[0].FirstChild.InnerText.Trim();
                //string datetime = tdNodes[0].SelectSingleNode("./span").InnerText.Trim();

                //分类
                string category = tdNodes[1].SelectSingleNode("./a//font").InnerText.Trim();
                string categoryHref = tdNodes[1].SelectSingleNode("./a").Attributes["href"].Value;
                string categoryId = categoryHref.Substring(categoryHref.LastIndexOf("/")).Replace("/", "");

                //团队
                HtmlNode teamNode = tdNodes[2].SelectSingleNode("./span/a");
                string team = "";
                string teamHref = "";
                string teamId = "";
                if (teamNode != null)
                {
                    team = teamNode.InnerText.Trim();

                    teamHref = teamNode.Attributes["href"].Value.Trim();
                    teamId = teamHref.Substring(teamHref.LastIndexOf("/")).Replace("/", "");
                }

                //番剧相关
                string title = tdNodes[2].SelectSingleNode("./a").InnerText.Trim();
                string htmlId = tdNodes[2].SelectSingleNode("./a").Attributes["href"].Value.Replace(".html", "");
                htmlId = htmlId.Substring(htmlId.LastIndexOf("/") + 1);
                string downloadArrow = tdNodes[3].SelectSingleNode("./a").Attributes["href"].Value;
                string size = tdNodes[4].InnerText;

                models.Add(new PostModel
                {
                    Id = Guid.NewGuid().ToString(),
                    Category = category,
                    CategoryId = long.Parse(categoryId),
                    DateTime = time,
                    DownloadArrow = downloadArrow,
                    FileSize = size,
                    Team = team,
                    TeamId = string.IsNullOrEmpty(teamId) ? null : (long?)long.Parse(teamId),
                    Title = title,
                    HtmlId = htmlId
                });

            }

            return models.ToArray();
        }
    }
}
