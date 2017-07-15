using System;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace CustomActionFilters.Common
{
    public class TrackExecutionTime : ActionFilterAttribute, IExceptionFilter
    {
        /// <summary>
        /// Called when [action executing].
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string message = "\n" + filterContext.ActionDescriptor.ControllerDescriptor.ControllerName +
                " -> " + filterContext.ActionDescriptor.ActionName + " -> OnActionExecuting \t- " +
                DateTime.Now.ToString() + "\n";
            LogExecutionTime(message);
        }

        /// <summary>
        /// Called when [action executed].
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string message = "\n" + filterContext.ActionDescriptor.ControllerDescriptor.ControllerName +
                " -> " + filterContext.ActionDescriptor.ActionName + " -> OnActionExecuted \t- " +
                DateTime.Now.ToString() + "\n";
            LogExecutionTime(message);
        }

        /// <summary>
        /// Called when [result executing].
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            string message = filterContext.RouteData.Values["controller"].ToString() +
                " -> " + filterContext.RouteData.Values["action"].ToString() +
                " -> OnResultExecuting \t- " + DateTime.Now.ToString() + "\n";
            LogExecutionTime(message);
        }

        /// <summary>
        /// Called when [result executed].
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            string message = filterContext.RouteData.Values["controller"].ToString() +
                " -> " + filterContext.RouteData.Values["action"].ToString() +
                " -> OnResultExecuted \t- " + DateTime.Now.ToString() + "\n";
            LogExecutionTime(message);
            LogExecutionTime("---------------------------------------------------------\n");
        }

        /// <summary>
        /// Called when [exception].
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public void OnException(ExceptionContext filterContext)
        {
            string message = filterContext.RouteData.Values["controller"].ToString() + " -> " +
               filterContext.RouteData.Values["action"].ToString() + " -> " +
               filterContext.Exception.Message + " \t- " + DateTime.Now.ToString() + "\n";
            LogExecutionTime(message);
            LogExecutionTime("---------------------------------------------------------\n");
        }

        /// <summary>
        /// Logs the execution time.
        /// </summary>
        /// <param name="message">The message.</param>
        private void LogExecutionTime(string message)
        {
            File.AppendAllText(HttpContext.Current.Server.MapPath("~/Data/Data.txt"), message);
        }
    }
}