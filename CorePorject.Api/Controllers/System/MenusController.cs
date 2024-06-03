using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CorePorject.Model;
using CorePorject.DTO.System;
using CorePorject.IService.System;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

namespace CorePorject.Api.Controllers.System
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MenusController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMenusService _menusService;

        public MenusController(IMapper mapper, IMenusService menusService)
        {
            this._mapper = mapper;
            this._menusService = menusService;
        }

        [HttpGet]
        public ResultData<List<MenuDto>> GetMenus() {

            var role = HttpContext.User.FindFirst("RoleID");
            if (role ==null)
            {
                return null;
            }
            int RoleID = int.Parse(role.Value);
            var menus = _menusService.GetMenusList(RoleID);
            var list = _mapper.Map<List<MenuDto>>(menus);

            ResultData<List<MenuDto>> reslist = new ResultData<List<MenuDto>>()
            {
                state = 1,
                code = "200",
                msg = "ok",
                data = list
            };
            return reslist;
        }
    }
}
