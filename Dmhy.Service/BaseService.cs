using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Dmhy.Service
{
    internal class BaseService
    {
        /// <summary>
        /// 下载数据
        /// </summary>
        /// <param name="url">下载的地址</param>
        /// <returns></returns>
        public string DownloadHtml(string url)
        {
            string html = "";

            //创建 http 连接获取数据
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
           
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                //是否获取到数据
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception("获取数据异常,状态码:" + response.StatusCode);
                }

                //读取数据
                using (Stream inputStream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(inputStream))
                {
                    html = reader.ReadToEnd();
                }
            }

            return html;
        }
    }
}
