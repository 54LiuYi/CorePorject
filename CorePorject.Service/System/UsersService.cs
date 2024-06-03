using System;
using System.Collections.Generic;
using System.Text;
using CorePorject.Model;
using CorePorject.IService.System;
using CorePorject.IRepository.System;

namespace CorePorject.Service.System
{
    public class UsersService : BaseService<User>, IUsersService
    {
        public UsersService(IUsersRepository repository) : base(repository)
        {
        }
    }
}
