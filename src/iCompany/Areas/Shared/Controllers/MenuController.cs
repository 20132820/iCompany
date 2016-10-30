using Gemini.AspNetCore.SiteMap;
using Microsoft.AspNetCore.Mvc;
using System;

namespace iCompany.Areas.Shared.Controllers
{
    [Area("Shared")]
    public class MenuController : Controller
    {
        private SiteMapNode rootSiteMapNode;

        public MenuController(SiteMapNode rootSiteMapNode)
        {
            this.rootSiteMapNode = rootSiteMapNode;
        }

        public IActionResult Index(string currentPath, string startPath = "", int deep = int.MaxValue)
        {
            var startNode = string.IsNullOrWhiteSpace(startPath) ? rootSiteMapNode : rootSiteMapNode.Find(startPath);
            if (startNode == null)
            {
                throw new Exception($"未找到到路径为{startPath}的菜单");
            }

            var sitemap = startNode.Copy(currentPath, this.User, deep);

            return Ok(sitemap.ChildNodes);
        }
    }
}
