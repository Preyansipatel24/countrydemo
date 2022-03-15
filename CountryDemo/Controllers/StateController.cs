using CountryDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CountryDemo.Controllers
{
    public class StateController : Controller
    {
        private readonly CountryContext _ss;
        public StateController(CountryContext ss)
        {
            _ss = ss;
        }

        public IActionResult Index1(string searchby, string searching)
        {
            try
            {
                List<stateviewmodel> Liststates = (from c in _ss.StateData
                                                   join
                                                   d in _ss.Countries on c.CoutryId equals d.Id
                                                   select new { c, d }).ToList().Select(x => new stateviewmodel
                                                   {
                                                       CountryId = x.d.Id,
                                                       Country = x.d.CountryName,
                                                       stateID = x.c.StateId,
                                                       stateName = x.c.StateName,
                                                   }).ToList();
                // IEnumerable<State> objlist = _ss.StateData;

                if (searching != null)
                {
                    var result = Liststates.Where(x => x.stateName.Contains(searching) || x.Country.Contains(searching) || searching == null).ToList();
                    return View(result);
                }




                return View(Liststates);
            }
            catch (Exception ex)
            {
                return View(null);
            }
        }

        //GET-CREATE
        public IActionResult Create()
        {
            var context = _ss.Countries.Select(x => new SelectListItem { Text = x.CountryName, Value = x.Id.ToString() });
            List<SelectList> list = new List<SelectList>().ToList();
            ViewBag.countryList = context;
            return View();
        }
        //Post-CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(State obj)
        {
            if (ModelState.IsValid)
            {
                _ss.StateData.Add(obj);
                _ss.SaveChanges();
                return RedirectToAction("Index1");

            }

            return View();
        }




        //GET-EDIT
        public IActionResult Edit(int? id)
        {
            var context = _ss.Countries.Select(x => new SelectListItem { Text = x.CountryName, Value = x.Id.ToString() });
            List<SelectList> list = new List<SelectList>().ToList();
            ViewBag.countryList = context;
       

            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _ss.StateData.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        //Post-Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(State obj)
        {
            if (ModelState.IsValid)
            {
              _ss.StateData.Update(obj);
                _ss.SaveChanges();
                return RedirectToAction("Index1");

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
            var obj = _ss.StateData.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }


        //Post-Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(State statedata)
        {
            var obj = _ss.StateData.Find(statedata.StateId);

            if (obj == null)
            {
                return NotFound();

            }
            _ss.StateData.Remove(obj);
            _ss.SaveChanges();
            return RedirectToAction("Index1");




        }










    }
}
