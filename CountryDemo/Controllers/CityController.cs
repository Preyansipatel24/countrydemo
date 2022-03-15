using CountryDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountryDemo.Controllers
{
    public class CityController : Controller
    {
        private readonly CountryContext _cc;
        public CityController(CountryContext cc)
        {
            _cc = cc;
        }


        public IActionResult Index2(string searchby ,string searching)
        {
            try
            {

                List<CityViewModel> cityList = (from c in _cc.Cities
                                                join
                                                   s in _cc.StateData on c.StateId equals s.StateId                                              
                                                join
                                                d in _cc.Countries on c.CoutryId equals d.Id
                                                select new { c, s, d }).ToList().Select(x => new CityViewModel
                                                {
                                                   CountryId = x.c.CoutryId,
                                                    Country = x.d.CountryName,
                                                   stateID = x.c.StateId,
                                                    stateName = x.s.StateName,
                                                   CityId = x.c.CityId,
                                                    CityName = x.c.CityName
                                                }).ToList();
                // IEnumerable<State> objlist = _ss.StateData;

                if (searching != null)
                {
                    var result = cityList.Where(x => x.CityName.Contains(searching) || x.Country.Contains(searching) || x.stateName.Contains(searching) || searching == null).ToList();
                    return View(result);
                }

                return View(cityList);
            }
            catch (Exception ex)
            {
                return View(null);
            }
        }



        //[HttpGet]
        //public async Task<IActionResult> Index2(string citySearch)
        //{
        //    ViewData["GetCityDetails"] = citySearch;
        //    var countryquery = from x in _cc.Cities select x;
        //    if (!String.IsNullOrEmpty(citySearch))
        //    {
        //        countryquery = countryquery.Where(c => c.CityName.Contains(citySearch));
        //    }
        //    return View(await countryquery.AsNoTracking().ToListAsync());
        //}

        //GET-CREATE
        public IActionResult Create()

        {


            var context = _cc.Countries.Select(x => new SelectListItem { Text = x.CountryName, Value = x.Id.ToString() });
            List<SelectList> list = new List<SelectList>().ToList();
            ViewBag.countryList = context;

            var cont = _cc.StateData.Select(x => new SelectListItem { Text = x.StateName, Value = x.StateId.ToString() });
            List<SelectList> list1 = new List<SelectList>().ToList();
            ViewBag.statelist = cont;
            return View();
        }
        //Post-CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(City obj)
        {
            if (ModelState.IsValid)
            {
                _cc.Cities.Add(obj);
                _cc.SaveChanges();
                return RedirectToAction("Index2");

            }

            return View();
        }



        //GET-EDIT
        public IActionResult Edit(int? id)
        {
            var context = _cc.Countries.Select(x => new SelectListItem { Text = x.CountryName, Value = x.Id.ToString() });
            List<SelectList> list = new List<SelectList>().ToList();
            ViewBag.countryList = context;

            var cont = _cc.StateData.Select(x => new SelectListItem { Text = x.StateName, Value = x.StateId.ToString() });
            List<SelectList> list1 = new List<SelectList>().ToList();
            ViewBag.statelist = cont;

            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _cc.Cities.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
       
        
        //Post-Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(City obj)
        {
            if (ModelState.IsValid)
            {
                _cc.Cities.Update(obj);
                _cc.SaveChanges();
                return RedirectToAction("Index2");

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
            var obj =_cc.Cities.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }


        //Post-Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(City cities)
        {
            var obj = _cc.Cities.Find(cities.CityId);

            if (obj == null)
            {
                return NotFound();

            }
            _cc.Cities.Remove(obj);
            _cc.SaveChanges();
            return RedirectToAction("Index2");




        }




    }
}
