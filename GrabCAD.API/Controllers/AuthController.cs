using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GrabCAD.API.Authentication;
using GrabCAD.API.Models;
using GrabCAD.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GrabCAD.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [AllowAnonymous]
        [HttpPost("token")]
        public string Authenticate([FromQuery] [Required] string username) => _authenticationService.Authenticate(username);
    }
}
