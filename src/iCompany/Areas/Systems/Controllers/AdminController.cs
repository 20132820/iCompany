using Gemini.AspNetCore.Data;
using iCompany.Areas.Systems.Models;
using iCompany.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace iCompany.Areas.Systems.Controllers
{
    [Area("Systems")]
    public class AdminController : Controller
    {
        private CompanyDbContext context;
        public AdminController(CompanyDbContext context)
        {
            this.context = context;
        }

        public IActionResult Get()
        {
            var collection = context.Administrator.Query<Administrator>(Request);
            var total = collection.Count();

            return Ok(new
            {
                total = total,
                rows = collection.Order(Request).Paging(Request).Select(a=> new { Id = a.Id, Name = a.Name, Role = a.Role })
            });
        }

        public IActionResult Post([FromForm] Administrator admin)
        {
            if (admin.Name == "初始化管理员")
            {
                throw new Exception("初始化管理员为系统保留名称，不允许使用，请更换");
            }

            admin.Id = admin.Id.ToLower();

            if (context.Administrator.Any(a => a.Id == admin.Id))
            {
                throw new Exception(String.Format("登录用户名为 ‘{0}’ 的管理员已存在，请更换", admin.Id));
            }

            if (context.Administrator.Any(a => a.Name == admin.Name))
            {
                throw new Exception(String.Format("名称为 ‘{0}’ 的管理员已存在，请更换", admin.Name));
            }

            //admin.Password = Administrator.ConvertPassword(admin.Password);
            admin.PasswordChangeTime = DateTime.Now.ToUniversalTime();

            context.Administrator.Add(admin);

            context.SaveChanges();

            return Ok();
        }
    }
}
