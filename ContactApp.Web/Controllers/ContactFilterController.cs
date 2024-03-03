using ContactApp.DataAccess.Data;
using ContactApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics.Metrics;
using System.Reflection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ContactApp.Web.Controllers
{
    public class ContactFilterController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IMemoryCache _cache;
        public ContactFilterController(ApplicationDbContext db, IMemoryCache cache)
        {
            _db = db;
            _cache = cache;
        }
        public async Task<IActionResult> Index()
        {
            string email = Request.Cookies["userEmail"].ToString() ?? "";
            var result = await _db.tblUsers.Where(x => x.Email == email).FirstOrDefaultAsync();
            if (result == null)
            {
                Response.Cookies.Delete("userAuthenticate");
                return RedirectToAction("Index", "Login");
            }
            else
            {
                List<string> leveldata = new List<string>()
            {
                "Board Members",
                "C-level Executives",
                "Vice Presidents",
                "Directors",
                "Managers",
                "Key Influencer"
            };
                List<string> departmentdata = new List<string>()
            {
                "Finance",
                "HR",
                "IT",
                "Legal",
                "Marketing",
                "Operations",
                "Procurement",
                "R&D",
                "Sales",
                "Cross Functional",
            };
                var industrydata = new RawIndustry
                {
                    heading1 = new List<string>
                {
                    "Crop Farming",
                    "Dairy & Animal Farming",
                    "Forestry & Logging",
                    "Fishing, Hunting & Trapping",
                    "Support Activities for Agriculture and Forestry",
                    "Agriculture, Forestry, Fishing and Hunting"
                },
                    heading2 = new List<string>
                {
                    "Performing Arts Companies",
                    "Sports",
                    "Promoters of Performing Arts, Sports, and Similar Events",
                    "Agents and Managers for Artists, Athletes, Entertainers, and Other Public Figures",
                    "Independent Artists, Writers, and Performers",
                    "Museums, Historical Sites, and Similar Institutions",
                    "Amusement Parks and Arcades",
                    "Casinos & Gambling Services",
                    "Other Amusement and Recreation Industries",
                    "Arts, Entertainment, and Recreation"
                },
                    heading3 = new List<string>
                {
                    "Management of Companies and Enterprises",
                    "Office Administrative Services",
                    "Facilities Support Services",
                    "Human Resources, Staffing & Recruiting",
                    "Other Support Services",
                    "Debt Collections",
                    "Call Centers",
                    "Investigation and Security Services",
                    "Waste Collection, Treatment and Disposal",
                    "Business Support Services"
                },
                    heading4 = new List<string>
                {
                    "Residential Building Construction",
                    "Commercial Building Construction",
                    "Utility System Construction",
                    "Land Subdivision",
                    "Highway, Street, and Bridge Construction",
                    "Other Heavy and Civil Engineering Construction",
                    "Foundation, Structure, and Building Exterior Contractors",
                    "Building Equipment Contractors",
                    "Building Finishing Contractors",
                    "Other Specialty Trade Contractors",
                    "Construction"
                },
                    heading5 = new List<string>
                {
                    "Elementary and Secondary Schools",
                    "Junior Colleges",
                    "Colleges, Universities, and Professional Schools",
                    "Business Schools and Computer and Management Training",
                    "Technical and Trade Schools",
                    "Other Schools and Instruction",
                    "Educational Support Services",
                    "Educational Services"
                },
                    heading6 = new List<string>
                {
                    "Oil and Gas Extraction",
                    "Coal Mining",
                    "Metal Ore Mining",
                    "Nonmetallic Mineral Mining and Quarrying",
                    "Support Activities for Mining",
                    "Electric Power Generation, Transmission and Distribution",
                    "Natural Gas Distribution",
                    "Water, Sewage and Other Systems",
                    "Pipeline Transportation of Crude Oil",
                    "Pipeline Transportation of Natural Gas",
                    "Other Pipeline Transportation",
                    "Energy, Utilities and Oil & Gas"
                },
                    heading7 = new List<string>
                {
                    "Monetary Authorities-Central Bank",
                    "Credit Unions & Depository Credit Intermediation",
                    "Banking",
                    "Nondepository Credit Intermediation",
                    "Credit Card Issuing & Transaction Processing",
                    "Activities Related to Credit Intermediation",
                    "Securities and Commodity Exchanges and Services",
                    "Investment Banking and Securities Dealing",
                    "Other Financial Investment Activities",
                    "Venture Capital & Private Equity",
                    "Insurance Carriers",
                    "Health Insurance Carriers",
                    "Insurance Agencies and Brokers",
                    "Insurance and Employee Benefit Funds",
                    "Other Investment Pools and Funds",
                    "Finance and Insurance"
                },
                    heading8 = new List<string>
                {
                    "Other Public Administration & Support Services",
                    "Federal Government",
                    "State Government, excluding schools and hospitals",
                    "Local Government, excluding schools and hospitals"
                },
                    heading9 = new List<string>
                {
                    "Doctors and Dentists Offices",
                    "Outpatient Surgery Centers and Emergency Rooms",
                    "Medical Testing and Clinical & Diagnostic Laboratories",
                    "Home Health Care & Hospice Services",
                    "Other Health Care Services",
                    "Hospitals and Emergency Rooms",
                    "Skilled Nursing Facilities",
                    "Nursing Homes, Retirement Communities & Residential Care Facilities",
                    "Individual, Family, and Child Care Services",
                    "Community Food and Housing, and Emergency and Other Relief Services",
                    "Vocational Rehabilitation Services",
                    "Health Care and Social Assistance"
                },
                    heading10 = new List<string>
                {
                    "Hotels and Traveler Accommodation",
                    "RV (Recreational Vehicle) Parks and Recreational Camps",
                    "Special Food Services & Caterers",
                    "Bars and Restaurants",
                    "Hospitality and Food Services"
                },
                    heading11 = new List<string>
                {
                    "Computer Hardware and Electronic Product Manufacturing",
                    "Computer Networking & Telecommunications Equipment",
                    "Software Development and Design",
                    "Telecommunications",
                    "Cloud Computing, Data Processing & Storage, Hosting, and Related Services",
                    "Internet Service Providers and Web Search Portals",
                    "Financial Software",
                    "Legal Software",
                    "Engineering Software & Engineering Services",
                    "Multimedia, Games, Graphics Software & Graphic Design Services",
                    "Computer Systems Design and Related Services",
                    "Human Resources Software",
                    "Supply Chain and Logistics Software & Consulting Services",
                    "Enterprise Resource Planning Software",
                    "Information Security & Security Systems Services",
                    "Information Technology"
                },
                    heading12 = new List<string>
                {
                    "Food, Beverage, and Tobacco Manufacturing",
                    "Textile & Apparel Manufacturing",
                    "Wood Product Manufacturing",
                    "Paper Manufacturing",
                    "Printing and Related Support Activities",
                    "Petroleum and Coal Products Manufacturing",
                    "Chemical Manufacturing",
                    "Pharmaceutical and Medicine Manufacturing",
                    "Plastics and Rubber Products Manufacturing",
                    "Nonmetallic Mineral Product Manufacturing",
                    "Primary Metal Manufacturing",
                    "Fabricated Metal Product Manufacturing",
                    "Machinery Manufacturing",
                    "Industrial Automation",
                    "Electrical Equipment, Appliance, and Component Manufacturing",
                    "Automobile & Auto Parts Manufacturing"
                },
                    heading13 = new List<string>
                {
                    "Newspaper, Periodical, Book, and Directory Publishers",
                    "Movies, Music, Television, Radio, and Subscription Broadcasting & Programming",
                    "Social Media & Other Information Services"
                },
                    heading14 = new List<string>
                {
                    "Automotive Repair and Maintenance",
                    "Electronic and Precision Equipment Repair and Maintenance",
                    "Commercial and Industrial Machinery and Equipment (except Automotive and Electronic) Repair and           Maintenance",
                    "Personal and Household Goods Repair and Maintenance",
                    "Personal Care Services",
                    "Death Care & Funeral Services",
                    "Drycleaning and Laundry Services",
                    "Religious Organizations",
                    "Nonprofit and Charitable Organizations",
                    "Social Advocacy Organizations",
                    "Civic and Social Organizations",
                    "Business, Professional, Labor, Political, and Membership Organizations",
                    "Other Services (except Public Administration)"
                },
                    heading15 = new List<string>
                {
                    "Law Firms & Legal Services",
                    "Accounting, Tax Preparation, Bookkeeping, and Payroll Services",
                    "Architectural, Engineering, and Related Services",
                    "Specialized Design Services",
                    "Other Professional, Scientific, and Technical Services",
                    "Biotechnology Research & Development",
                    "Marketing, Advertising, Public Relations, and Related Services",
                    "Veterinary Services",
                    "Travel Arrangement and Reservation Services",
                    "Professional & Consumer Services"
                },
                    heading16 = new List<string> {
                    "Commercial Real Estate",
                    "Real Estate Agents and Brokers",
                    "Activities Related to Real Estate",
                    "Automotive and Automotive Equipment Rental and Leasing",
                    "Consumer Goods Rental",
                    "General Rental Centers",
                    "Commercial and Industrial Machinery and Equipment Rental and Leasing",
                    "Lessors of Nonfinancial Intangible Assets",
                    "Real Estate and Rental & Leasing"
                },
                    heading17 = new List<string>
                {
                    "Automobile Dealers",
                    "Other Motor Vehicle Dealers",
                    "Automotive Parts, Accessories, and Tire Stores",
                    "Furniture and Home Furnishings Stores",
                    "Electronics and Appliance Stores",
                    "Building Materials, Hardware Stores and Garden Equipment and Supplies Dealers",
                    "Grocery, Food, and Beverage Stores",
                    "Health and Personal Care Stores",
                    "Gasoline Stations",
                    "Clothing and Clothing Accessories Stores",
                    "Sporting Goods, Hobby, Book, and Music Stores",
                    "General Merchandise Stores",
                    "Miscellaneous Store Retailers",
                    "Nonstore Retailers",
                    "eCommerce and Mail-Order Retail",
                    "Retail Trade"
                },
                    heading18 = new List<string>
                {
                    "Airlines & Aviation",
                    "Rail Transportation",
                    "Water Transportation",
                    "Trucking & Freight Transportation",
                    "Transit and Ground Passenger Transportation",
                    "Scenic and Sightseeing Transportation",
                    "Support Activities for Transportation",
                    "Transportation Logistics Services",
                    "Postal Service, Couriers & Messengers",
                    "Warehousing and Storage",
                    "Transportation and Warehousing"
                },
                    heading19 = new List<string>
                {
                    "Automobile and Auto Parts & Supplies Merchant Wholesalers",
                    "Furniture and Home Furnishing Merchant Wholesalers",
                    "Lumber and Other Construction Materials Merchant Wholesalers",
                    "Professional and Commercial Equipment and Supplies Merchant Wholesalers",
                    "Metal and Mineral (except Petroleum) Merchant Wholesalers",
                    "Consumer Electronics & Household Appliances Merchant Wholesalers",
                    "Plumbing and HVAC Equipment Merchant Wholesalers",
                    "Machinery, Equipment, and Supplies Merchant Wholesalers",
                    "Miscellaneous Durable Goods Merchant Wholesalers",
                    "Paper and Paper Product Merchant Wholesalers",
                    "Drugs and Druggists' Sundries Merchant Wholesalers",
                    "Apparel, Piece Goods, and Notions Merchant Wholesalers",
                    "Grocery and Related Product Merchant Wholesalers",
                    "Farm Product Raw Material Merchant Wholesalers",
                    "Chemical and Allied Products Merchant Wholesalers",
                    "Petroleum and Petroleum Products Merchant Wholesalers"
                }
                };
                ViewBag.industrylist = industrydata;
                ViewBag.levellist = leveldata;
                ViewBag.departmentlist = departmentdata;
                var totalCount = await _db.tblContacts.CountAsync();
                ViewBag.totalcount = totalCount;
                return View();
            }


        }
        [HttpGet]
        public string getTotalContacs()
        {
            string totalContacts = "0";
            if (!string.IsNullOrWhiteSpace(Request.Cookies["totalcount"]))
                totalContacts = Request.Cookies["totalcount"];
            return totalContacts;
        }

        [HttpPost]
        public async Task<IActionResult> GetAll([FromBody] Customsearchcontact? searchdata)
        {
            var cacheKey = "db";
            List<Contact> result;
            _cache.Remove(cacheKey);
            if (!_cache.TryGetValue(cacheKey, out result))
            {
                result = await FetchFilteredDataFromDatabase(searchdata);
                _cache.Set(cacheKey, result, TimeSpan.FromDays(1));
            }
            if (searchdata != null)
            {
                result = ApplyFiltersOnCachedData(result, searchdata);
            }
            UpdateTotalCountHeader(result);
            return Json(new { data = result });
        }
        private List<Contact> ApplyFiltersOnCachedData(List<Contact> query, Customsearchcontact searchdata)
        {
            if (searchdata != null)
            {
                if (searchdata.department != null && searchdata.department.Count > 0)
                    query = query.Where(x => searchdata.department.Contains(x.job_department)).ToList();

                if (searchdata.industry != null && searchdata.industry.Count > 0)
                    query = query.Where(x => searchdata.industry.Contains(x.company_industry)).ToList();

                if (searchdata.level != null && searchdata.level.Count > 0)
                    query = query.Where(x => searchdata.level.Contains(x.job_level)).ToList();

                if (!string.IsNullOrWhiteSpace(searchdata.company))
                    query = query.Where(x => x.company.Contains(searchdata.company)).ToList();

                if (!string.IsNullOrWhiteSpace(searchdata.keyword))
                    query = query.Where(x => x.company_sector.Contains(searchdata.keyword)).ToList();

                if (!string.IsNullOrWhiteSpace(searchdata.job_title))
                    query = query.Where(x => x.job_title.Contains(searchdata.job_title)).ToList();

                if (!string.IsNullOrWhiteSpace(searchdata.country))
                    query = query.Where(x => x.country.Contains(searchdata.country)).ToList();

                if (!string.IsNullOrWhiteSpace(searchdata.naics_code))
                    query = query.Where(x => x.naics_code.Contains(searchdata.naics_code)).ToList();

                if (!string.IsNullOrWhiteSpace(searchdata.city))
                    query = query.Where(x => x.city.Contains(searchdata.city)).ToList();

                if (!string.IsNullOrWhiteSpace(searchdata.first_name))
                    query = query.Where(x => x.first_name.Contains(searchdata.first_name)).ToList();

                if (!string.IsNullOrWhiteSpace(searchdata.last_name))
                    query = query.Where(x => x.last_name.Contains(searchdata.last_name)).ToList();

                if (!string.IsNullOrWhiteSpace(searchdata.work_email))
                    query = query.Where(x => x.work_email.Contains(searchdata.work_email)).ToList();

                if (!string.IsNullOrWhiteSpace(searchdata.zip))
                    query = query.Where(x => x.zip.Contains(searchdata.zip)).ToList();

                if (!string.IsNullOrWhiteSpace(searchdata.state))
                    query = query.Where(x => x.state.Contains(searchdata.state)).ToList();

                if (searchdata.minrevenue != null)
                    query = query.Where(x => Convert.ToInt32(x.company_revenue) >= searchdata.minrevenue).ToList();

                if (searchdata.maxrevenue != null)
                    query = query.Where(x => Convert.ToInt32(x.company_revenue) <= searchdata.maxrevenue).ToList();

                if (searchdata.minemp != null)
                    query = query.Where(x => Convert.ToInt32(x.company_employee_count) >= searchdata.minemp).ToList();

                if (searchdata.maxemp != null)
                    query = query.Where(x => Convert.ToInt32(x.company_employee_count) <= searchdata.maxemp).ToList();
            }

            return query;
        }


        private void UpdateTotalCountHeader(List<Contact> result)
        {
            var datacount = result.Count();
            var millionNo = 1000000;
            if (datacount >= millionNo)
            {
                Response.Headers.Add("tcount", "1M+");
            }
            else
            {
                string formattedCount = datacount.ToString("#,0");
                Response.Headers.Add("tcount", formattedCount);
            }
        }
        private async Task<List<Contact>> FetchFilteredDataFromDatabase(Customsearchcontact? searchdata)
        {
            var query = _db.tblContacts.AsQueryable();
            if (searchdata != null)
            {
                if (searchdata.department != null && searchdata.department?.Count > 0)
                    query = query.Where(x => searchdata.department.Contains(x.job_department));

                if (searchdata.industry != null && searchdata.industry?.Count > 0)
                    query = query.Where(x => searchdata.industry.Contains(x.company_industry));
                if (searchdata.level != null && searchdata.level?.Count > 0)
                    query = query.Where(x => searchdata.level.Contains(x.job_level));

                if (!string.IsNullOrWhiteSpace(searchdata.company))
                    query = query.Where(x => x.company.Contains(searchdata.company));

                if (!string.IsNullOrWhiteSpace(searchdata.keyword))
                    query = query.Where(x => x.company_sector.Contains(searchdata.keyword));
                if (!string.IsNullOrWhiteSpace(searchdata.job_title))
                    query = query.Where(x => x.job_title.Contains(searchdata.job_title));

                if (!string.IsNullOrWhiteSpace(searchdata.country))
                    query = query.Where(x => x.country.Contains(searchdata.country));
                if (!string.IsNullOrWhiteSpace(searchdata.naics_code))
                    query = query.Where(x => x.naics_code.Contains(searchdata.naics_code));

                if (!string.IsNullOrWhiteSpace(searchdata.city))
                    query = query.Where(x => x.city.Contains(searchdata.city));
                if (!string.IsNullOrWhiteSpace(searchdata.first_name))
                    query = query.Where(x => x.first_name.Contains(searchdata.first_name));
                if (!string.IsNullOrWhiteSpace(searchdata.last_name))
                    query = query.Where(x => x.last_name.Contains(searchdata.last_name));
                if (!string.IsNullOrWhiteSpace(searchdata.work_email))
                    query = query.Where(x => x.work_email.Contains(searchdata.work_email));

                if (!string.IsNullOrWhiteSpace(searchdata.zip))
                    query = query.Where(x => x.zip.Contains(searchdata.zip));

                if (!string.IsNullOrWhiteSpace(searchdata.state))
                    query = query.Where(x => x.state.Contains(searchdata.state));

                if (searchdata.minrevenue != null)
                    query = query.Where(x => Convert.ToInt32(x.company_revenue) >= searchdata.minrevenue);

                if (searchdata.maxrevenue != null)
                    query = query.Where(x => Convert.ToInt32(x.company_revenue) <= searchdata.maxrevenue);

                if (searchdata.minemp != null)
                    query = query.Where(x => Convert.ToInt32(x.company_employee_count) >= searchdata.minemp);
                if (searchdata.maxemp != null)
                    query = query.Where(x => Convert.ToInt32(x.company_employee_count) <= searchdata.maxemp);
            }

            var queryData = await query
                .AsNoTracking()
                .OrderBy(x => x.Id)
                .ToListAsync();
            return queryData;
        }

        [HttpPost]
        public async Task<IActionResult> Export([FromBody] Customsearchcontact? searchdata)
        {
            var query = _db.tblContacts.AsQueryable();

            if (searchdata != null)
            {
                if (searchdata.department != null && searchdata.department?.Count > 0)
                    query = query.Where(x => searchdata.department.Contains(x.job_department));

                if (searchdata.industry != null && searchdata.industry?.Count > 0)
                    query = query.Where(x => searchdata.industry.Contains(x.company_industry));

                if (searchdata.level != null && searchdata.level?.Count > 0)
                    query = query.Where(x => searchdata.level.Contains(x.job_level));

                if (!string.IsNullOrWhiteSpace(searchdata.company))
                    query = query.Where(x => x.company.Contains(searchdata.company));
                if (!string.IsNullOrWhiteSpace(searchdata.keyword))
                    query = query.Where(x => x.company_sector.Contains(searchdata.keyword));

                if (!string.IsNullOrWhiteSpace(searchdata.country))
                    query = query.Where(x => x.country.Contains(searchdata.country));

                if (!string.IsNullOrWhiteSpace(searchdata.city))
                    query = query.Where(x => x.city.Contains(searchdata.city));
                if (!string.IsNullOrWhiteSpace(searchdata.job_title))
                    query = query.Where(x => x.job_title.Contains(searchdata.job_title));

                if (!string.IsNullOrWhiteSpace(searchdata.zip))
                    query = query.Where(x => x.zip.Contains(searchdata.zip));
                if (!string.IsNullOrWhiteSpace(searchdata.first_name))
                    query = query.Where(x => x.first_name.Contains(searchdata.first_name));
                if (!string.IsNullOrWhiteSpace(searchdata.last_name))
                    query = query.Where(x => x.last_name.Contains(searchdata.last_name));
                if (!string.IsNullOrWhiteSpace(searchdata.work_email))
                    query = query.Where(x => x.work_email.Contains(searchdata.work_email));
                if (!string.IsNullOrWhiteSpace(searchdata.state))
                    query = query.Where(x => x.state.Contains(searchdata.state));
                if (!string.IsNullOrWhiteSpace(searchdata.naics_code))
                    query = query.Where(x => x.country.Contains(searchdata.naics_code));

                if (searchdata.minrevenue != null)
                    query = query.Where(x => Convert.ToInt32(x.company_revenue) >= searchdata.minrevenue);

                if (searchdata.maxrevenue != null)
                    query = query.Where(x => Convert.ToInt32(x.company_revenue) <= searchdata.maxrevenue);

                if (searchdata.minemp != null)
                    query = query.Where(x => Convert.ToInt32(x.company_employee_count) >= searchdata.minemp);
                if (searchdata.maxemp != null)
                    query = query.Where(x => Convert.ToInt32(x.company_employee_count) <= searchdata.maxemp);
            }
            int remval = 0;
            var email = Request.Cookies["userEmail"].ToString();
            var key = email.Replace("@", "_").Replace(".", "_") + "perlimit";
            var key2 = email.Replace("@", "_").Replace(".", "_") + "rem";
            var key3 = email.Replace("@", "_").Replace(".", "_") + "todaylimit";
            int exportlimit = Convert.ToInt32(Request.Cookies[key]);
            int todayexportlimit = Convert.ToInt32(Request.Cookies[key3]);
            if (!string.IsNullOrWhiteSpace(Request.Cookies[key2]))
            {
                remval = Convert.ToInt32(Request.Cookies[key2]);
            }
            else
            {
                remval = 0;
            }
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Delete(key2);
            List<ContactExportDto> result = null;
            if (remval <= todayexportlimit)
            {
                result = await query.OrderBy(x => x.Id).Take(exportlimit)
                    .Select(x => new ContactExportDto
                    {
                        first_name = x.first_name,
                        last_name = x.last_name,
                        work_email = x.work_email,
                        personal_email = x.personal_email,
                        company = x.company,
                        domain = x.domain,
                        job_title = x.job_title,
                        job_level = x.job_level,
                        job_department = x.job_department,
                        work_phone = x.work_phone,
                        mobile = x.mobile,
                        street1 = x.street1,
                        city = x.city,
                        state = x.state,
                        zip = x.zip,
                        country = x.country,
                        company_revenue = x.company_revenue,
                        company_employee_count = x.company_employee_count,
                        linkedin_url = x.linkedin_url,
                        naics_code = x.naics_code,
                        company_sector = x.company_sector,
                        company_industry = x.company_industry
                    })
                    .ToListAsync();
                if (remval > 0)
                {
                    remval = remval - exportlimit;
                }
                else
                {
                    remval = todayexportlimit - exportlimit;
                }

                if (remval <= 0)
                {
                    remval = 0;
                }
            }
            else if (remval < exportlimit && remval > 0)
            {
                result = await query.OrderBy(x => x.Id).Take(remval)
                    .Select(x => new ContactExportDto
                    {
                        first_name = x.first_name,
                        last_name = x.last_name,
                        work_email = x.work_email,
                        personal_email = x.personal_email,
                        company = x.company,
                        domain = x.domain,
                        job_title = x.job_title,
                        job_level = x.job_level,
                        job_department = x.job_department,
                        work_phone = x.work_phone,
                        mobile = x.mobile,
                        street1 = x.street1,
                        city = x.city,
                        state = x.state,
                        zip = x.zip,
                        country = x.country,
                        company_revenue = x.company_revenue,
                        company_employee_count = x.company_employee_count,
                        linkedin_url = x.linkedin_url,
                        naics_code = x.naics_code,
                        company_sector = x.company_sector,
                        company_industry = x.company_industry
                    })
                    .ToListAsync();
                remval = 0;
            }
            Response.Headers.Add("remval", remval.ToString());
            Response.Cookies.Append(key2, remval.ToString(), option);
            ViewBag.rem = remval;
            return Json(result);
        }






    }
}
