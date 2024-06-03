using System;
using System.Collections.Generic;

#nullable disable

namespace CorePorject.Model
{
    public partial class Menu
    {
        public Menu()
        {
            Authorities = new HashSet<Authority>();
        }

        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string ViewUrl { get; set; }
        public int ParentId { get; set; }
        public string Icon { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }

        public virtual ICollection<Authority> Authorities { get; set; }
    }
}
