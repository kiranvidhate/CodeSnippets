using System.Web.Mvc;

namespace ReusableAttributeRouting.Controllers
{
    [RoutePrefix("MyRoute")]
    public class CustomAttributesController : Controller
    {
        [Route("~/")] //Specifies that this is the default action for the entire application. Route: /

        [Route("")] //Specifies that this is the default action for this route prefix. Route: /Users

        //Simple Attribute Routing
        [Route("Simple")]
        public ActionResult Index()
        {
            ViewBag.Message = "This is Simple Attribute Routing Example.";
            return View("Index");
        }

        //Single Parameter Attribute Routing
        [Route("WithId/{id}")]
        public ActionResult ViewWithParameter(int id)
        {
            ViewBag.Message = "This is Attribute Routing with ID:" + id;
            return View("Index");
        }

        //Multiple Parameter Attribute Routing
        //We can also specify if a parameter is optional by using ?:
        [Route("WithMulti/{id}/{name?}")]
        public ActionResult ViewWithMultipleParameter(int id, string name)
        {
            ViewBag.Message = "This is Attribute Routing with ID:" + id + " and Name:" + name;
            return View("Index");
        }

        //Multiple Parameter Attribute Routing
        //We can also specify if a parameter is optional by using ?:
        [Route("WithIntId/{id:int}")]
        public ActionResult ViewWithDataTypeParameter(int id)
        {
            ViewBag.Message = "This is Attribute Routing with Integer ID:" + id;
            return View("Index");
        }
    }
}