using System;
using System.Collections.Generic;
using System.Text;
using CorePorject.Model;
using CorePorject.IRepository.System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace CorePorject.Repository.System
{
    public class MenusRepository : BaseRepository<Menu>, IMenusRepository
    {
        public MenusRepository(HealthyDBContext context) : base(context)
        {
        }

        public IQueryable<Menu> GetMenus()
        {
            var menus = _context.Set<Menu>().Include(m=>m.Authorities).AsQueryable();

            return menus;

        }
    }
}
