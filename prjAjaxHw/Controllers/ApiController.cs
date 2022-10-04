﻿using Microsoft.AspNetCore.Mvc;
using prjAjaxHw.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prjAjaxHw.Controllers
{
    public class ApiController : Controller
    {
        private readonly DemoContext _context;

        public ApiController(DemoContext context)  //相依性注入
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register(Member member /*string Name, string Email, int Age*/)
        {
            //todo 將資料存入資料庫
            _context.Members.Add(member);
            _context.SaveChanges();  //db memberId 設識別

            return Content(member.Name, "text/plain");

        }
        public IActionResult checkExist(string Name)
        {
            bool isExist = _context.Members.Any(e => e.Name == Name);
            if (string.IsNullOrEmpty(Name))
                return Content("不可空白", "text/plain");
            else if (isExist)
                return Content("帳號已存在", "text/plain");
            else
                return Content("", "text/plain");
        }
    }
}
