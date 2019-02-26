using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GrabCAD.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : Controller
    {
        [HttpGet]
        public ActionResult Get()
        {
            return Json("default");
        }
    }
}