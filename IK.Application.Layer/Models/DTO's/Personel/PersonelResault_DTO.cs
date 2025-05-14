using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK.Application.Layer.Models.DTO_s.Personel
{
    public class PersonelResault_DTO
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }


        public static PersonelResault_DTO Success(string message) =>
            new PersonelResault_DTO { IsSuccess = true, Message = message };

        public static PersonelResault_DTO Failure(string message) =>
            new PersonelResault_DTO { IsSuccess = false, Message = message };
    }
}
