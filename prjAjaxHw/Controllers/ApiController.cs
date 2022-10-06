using Microsoft.AspNetCore.Mvc;
using prjAjaxHw.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prjAjaxHw.Controllers
{
    public class ApiController : Controller
    {
        private readonly DemoContext _DMcontext;
        private readonly NorthwindContext _NWcontext;

        public ApiController(DemoContext DMcontext, NorthwindContext NWcontext)  //相依性注入
        {
            _DMcontext = DMcontext;
            _NWcontext = NWcontext;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register(Member member /*string Name, string Email, int Age*/)
        {
            //todo 將資料存入資料庫
            _DMcontext.Members.Add(member);
            _DMcontext.SaveChanges();  //db memberId 設識別

            return Content(member.Name, "text/plain");

        }
        public IActionResult checkExist(string name)
        {
            bool isExist = _DMcontext.Members.Any(m => m.Name == name);
            //if (string.IsNullOrEmpty(Name))  
            //    return Content("不可空白", "text/plain");
            //else if (isExist)
            //    return Content("帳號已存在", "text/plain");
            //else
            //    return Content("", "text/plain");
            return Content(isExist.ToString(), "text/plain");  //要考量回傳資料量

        }

        //讀取所有城市資料
        public IActionResult City()
        {
            var cities = _DMcontext.Addresses.Select(a => a.City).Distinct();
            return Json(cities);

        }
        //依據城市名稱讀取鄉鎮區資料
        public IActionResult Site(string city)
        {
            var sites = _DMcontext.Addresses.Where(a => a.City == city).Select(a => a.SiteId).Distinct();
            return Json(sites);

        }
        //依據城市名稱讀取鄉鎮區資料
        public IActionResult Road(string site)
        {
            var roads = _DMcontext.Addresses.Where(a => a.SiteId == site).Select(a => a.Road).Distinct();
            return Json(roads);

        }
        public IActionResult autoCmpNW(string product)
        {
            var products = _NWcontext.Products.Where(p => p.ProductName.Contains(product)).Select(p => p.ProductName);
            return Json(products);

        }

    }
}
