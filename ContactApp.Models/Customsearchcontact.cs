using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.Models
{
    public class Customsearchcontact
    {
        public string? company { get; set; }
        public string? keyword { get; set; }
        public string? city { get; set; }
        public string? country { get; set; }
        public string? zip { get; set; }
        public string? state { get; set; }
        public string? locationtype { get; set; }
        public int? minrevenue { get; set; }
        public int? maxrevenue { get; set; }
        public int? minemp { get; set; }
        public int? maxemp { get; set; }
        public List<string>? department { get; set; }
        public List<string>? level { get; set; }
        public List<string>? industry { get; set; }
        public string? naics_code { get; set; }
        public string? job_title { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? work_email { get; set; }
    }
}
