using IK.CoreLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK.Application.Layer.Models.DTO_s.Login
{
    public class LoginResult_DTO
    {
        public int UserId { get; set; }
        public int? KurumID { get; set; }
        public string? KurumAdi { get; set; }
        public bool IsAdmin { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        public DateTime TokenExpiration { get; set; }
    }

}
