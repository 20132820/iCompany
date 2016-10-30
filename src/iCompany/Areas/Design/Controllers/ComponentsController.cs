using Gemini.AspNetCore.Data;
using iCompany.Areas.Design.Models;
using iCompany.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace iCompany.Areas.Design.Controllers
{
    [Area("Design")]
    public class ComponentsController : Controller
    {
        private CompanyDbContext context;
        public ComponentsController(CompanyDbContext context)
        {
            this.context = context;
        }

        public IActionResult Get()
        {
            var collection = context.Components.Query<Components>(Request);
            var total = collection.Count();

            return Ok(new
            {
                total = total,
                rows = collection.Order(Request).Paging(Request)
            });
        }

        public IActionResult Post([FromForm] Components components)
        {
            var obj = context.Components.FirstOrDefault(a => a.Id == components.Id);
            if(obj == null)
            {
                obj = new Components();
                obj.Id = Guid.NewGuid();
                context.Components.Add(obj);
            }
            obj.Name = components.Name;
            obj.No = components.No;

            context.SaveChanges();

            return Ok();
        }

        public IActionResult Delete(Guid id)
        {
            var obj = new Components { Id = id };
            context.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            context.SaveChanges();

            return Ok();
        }
    }
}
