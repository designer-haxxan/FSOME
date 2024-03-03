using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.Models
{
    public class ContactExportDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Ignore]
        public int Id { get; set; }
        [Display(Name = "First Name")]
        [Required]
        public string? first_name { get; set; }
        [Display(Name = "Last Name")]
        [Required]
        public string? last_name { get; set; }

        [Display(Name = "Work Email")]
        [Required]
        public string? work_email { get; set; }
        [Display(Name = "Personal Email")]
        [Required]
        public string? personal_email { get; set; }
        [Display(Name = "Company")]
        [Required]
        public string? company { get; set; }
        [Display(Name = "Domain")]
        public string? domain { get; set; }
        [Display(Name = "Job Title")]
        public string? job_title { get; set; }
        [Display(Name = "Job Level")]
        public string? job_level { get; set; }
        [Display(Name = "Job Department")]
        public string? job_department { get; set; }
        [Display(Name = "Work Phone")]
        public string? work_phone { get; set; }
        [Display(Name = "Mobile")]
        public string? mobile { get; set; }
        [Display(Name = "Street1")]
        public string? street1 { get; set; }
        [Display(Name = "City")]
        public string? city { get; set; }
        [Display(Name = "State")]
        public string? state { get; set; }
        [Display(Name = "Zip Code")]
        public string? zip { get; set; }
        [Display(Name = "Country")]
        public string? country { get; set; }
        [Display(Name = "Company Revenue")]
        public string? company_revenue { get; set; }
        [Display(Name = "Company Employee Count")]
        public string? company_employee_count { get; set; }
        [Display(Name = "Linkedin Url")]
        public string? linkedin_url { get; set; }
        [Display(Name = "Company Linkedin Url")]
        [Ignore]
        public string? company_linkedin_url { get; set; }
        [Display(Name = "Naics Code")]
        public string? naics_code { get; set; }
        [Display(Name = "Company Sector")]
        public string? company_sector { get; set; }
        [Display(Name = "Company Industry")]
        public string? company_industry { get; set; }
    }
}
