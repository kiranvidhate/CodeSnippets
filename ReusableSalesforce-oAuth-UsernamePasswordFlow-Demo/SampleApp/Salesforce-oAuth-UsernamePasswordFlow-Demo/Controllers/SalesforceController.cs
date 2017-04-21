using Salesforce_oAuth_UsernamePasswordFlow_Demo.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Salesforce_oAuth_UsernamePasswordFlow_Demo.Controllers
{
    public class SalesforceController : Controller
    {
        SalesforceUtility objSalesforceUtility = new SalesforceUtility();
        // GET: Salesforce
        public ActionResult Index()
        {
            string ConnectionReponse = objSalesforceUtility.ConnectToSalesforce();
            return View();

        }

        // GET: Salesforce/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Salesforce/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Salesforce/Create
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

        // GET: Salesforce/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Salesforce/Edit/5
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

        // GET: Salesforce/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Salesforce/Delete/5
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
