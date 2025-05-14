using IK.Application.Layer.Models.DTO_s.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IK.Application.Layer.Services.LoginService
{
    public interface ILoginService
    {
         Task<LoginResult_DTO> LoginAsync(Login_DTO login);

         Task<string> RegisterUserAsync(RegisterUser_DTO user);

         Task<int> GetUserIDAsync(ClaimsPrincipal claim);
    }
}
