using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.Models
{
    public class LoginViewModel
    {
        [Required]
        public string? Email { get; set;}
        [Required]
        public string? password { get; set;}
        public bool IsRemember { get; set;}
    }
}
