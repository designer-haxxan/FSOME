using ContactApp.DataAccess.IRepository;
using ContactApp.DataAccess.Repository;
using ContactApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.IO;
using CsvHelper;

namespace ContactApp.Web.Controllers
{
    public class ContractController : Controller
    {
        private readonly IContact _contact;
        public ContractController(IContact contact)
        {
            _contact = contact;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(Models.Contact contactData)
        {
            if (ModelState.IsValid)
            {
                var result = await _contact.Save(contactData);
                if (result.IsSuccess)
                {
                    TempData["success"] = "Successfully Saved Record";
                    ModelState.Clear();
                }
                else
                {
                    TempData["error"] = result.Message;
                }
            }
            return View();
        }

        public IActionResult Upload()
        {
            return View();
        }
        [HttpPost]
        [RequestSizeLimit(100_000_000)]
        public async Task<IActionResult> Upload(IFormFile fileInput)
        {
            if (fileInput == null || fileInput.Length == 0)
            {
                return BadRequest("File not selected or empty.");
            }

            try
            {
                var contacts = new List<Models.Contact>();

                using (var streamReader = new StreamReader(fileInput.OpenReadStream()))
                {
                    using (var csvReader = new CsvHelper.CsvReader(streamReader, CultureInfo.InvariantCulture)) // Fully qualify CsvReader
                    {

                        csvReader.Read(); // Skip the header row
                        csvReader.ReadHeader(); // Use header row for mapping

                        while (csvReader.Read())
                        {
                            var contact = csvReader.GetRecord<Models.Contact>();
                            contacts.Add(contact);
                        }
                    }
                }

                var result = await _contact.SaveRange(contacts);

                if (result.IsSuccess)
                {
                    return Ok(new { success = true, message = result.Message });
                }

                return Ok(new { success = false, message = result.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpPost]
        //public async Task<IActionResult> Upload(IFormFile fileInput)
        //{
        //    if (fileInput == null || fileInput.Length == 0)
        //    {
        //        return BadRequest("File not selected or empty.");
        //    }

        //    try
        //    {
        //        var contacts = new List<Models.Contact>();

        //        using (var reader = new StreamReader(fileInput.OpenReadStream()))
        //        {
        //            // Skip the first line (header)
        //            await reader.ReadLineAsync();

        //            while (!reader.EndOfStream)
        //            {
        //                var line = await reader.ReadLineAsync();
        //                var values = line.Split(',');

        //                var contact = new Models.Contact
        //                {
        //                    first_name = values[1],
        //                    last_name = values[2],
        //                    work_email = values[3],
        //                    personal_email = values[4],
        //                    company = values[5],
        //                    domain = values[6],
        //                    job_title  = values[7],
        //                    job_level = values[8],
        //                    job_department = values[9],
        //                    work_phone = values[10],
        //                    work_mobile = values[11],
        //                    work_dbn = values[12],
        //                    mobile = values[13],
        //                    alt_mobile = values[14],
        //                    branch_phone = values[15],
        //                    hq_phone = values[16],
        //                    street1 = values[17],
        //                    street2 = values[18],
        //                    city = values[19],
        //                    state = values[20],
        //                    zip = values[21],
        //                    country = values[22],
        //                    location_type = values[23],
        //                    company_revenue = values[24],
        //                    company_employee_count = values[25],
        //                    linkedin_url = values[26],
        //                    twitter_url = values[27],
        //                    company_linkedin_url = values[28],
        //                    naics_code = values[29],
        //                    sic_code = values[30],
        //                    company_sector= values[31],
        //                    company_industry= values[32],
        //                    keywords = values[33]

        //                };

        //                contacts.Add(contact);
        //            }
        //        }

        //        var result = await _contact.SaveRange(contacts);

        //        if (result.IsSuccess)
        //        {
        //            return Ok(new { success = true, message = result.Message });
        //        }

        //        return Ok(new { success = false, message = result.Message });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}




    }
}
