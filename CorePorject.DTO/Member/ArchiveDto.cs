using System;
using System.Collections.Generic;
using System.Text;

namespace CorePorject.DTO.Member
{
    public class ArchiveDto
    {
        public string No { get; set; }
        public string Idcard { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public DateTime Birthday { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Photo { get; set; }
        public int Marital { get; set; }
        public string Education { get; set; }
        public bool State { get; set; }
        public string Native { get; set; }
        public string Occupation { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public double Temperature { get; set; }
        public double Bust { get; set; }
        public double Waist { get; set; }
        public double Hipline { get; set; }
        public int Hr { get; set; }
        public int Ri { get; set; }
        public int Dbp { get; set; }
        public int Sbp { get; set; }
        public double SpO2 { get; set; }
        public double Bmi { get; set; }
        public string Past { get; set; }
        public string Family { get; set; }
        public string Allergy { get; set; }
        public string Medical { get; set; }
        public string Chronic { get; set; }
        public string Remarks { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
