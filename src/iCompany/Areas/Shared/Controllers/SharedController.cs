using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace iCompany.Areas.Shared.Controllers
{
    [Area("Shared")]
    public class SharedController : Controller
    {
        public IActionResult View(string viewName, bool partial = false)
        {
            if (partial)
            {
                return PartialView(viewName);
            }
            else
            {
                return base.View(viewName);
            }
        }

        public PartialViewResult UploadFile()
        {
            return PartialView();
        }

        public IActionResult Workplace()
        {
            return View();
        }
    }

    public static class SharedExtentions
    {
        public static string View(this IUrlHelper helper, string viewName, string controller, string area, bool partial = false)
        {
            return helper.Action("View", "Shared",
                new
                {
                    area = "Shared",
                    viewName = $"/Areas/{area}/Views/{controller}/{viewName}.cshtml",
                    partial = partial
                });
        }

        public static string Action(this IUrlHelper helper, string action, string controller, string area)
        {
            return helper.Action(action, controller, new { area = area });
        }
    }
}
