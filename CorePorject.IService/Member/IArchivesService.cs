using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using CorePorject.Model;

namespace CorePorject.IService.Member
{
    public interface IArchivesService:IBaseService<Archive>
    {
        /// <summary>
        /// 添加档案
        /// </summary>
        /// <param name="archive"></param>
        /// <returns></returns>
        public int AddArchive(Archive archive);

        /// <summary>
        /// 修改档案
        /// </summary>
        /// <param name="archive"></param>
        /// <returns></returns>
        public int UpdateArchive(Archive archive);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="no">档案编号</param>
        /// <returns></returns>
        public int DeleteArchive(string no);

        /// <summary>
        /// 分页查询档案列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<Archive> GetArchiveList(int pageIndex, int pageSize,out int count, string name);

        
    }
}
