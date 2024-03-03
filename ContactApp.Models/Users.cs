using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.Models
{
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage ="Please Enter Email")]
        [EmailAddress(ErrorMessage ="Please Enter Valid Email Address")]
        [Display(Name ="Email")]
        public string? Email { get; set; }
        [Required]
        [Display(Name = "Password")]
        public string? password { get; set; }
        [Required(ErrorMessage = "Please Enter First Name")]
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage ="Please Enter Last Name")]
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }
        [Required(ErrorMessage ="Please Enter Day wise Limit")]
        [Display(Name ="Today Export Limit")]
        public int? ExportTodayLimit { get; set; }
        [Required(ErrorMessage ="Please Enter Per Export Limit")]
        [Display(Name ="Per Export Limit")]
        public int? PerExportLimit { get; set; }

        [Display(Name = "isActive")]
        public bool isActive { get; set; }
        [Display(Name = "isAdmin")]
        public bool isAdmin { get; set; }

    }
}
