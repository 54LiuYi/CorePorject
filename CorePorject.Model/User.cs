using System;
using System.Collections.Generic;

#nullable disable

namespace CorePorject.Model
{
    public partial class User
    {
        public int UserId { get; set; }
        public string LoginName { get; set; }
        public string LoginPwd { get; set; }
        public string UserName { get; set; }
        public string Img { get; set; }
        public DateTime Birthday { get; set; }
        public string Sex { get; set; }
        public int RoleId { get; set; }
        public int Frequency { get; set; }
        public int State { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }

        public virtual Role Role { get; set; }
    }
}
