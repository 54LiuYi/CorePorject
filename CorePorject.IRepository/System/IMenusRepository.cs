using System;
using System.Collections.Generic;
using System.Text;
using CorePorject.Model;
using System.Linq;

namespace CorePorject.IRepository.System
{
    public interface IMenusRepository:IBaseRepository<Menu>
    {
        public IQueryable<Menu> GetMenus();
    }
}
