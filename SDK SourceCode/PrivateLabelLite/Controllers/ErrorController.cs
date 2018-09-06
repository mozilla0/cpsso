using PrivateLabelLite.Models;
using System.Web;
using System.Web.Mvc;

namespace PrivateLabelLite.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        [HandleError]
        public ActionResult DefaultError()
        {
            ErrorModel model = new ErrorModel()
            {
                Message = "Error has been occurred."
            };
            return View(model);
        }

        [HandleError]
        public ActionResult NotFound(string message)
        {
            if (Request.IsAjaxRequest())
            {
                throw new HttpException(404, "Not found");
            }
            Response.StatusCode = 404;
            ErrorModel model = new ErrorModel()
            {
                Message = message
            };
            return View(model);
        }

        [HandleError]
        public ActionResult ServerError(string message)
        {
            Response.StatusCode = 500;
            ErrorModel model = new ErrorModel()
            {
                Message = message
            };
            return View(model);
        }
        [HandleError]
        public ActionResult StreamOneCodeError(string message)
        {
            Response.StatusCode = 10;
            ErrorModel model = new ErrorModel()
            {
                Message = message
            };
            return View(model);
        }
    }
}