﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Azure.Search;
using Search_as_a_service_azure.App_Code;


namespace Search_as_a_service_azure.Controllers
{
    public class HotelsController : Controller
    {
        azuresearchservice service;
       
        public HotelsController()
        {
            service = new azuresearchservice();
        }

        // GET: Hotels
        public ActionResult Index()
        {
           
            return View( service.allHotels());
        }

        public PartialViewResult GetSearchRecord(string ls)
        {
            return PartialView("_FTSearch", service.SearchDocuments(service.getIndexClient(), searchText: "fancy wifi"));
        }

        // GET: Hotels/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Hotels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Hotels/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Hotels/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Hotels/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Hotels/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Hotels/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
