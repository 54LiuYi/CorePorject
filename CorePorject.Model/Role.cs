using System;
using System.Collections.Generic;

#nullable disable

namespace CorePorject.Model
{
    public partial class Role
    {
        public Role()
        {
            Authorities = new HashSet<Authority>();
            Users = new HashSet<User>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string Describe { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }

        public virtual ICollection<Authority> Authorities { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
