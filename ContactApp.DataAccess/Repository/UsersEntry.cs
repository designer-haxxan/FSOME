using Azure;
using ContactApp.DataAccess.Data;
using ContactApp.DataAccess.IRepository;
using ContactApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.DataAccess.Repository
{
    public class UsersEntry : IUsersEntry
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UsersEntry(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ResponseMessage> Delete(int id)
        {
            try
            {
                var result = await _db.tblUsers.Where(x => x.Id == id).FirstOrDefaultAsync();
                if (result != null)
                {
                    _db.tblUsers.Remove(result);
                    await _db.SaveChangesAsync();
                    _httpContextAccessor.HttpContext.Response.Cookies.Delete("userAuthenticate");
                    _httpContextAccessor.HttpContext.Response.Cookies.Delete("userEmail");
                    _httpContextAccessor.HttpContext.Response.Cookies.Delete("userfname");

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

        public async Task<Users> Get(int id)
        {
            try
            {
                var UsersData = await _db.tblUsers.Where(x => x.Id == id).FirstOrDefaultAsync();
                return UsersData;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Users>> GetAll(CustomSearchUsers? searchData)
        {
            var result = _db.tblUsers.AsQueryable();

            if (searchData != null)
            {
                if (!string.IsNullOrWhiteSpace(searchData.Email))
                    result = result.Where(x => x.Email == searchData.Email);

                if (!string.IsNullOrWhiteSpace(searchData.FirstName))
                    result = result.Where(x => x.FirstName == searchData.FirstName);

                if (!string.IsNullOrWhiteSpace(searchData.LastName))
                    result = result.Where(x => x.LastName == searchData.LastName);

                if (searchData.isAdmin)
                    result = result.Where(x => x.isAdmin == searchData.isAdmin);

                if (searchData.isActive)
                    result = result.Where(x => x.isActive == searchData.isActive);
            }

            var users = await result.OrderBy(x => x.FirstName).ToListAsync();
            return users;
        }


        public async Task<ResponseMessage> Save(Users users)
        {
            try
            {
                var result = await _db.tblUsers.Where(x => x.Email == users.Email).FirstOrDefaultAsync();
                if(result != null)
                {
                    return new ResponseMessage
                    {
                        IsSuccess = false,
                        Message = "This Email is Already Exist"
                    };
                }
                _db.tblUsers.Add(users);
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

        public async Task<ResponseMessage> Update(Users users)
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
