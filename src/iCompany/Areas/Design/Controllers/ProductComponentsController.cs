using Gemini.AspNetCore.Data;
using iCompany.Areas.Design.Models;
using iCompany.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace iCompany.Areas.Design.Controllers
{
    [Area("Design")]
    public class ProductComponentsController : Controller
    {
        private CompanyDbContext context;
        public ProductComponentsController(CompanyDbContext context)
        {
            this.context = context;
        }

        public IActionResult Get()
        {
            var collection = context.ProductComponents.Query<ProductComponents>(Request);
            var total = collection.Count();

            return Ok(new
            {
                total = total,
                rows = collection.Order(Request).Paging(Request)
            });
        }

        public IActionResult Post([FromForm] ProductComponents productComponents)
        {
            var obj = context.ProductComponents.FirstOrDefault(a => a.Id == productComponents.Id);
            if(obj == null)
            {
                obj = new ProductComponents();
                obj.Id = Guid.NewGuid();
                context.ProductComponents.Add(obj);
            }
            obj.ProductId = productComponents.ProductId;
            obj.ComponentsId = productComponents.ComponentsId;
            obj.Amount = productComponents.Amount;

            context.SaveChanges();

            return Ok();
        }

        public IActionResult Delete(Guid id)
        {
            var obj = new ProductComponents { Id = id };
            context.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            context.SaveChanges();

            return Ok();
        }
    }
}
