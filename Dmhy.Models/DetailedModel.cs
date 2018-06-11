using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dmhy.Models
{
    /// <summary>
    /// 帖子详细
    /// </summary>
    public class DetailedModel : PostModel
    {

        /// <summary>
        /// 帖子内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// BT列表
        /// </summary>
        public Object[] BTDict { get; set; } = { };

        public Object[] BTContentDict { get; set; } = {};



    }
}
