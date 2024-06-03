using System;
using System.Collections.Generic;
using System.Text;

namespace CorePorject.DTO.System
{
    public class MenuDto
    {

        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string ViewUrl { get; set; }
        public int ParentId { get; set; }
        public string Icon { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }

    }
}
