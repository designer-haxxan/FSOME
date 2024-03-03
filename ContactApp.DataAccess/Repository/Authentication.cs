using Azure;
using ContactApp.DataAccess.Data;
using ContactApp.DataAccess.IRepository;
using ContactApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.DataAccess.Repository
{
    public class Authentication : IAuthentication
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public Authentication(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseMessage> LoginUser(LoginViewModel data)
        {
            if (!isValidEmailDomain(data.Email) && !isValidEmailFormat(data.Email))
            {
                return new ResponseMessage
                {
                    IsSuccess = false,
                    Message = "Email is Not Valid"
                };
            }
            var result = await _db.tblUsers.Where(x => x.Email == data.Email && x.password == data.password).FirstOrDefaultAsync();
            if (result == null)
            {
                return new ResponseMessage
                {
                    IsSuccess = false,
                    Message = "Your username or password is incorrect"
                };
            }
            if (result != null && result.isActive == false && result.isAdmin==true)
            {
                return new ResponseMessage
                {
                    IsSuccess = false,
                    Message = "Your Email is Not Activated from Admin Side"
                };
            }
            if (data.IsRemember)
            {
                CreateCookies(result.Email, result.isActive == true ? "Active" : "InActive", 7, result.FirstName);

            }
            else
            {
                CreateCookies(result.Email, result.isActive == true ? "Active" : "InActive", 1, result.FirstName);
            }
            if (result != null && result.isActive == false && result.isAdmin == false)
            {
                return new ResponseMessage
                {
                    IsSuccess = true,
                    Message = "signup"
                };
            }
            else
            {
                return new ResponseMessage
                {
                    IsSuccess = true,
                    Message = "Successfully Login"
                };
            }
        }
        public async Task<ResponseMessage> RegisterUser(Users data)
        {
            if (!isValidEmailDomain(data.Email) && !isValidEmailFormat(data.Email))
            {
                return new ResponseMessage
                {
                    IsSuccess = false,
                    Message = "Email is Not Valid"
                };
            }
            var result = await _db.tblUsers.Where(x => x.Email == data.Email).FirstOrDefaultAsync();
            if (result != null)
            {
                return new ResponseMessage
                {
                    IsSuccess = false,
                    Message = "Sorry This Email is already Exist"
                };
            }
            _db.tblUsers.Add(data);
            await _db.SaveChangesAsync();
            return new ResponseMessage
            {
                IsSuccess = true,
                Message = " Welcome aboard! Your free trial account is ready to go"
            };
        }
        private void CreateCookies(string Email, string IsActive, int days, string firstname)
        {
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(7);
            _httpContextAccessor.HttpContext.Response.Cookies.Append("userAuthenticate", IsActive, option);
            _httpContextAccessor.HttpContext.Response.Cookies.Append("userEmail", Email, option);
            _httpContextAccessor.HttpContext.Response.Cookies.Append("userfname", firstname, option);
        }
        private bool isValidEmailFormat(string email)
        {
            try
            {
                var result = new MailAddress(email);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool isValidEmailDomain(string email)
        {
            try
            {
                string domain = email.Split('@')[1];
                var host = Dns.GetHostEntry(domain);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
