using System;
using System.Collections.Generic;

#nullable disable

namespace CorePorject.Model
{
    public partial class Authority
    {
        public int Authorityid { get; set; }
        public int RoleId { get; set; }
        public int MenuId { get; set; }
        public int Sort { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }

        public virtual Menu Menu { get; set; }
        public virtual Role Role { get; set; }
    }
}
