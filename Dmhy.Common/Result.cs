using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dmhy.Common
{
    public class Result
    {
        /// <summary>
        /// 执行结果
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMsg { get; set; }

        public int Count { get; set; }

        public string Title { get; set; }

        /// <summary>
        /// 执行返回数据
        /// </summary>
        public object Data { get; set; }
    }
}
