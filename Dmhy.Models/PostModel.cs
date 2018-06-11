using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dmhy.Models
{
    /// <summary>
    /// 贴子 Model
    /// </summary>
    public class PostModel : BaseModel
    {
        /// <summary>
        /// 发布日期
        /// </summary>
        public string DateTime { get; set; }

        /// <summary>
        /// 类别名称
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 类别ID
        /// </summary>
        public long CategoryId { get; set; }

        /// <summary>
        /// 团队名称
        /// </summary>
        public string Team { get; set; }

        /// <summary>
        /// 团队ID
        /// </summary>
        public long? TeamId { get; set; }

        /// <summary>
        /// 番剧标题
        /// </summary>
        public string Title { get; set; }

        public string HtmlId { get; set; }

        /// <summary>
        /// 种子下载地址
        /// </summary>
        public string DownloadArrow { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public string FileSize { get; set; }

    }
}
