using ContactApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.DataAccess.IRepository
{
    public interface IContact
    {
        Task<ResponseMessage> Save(Contact users);
        Task<ResponseMessage> SaveRange(IEnumerable<Contact> users);
        Task<ResponseMessage> Update(Contact users);

        Task<IEnumerable<Users>> GetAll(CustomSearchUsers? searchData);
        Task<Contact> Get(int id);
        Task<ResponseMessage> Delete(int id);
    }
}
