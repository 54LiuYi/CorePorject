using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using CorePorject.Model;
using CorePorject.IRepository.Member;
using CorePorject.IService.Member;
using CorePorject.IRepository;
using System.Globalization;

namespace CorePorject.Service.Member
{
    public class ArchivesService : BaseService<Archive>, IArchivesService
    {
        public ArchivesService(IArchivesRepository repository) : base(repository)
        {
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="archive"></param>
        /// <returns></returns>
        public int AddArchive(Archive archive)
        {
            MD5 md5 = MD5.Create();
            archive.Password = BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(archive.Password))).Replace("-", null);

            //处理生日
            IFormatProvider ifp = new CultureInfo("zh-CN", true);
            DateTime birthday = DateTime.ParseExact(archive.Idcard.Substring(6, 8), "yyyyMMdd", ifp);
            archive.Birthday = birthday;

            //根据现有编号生成新编号
            var NODate = DateTime.Now.ToString("yyyyMMdd");
            //判断当前日期下是否有会员档案
            var data = _repository.GetModels(a => a.No.StartsWith(NODate)).OrderByDescending(a => a.No).FirstOrDefault();
            if (data != null)
            {
                var number = int.Parse(data.No.Substring(data.No.Length - 4)) +1;
                archive.No = string.Format("{0}{1:0000}", NODate, number);
            }
            else
            {
                archive.No = NODate + "0001";
            }


            archive.CreateTime = DateTime.Now;
            archive.UpdateTime = DateTime.Now;

            base.Add(archive);
            return base.SaveChanges();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="archive"></param>
        /// <returns></returns>
        public int UpdateArchive(Archive archive)
        {
            MD5 md5 = MD5.Create();
            archive.Password = BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(archive.Password))).Replace("-", null);

            //处理生日
            IFormatProvider ifp = new CultureInfo("zh-CN", true);
            DateTime birthday = DateTime.ParseExact(archive.Idcard.Substring(6, 8), "yyyyMMdd", ifp);
            archive.Birthday = birthday;

            archive.UpdateTime = DateTime.Now;

            base.Update(archive);
            return base.SaveChanges();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="no">档案编号</param>
        /// <returns></returns>
        public int DeleteArchive(string no)
        {
            var archive = _repository.GetModels(a => a.No == no).FirstOrDefault();
            _repository.Delete(archive);
            return _repository.SaveChanges();
        }

       
        /// <summary>
        /// 分页查询档案列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<Archive> GetArchiveList(int pageIndex, int pageSize, out int count, string name)
        {
            var archives = base.GetModelsByPageList<string>(pageIndex, pageSize, true, a => a.No,
               a => a.No.Contains(name) || a.Idcard.Contains(name) || a.Name.Contains(name), out count);

            return archives;
        }

    }
}
