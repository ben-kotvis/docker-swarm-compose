using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Api.Models;
using Microsoft.Extensions.Caching.Distributed;

namespace Api.Controllers
{
    public class HomeController : Controller
    {

        private readonly IDistributedCache _distributedCache;
        public HomeController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> About()
        {
            string message = await _distributedCache.GetStringAsync("key");
            if (string.IsNullOrEmpty(message))
            {

                message = $"Time is {DateTime.UtcNow.ToLongTimeString()}";
                await _distributedCache.SetStringAsync("key", message);
            }

            ViewData["Message"] = message;

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
