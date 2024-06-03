using System;
using System.Collections.Generic;
using System.Text;
using CorePorject.Model;
using CorePorject.IRepository.System;
using Microsoft.EntityFrameworkCore;

namespace CorePorject.Repository.System
{
    public class UsersRepository : BaseRepository<User>, IUsersRepository
    {
        public UsersRepository(HealthyDBContext context) : base(context)
        {
        }
    }
}
