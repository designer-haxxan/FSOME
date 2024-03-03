using ContactApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.DataAccess.IRepository
{
    public interface IAuthentication
    {
        Task<ResponseMessage> LoginUser(LoginViewModel data);
        Task<ResponseMessage> RegisterUser(Users data);
    }
}
