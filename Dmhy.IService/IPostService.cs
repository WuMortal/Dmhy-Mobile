using Dmhy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dmhy.IService
{
    public interface IPostService : IServiceSupport
    {
        /// <summary>
        /// 获取 pageindex 页的数据
        /// </summary>
        /// <param name="pageIndex">获取数据的页码</param>
        /// <returns></returns>
        PostModel[] GetTopsDataByPageIndex(long pageIndex);

        /// <summary>
        /// 根据 关键词 分页获取数据
        /// </summary>
        /// <param name="keyWord">关键词</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        PostModel[] GetTopsDataByKeyWord(string keyWord,long pageIndex);

        /// <summary>
        /// 根据 类别 ID 获取数据
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        PostModel[] GetTopsDataByCategoryId(long categoryId,long pageIndex);

        /// <summary>
        /// 更据 字幕组ID 分页获取数据
        /// </summary>
        /// <param name="teamId">字幕组ID</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        PostModel[] GetTopsDataByTeamId(long teamId, long pageIndex);

    }
}
