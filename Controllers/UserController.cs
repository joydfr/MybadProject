using System.IdentityModel.Tokens.Jwt;
using MvcMyBad.Interfaces;
using System.Security.Claims;
using MvcMyBad.Models;
using MvcMyBad.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using MyBadProjet.Controllers;


namespace MvcMyBad.Controllers
{


    namespace MvcMyBad.Controllers
    {
        public class UserController : Controller
        {
            private readonly UserService _userService;

            public UserController(UserService userService)
            {
                _userService = userService;
            }

            [HttpGet("signup")]
            public IActionResult Create()
            {
                return View(new PostUserDto());
            }

            [HttpPost("signup")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> CreateUserAsync(PostUserDto user)
            {
                if (!ModelState.IsValid)
                {
                    return View("Create", user);
                }

                try
                {
                    var newUser = await _userService.CreateUserAsync(user);
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Une erreur est survenue lors de l'inscription.");
                    return View("Create", user);
                }
            }

            [HttpGet("login")]
            public IActionResult Login()
            {
                return View();
            }

            [HttpPost("login")]
            public async Task<IActionResult> LoginUser(UserLoginDTO user)
            {
                var userLogin = await _userService.LoginUser(user);
                if (userLogin == null)
                {
                    return Unauthorized();
                }

                var token = _userService.GenerateJwtToken(userLogin);
                return RedirectToAction("Index", "Home");
            }
        }
    }
}