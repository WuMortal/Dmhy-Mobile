using Dmhy.Common;
using Dmhy.IService;
using Dmhy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Dmhy.WebAPI.Controllers
{
    public class AnimeController : ApiController
    {
        public IPostService PostService { get; set; }

        public IDetailedService DetailedService { get; set; }

        public IDramaService DramaService { get; set; }

        /// <summary>
        /// 获取最新的番剧
        /// </summary>
        /// <param name="pageIndex">需要获取的页码</param>
        /// <returns></returns>
        [HttpPost]
        public Result Topics(long pageIndex)
        {

            var data = PostService.GetTopsDataByPageIndex(pageIndex);

            if (data == null)
            {
                return new Result
                {
                    Status = "not data",
                    ErrorMsg = "没有数据",
                    Title = "最新番剧列表"
                };
            }

            return new Result
            {
                Data = data,
                Count = data.Count(),
                Status = "ok",
                Title = "最新番剧列表"
            };
        }

        /// <summary>
        /// 搜索番剧
        /// </summary>
        /// <param name="keyWord">关键词</param>
        /// <param name="pageIndex">需要获取的页码</param>
        /// <returns></returns>
        [HttpPost]
        public Result Search(string keyWord, long pageIndex)
        {
            var data = PostService.GetTopsDataByKeyWord(keyWord, pageIndex);


            if (data == null)
            {
                return new Result
                {
                    Status = "not data",
                    ErrorMsg = "没有数据",
                    Title = $"搜索番剧“{keyWord}”结果"
                };
            }

            return new Result
            {
                Data = data,
                Count = data.Count(),
                Status = "ok",
                Title = $"搜索番剧“{keyWord}”结果"
            };
        }

        /// <summary>
        /// 根据 字幕组Id 获取番剧
        /// </summary>
        /// <param name="teamId">字幕组Id</param>
        /// <param name="pageIndex">需要获取的页码</param>
        /// <returns></returns>
        [HttpPost]
        public Result TeamPost(long teamId, long pageIndex)
        {
            var data = PostService.GetTopsDataByTeamId(teamId, pageIndex);

            if (data == null)
            {
                return new Result
                {
                    Status = "not data",
                    ErrorMsg = "没有数据"
                };
            }

            return new Result
            {
                Data = data,
                Count = data.Count(),
                Status = "ok",
                Title = $"搜索字幕组“{data[0].Team}”结果"
            };
        }

        /// <summary>
        /// 根据 类别ID 获取番剧
        /// </summary>
        /// <param name="categoryId">类别ID</param>
        /// <param name="pageIndex">需要获取的页码</param>
        /// <returns></returns>
        [HttpPost]
        public Result CategoryPost(long categoryId, long pageIndex)
        {
            var data = PostService.GetTopsDataByCategoryId(categoryId, pageIndex);

            if (data == null)
            {
                return new Result
                {
                    Status = "not data",
                    ErrorMsg = "没有数据"
                };
            }

            return new Result
            {
                Data = data,
                Count = data.Count(),
                Status = "ok",
                Title = $"搜索类别“{data[0].Category}”结果"
            };
        }

        /// <summary>
        /// 获取番剧索引表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Result Schedule()
        {
            var data = DramaService.GetDramaData();

            if (data.Count() <= 0)
            {
                return new Result
                {
                    Status = "not data",
                    ErrorMsg = "没有数据"
                };
            }

            return new Result
            {
                Data = data,
                Count = data.Count(),
                Status = "ok",
                Title = "番剧索引表"
            };
        }

        /// <summary>
        /// 番剧详细
        /// </summary>
        /// <param name="name">番剧地址</param>
        /// <returns></returns>
        public Result Detailed(string name)
        {
            var data = DetailedService.GetDetailed(name);

            if (data == null)
            {
                return new Result
                {
                    Status = "not data",
                    ErrorMsg = "没有数据"
                };
            }

            return new Result
            {
                Data = data,
                Status = "ok",
                Title = data.Title
            };
        }
    }
}
