using System;
using System.Collections.Generic;
using System.Text;
using CorePorject.Model;

namespace CorePorject.IService.System
{
    public interface IMenusService:IBaseService<Menu>
    {
        public List<Menu> GetMenusList(int RoleID);
    }
}
