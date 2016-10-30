using Gemini.AspNetCore.Data;
using iCompany.Areas.Design.Models;
using iCompany.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace iCompany.Areas.Design.Controllers
{
    [Area("Design")]
    public class ProductController : Controller
    {
        private CompanyDbContext context;
        public ProductController(CompanyDbContext context)
        {
            this.context = context;
        }

        public IActionResult Get()
        {
            var collection = context.Product.Query<Product>(Request);
            var total = collection.Count();

            return Ok(new
            {
                total = total,
                rows = collection.Order(Request).Paging(Request)
            });
        }

        public IActionResult Post([FromForm] Product product)
        {
            var obj = context.Product.FirstOrDefault(a => a.Id == product.Id);
            if(obj == null)
            {
                obj = new Product();
                obj.Id = Guid.NewGuid();
                context.Product.Add(obj);
            }
            obj.Name = product.Name;
            obj.No = product.No;

            context.SaveChanges();

            return Ok();
        }

        public IActionResult Delete(Guid id)
        {
            var obj = new Product { Id = id };
            context.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            context.SaveChanges();

            return Ok();
        }
    }
}
