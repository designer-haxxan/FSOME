using ContactApp.DataAccess.Data;
using ContactApp.DataAccess.IRepository;
using ContactApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.DataAccess.Repository
{
    public class Contact : IContact
    {
        private readonly ApplicationDbContext _db;
        public Contact(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<ResponseMessage> Delete(int id)
        {
            try
            {
                var result = await _db.tblContacts.Where(x => x.Id == id).FirstOrDefaultAsync();
                if (result != null)
                {
                    _db.tblContacts.Remove(result);
                    await _db.SaveChangesAsync();
                    return new ResponseMessage
                    {
                        IsSuccess = true,
                        Message = "Successfully Deleted From the Database"
                    };
                }
            }
            catch (Exception ex)
            {

                return new ResponseMessage
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }


            return new ResponseMessage
            {
                IsSuccess = false,
                Message = "Not found"
            };
        }

        public async Task<Models.Contact> Get(int id)
        {
            try
            {
                var UsersData = await _db.tblContacts.Where(x => x.Id == id).FirstOrDefaultAsync();
                return UsersData;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Task<IEnumerable<Users>> GetAll(CustomSearchUsers? searchData)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseMessage> Save(Models.Contact users)
        {
            try
            {
                _db.tblContacts.Add(users);
                await _db.SaveChangesAsync();
                return new ResponseMessage
                {
                    IsSuccess = true,
                    Message = "Record Successfully Saved"
                };
            }
            catch (Exception ex)
            {
                return new ResponseMessage
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ResponseMessage> SaveRange(IEnumerable<Models.Contact> users)
        {
            try
            {
                _db.tblContacts.AddRange(users);
                await _db.SaveChangesAsync();
                return new ResponseMessage
                {
                    IsSuccess = true,
                    Message = "Record Successfully Uploaded"
                };
            }
            catch (Exception ex)
            {
                return new ResponseMessage
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }


        public async Task<ResponseMessage> Update(Models.Contact users)
        {
            try
            {
                _db.Attach(users);
                _db.Entry(users).State = EntityState.Modified;

                await _db.SaveChangesAsync();

                return new ResponseMessage
                {
                    IsSuccess = true,
                    Message = "Record Successfully Updated"
                };
            }
            catch (Exception ex)
            {
                return new ResponseMessage
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }
}
