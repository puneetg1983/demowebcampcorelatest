using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using demowebcampcorelatest.Models;
using System.Threading;

namespace demowebcampcorelatest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            throw new ApplicationException("Find this in trace file");
            return View();
        }

        public IActionResult Slow()
        {
            Thread.Sleep(5000);
            return View();
        }

        public IActionResult HighSleep()
        {
            Thread.Sleep(215 * 1000);
            return View();
        }

        public IActionResult NestedExceptionCrash()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(CrashMe));
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        void CrashMe(object obj)
        {
            try
            {
                RaiseEventVoidAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("The CrashMe function crashed with an inner exception", ex);
            }
        }

        private void RaiseEventVoidAsync()
        {
            throw new Exception("Error!");
        }
    }
}
