using Dmhy.IService;
using Dmhy.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dmhy.Service
{
    public class DramaService : IDramaService
    {
        private BaseService _baseService = new BaseService();

        public DramaIndexModel[] GetDramaData()
        {
            string url = "https://share.dmhy.org/";

            string html = _baseService.DownloadHtml(url);

            return ToModels(html);
        }

        private DramaIndexModel[] ToModels(string html)
        {
            List<DramaIndexModel> dramaIndexModels = new List<DramaIndexModel>();

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            HtmlNode scriptNode = doc.DocumentNode.SelectSingleNode(".//div[@class=\"main\"]/script[3]");

            string scriptData = scriptNode.InnerHtml.Trim();

            List<string> dataList = new List<string>();


            //数据处理
            int startIndex = 0;
            int endIndex = 0;

            //获取所有星期的动漫数据
            while (true)
            {
                startIndex = scriptData.IndexOf("//星期", endIndex);

                //if (startIndex < 0)
                //{
                //    break;
                //}

                endIndex = scriptData.IndexOf("//星期", startIndex + 7);

                if (endIndex < 0)
                {
                    string endText = scriptData.Substring(startIndex);
                    dataList.Add(endText);
                    break;
                }

                string text = scriptData.Substring(startIndex, endIndex - startIndex);
                dataList.Add(text);

            }

            //遍历所有星期数据
            foreach (string dramaData in dataList)
            {
                //获取数据正则
                MatchCollection matchs = Regex.Matches(dramaData, @".+?push\(\['(?<imgSrc>.+?)','(?<name>.+?)'.+?keyword=(?<keyword>.+?)""");

                if (matchs.Count <= 0)
                {
                    throw new Exception("获取番剧索引数据失败，无匹配数据!");
                }

                //保存当前星期的数据
                List<DramaModel> dramaModels = new List<DramaModel>();

                //获取当前星期匹配到的数据
                foreach (Match match in matchs)
                {
                    //转换成 DramaModel 类
                    dramaModels.Add(new DramaModel
                    {
                        Id = Guid.NewGuid().ToString(),
                        Names = match.Groups["name"].Value,
                        ImgSrc = match.Groups["imgSrc"].Value,
                        KeyWord = match.Groups["keyword"].Value
                    });
                }

                dramaIndexModels.Add(new DramaIndexModel
                {
                    DramaModels = dramaModels.ToArray()
                });
            }


            return dramaIndexModels.ToArray();
        }
    }
}
