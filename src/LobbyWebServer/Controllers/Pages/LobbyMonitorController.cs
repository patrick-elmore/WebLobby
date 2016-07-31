using LobbyWebServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LobbyWebServer.Controllers
{
    public class LobbyMonitorController : Controller
    {
		[HttpGet]
		[Route("~/")]
		[Route("LobbyMonitor")]
        public ActionResult LobbyMonitor()
        {
            return View("~/Views/LobbyMonitor.cshtml", new LobbyMonitorModel());
        }
    }
}