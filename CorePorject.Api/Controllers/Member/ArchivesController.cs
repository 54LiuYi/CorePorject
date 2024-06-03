using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CorePorject.Model;
using CorePorject.DTO.Member;
using CorePorject.IService.Member;
using System.Text;

namespace CorePorject.Api.Controllers.Member
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArchivesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IArchivesService _archivesService;

        public ArchivesController(IMapper mapper, IArchivesService archivesService)
        {
            this._mapper = mapper;
            this._archivesService = archivesService;
        }

        [HttpGet]
        //查询列表
        public ResultPageList<ArchiveDto> GetArchiveList(string name = "", int page = 1, int size = 5)
        {
            name = string.IsNullOrWhiteSpace(name) ? "" : name;
            int count = 0;
            //查询
            var archives = _archivesService.GetArchiveList(page, size, out count, name);
            //Dto类型转换
            var list = _mapper.Map<List<ArchiveDto>>(archives);
            ResultPageList<ArchiveDto> reslist = new ResultPageList<ArchiveDto>()
            {
                state = 1,
                code = "200",
                msg = "ok",
                page = page,
                size = size,
                count = count,
                data = list
            };
            return reslist;
        }

        [HttpPost]
        //添加
        public ResultHandle Post(ArchiveDto archiveDto)
        {
            //Dto类型转换
            var archive = _mapper.Map<Archive>(archiveDto);
            //添加
            int re = _archivesService.AddArchive(archive);
            ResultHandle result = new ResultHandle()
            {
                state = re > 0 ? 1 : 0,
                code = re > 0 ? "200" : "",
                msg = re > 0 ? "添加成功!" : "添加失败!"
            };
            return result;
        }

        [HttpPut]
        //修改
        public ResultHandle Put(ArchiveDto archiveDto)
        {
            //Dto类型转换
            var archive = _mapper.Map<Archive>(archiveDto);
            //修改
            int re = _archivesService.UpdateArchive(archive);
            ResultHandle result = new ResultHandle()
            {
                state = re > 0 ? 1 : 0,
                code = re > 0 ? "200" : "",
                msg = re > 0 ? "修改成功!" : "修改失败!"
            };
            return result;
        }

        [HttpDelete("{id}")]
        //删除
        public ResultHandle Delete(string id)
        {
            //删除
            int re = _archivesService.DeleteArchive(id);
            ResultHandle result = new ResultHandle()
            {
                state = re > 0 ? 1 : 0,
                code = re > 0 ? "200" : "",
                msg = re > 0 ? "删除成功!" : "删除失败!"
            };
            return result;
        }
    }
}
