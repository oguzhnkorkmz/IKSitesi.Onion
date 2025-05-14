using IK.CoreLayer.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK.CoreLayer.Entities
{
    public class ApplicationUser : IdentityUser<int>
    {
        public int? KurumID { get; set; }
        public Kurum? Kurum { get; set; }

        public int? PersonelID { get; set; }
        public Personel? Personel { get; set; }

    }
}
