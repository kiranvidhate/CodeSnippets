using System;
using System.Web.Mvc;
using CustomActionFilters.Common;

namespace CustomActionFilters.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        [TrackExecutionTime]
        public ViewResult Index()
        {
            ViewBag.results = "Index Action Invoked";
            return View();
        }

        /// <summary>
        /// Welcomes this instance.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception">Exception ocuured</exception>
        [TrackExecutionTime]
        public string Welcome()
        {
            throw new Exception("Exception ocuured");
        }
    }
}