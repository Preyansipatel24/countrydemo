using CountryDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;

using ClosedXML.Excel;



using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using OfficeOpenXml;
using Microsoft.Data.SqlClient;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Hosting;


namespace CountryDemo.Controllers
{
    public class ContryController : Controller
    {

        private readonly CountryContext _db;
        private readonly IWebHostEnvironment _OhostEnvironment;


        public ContryController(CountryContext db, IWebHostEnvironment OhostEnvironment)
        {
            _db = db;
            _OhostEnvironment = OhostEnvironment;
        }
        List<Country> _Countries = new List<Country>();
        public IActionResult Index(string searching, int pg = 1)
        {
            //for (int i = 0; i <= _Countries.Count; i++)
            //{
            //    _Countries.Add(new Country()
            //    {
            //        Id = i,
            //       CountryName = "Country"+i,
            //    });

            //}


            IEnumerable<Country> objlist = _db.Countries;




            if (searching != null)
            {
                //pageNumber = 1;
                return View(_db.Countries.Where(x => x.CountryName.Contains(searching) || searching == null).ToList());

            }

            //const int pageSize = 10;
            //if (pg < 1)
            //    pg = 1;

            //int recsCount = objlist.Count();
            //var pagination = new Pagination(recsCount, pg, pageSize);


            //int recSkip = (pg - 1) * pageSize;

            //var data = objlist.Skip(recSkip).Take(pagination.PageSize).ToList();
            //this.ViewBag.Pagination = pagination;

           

            return View(objlist);



        }




        [HttpPost]
        public IActionResult Export()
        {
            IEnumerable<Country> objlist = _db.Countries;
            using (var workbook  = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Country");
                var currentRow = 1;

                worksheet.Cell(currentRow,1).Value = "Id";
                worksheet.Cell(currentRow, 2).Value = "CountryName";


               foreach (var Country in objlist)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = Country.Id;
                    worksheet.Cell(currentRow, 2).Value = Country.CountryName;


                }
               using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content =stream.ToArray();
                    return File(
                        content, "appliction/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Country.xlsx"
                        ); 
                }

            }
        }
      

        public IActionResult GetCustomers()
        {
            DataSet ds = new DataSet();
             List<Country> listdata = new List<Country>();   


            return View(listdata);
        }




    //GET-CREATE
    public IActionResult Create()
        {
           
          
            return View();
        }
        //Post-CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Country obj)
        {
            if (ModelState.IsValid)
            {
                _db.Countries.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");

            }

            return View(obj);
        }
        //GET-EDIT
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Countries.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        //Post-Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Country obj)
        {
            if (ModelState.IsValid)
            {
                _db.Countries.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");

            }

            return View(obj);
        }
        //GET-Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Countries.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }


        //Post-Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Countries.Find(id);

            if (obj == null)
            {
                return NotFound();

            }
            _db.Countries.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");




        }


        //public ActionResult PrintStudent(int param)
        //{
        //     List<Country> objlist = _db.Countries.ToList();    

        //    CountryReport rpt = new CountryReport(_OhostEnvironment);
        //    return File(rpt.Report(objlist), "application/pdf");
        //}



    }
}
