using System;
using System.Collections.Generic;
using System.Text;
using CorePorject.Model;
using CorePorject.IRepository.System;
using CorePorject.IService.System;
using System.Linq;

namespace CorePorject.Service.System
{
    public class MenusService : BaseService<Menu>, IMenusService
    {
        private readonly IMenusRepository repository;

        public MenusService(IMenusRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        public List<Menu> GetMenusList(int RoleID)
        {
            var menus = repository.GetMenus();
            var list = from m in menus
                       from a in m.Authorities
                       where a.RoleId == RoleID
                       select m;
            return list.ToList();
        }
    }
}
