using ContactApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.DataAccess.IRepository
{
    public interface IUsersEntry
    {
        Task<ResponseMessage> Save(Users users);
        Task<ResponseMessage> Update(Users users);

        Task<IEnumerable<Users>> GetAll(CustomSearchUsers? searchData);
        Task<Users> Get(int id);
        Task<ResponseMessage> Delete(int id);
    }
}
