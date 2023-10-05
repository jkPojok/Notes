using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NotesIdentity.Entities;
using NotesIdentity.Models;

namespace NotesIdentity.Controllers
{
    [Route("[controller]/[action]")]
    public class AuthController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IIdentityServerInteractionService _interactionService;

        public AuthController(
            SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            IIdentityServerInteractionService interactionService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _interactionService = interactionService;
        }

        [HttpGet]
        public IActionResult Register(string returnUrl)
        {
            var viewModel = new RegisterViewModel
            {
                ReturnUrl = returnUrl 
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var user = new AppUser
            {
                UserName = viewModel.Username,
                Email = viewModel.Email
            };

            var result = await _userManager.CreateAsync(user, viewModel.Password!);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);

                if (string.IsNullOrEmpty(viewModel.ReturnUrl))
                {
                    return Redirect("/");
                }
                return Redirect(viewModel.ReturnUrl);
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            var viewModel = new LoginViewModel
            {
                ReturnUrl = returnUrl
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var user = await _userManager.FindByNameAsync(viewModel.Username!) ?? await _userManager.FindByEmailAsync(viewModel.Username!);
            if (user == null)
            {
                return View(viewModel);
            }

            var result = await _signInManager.PasswordSignInAsync(user, viewModel.Password!, false, false);
            if (result.Succeeded)
            {
                if (string.IsNullOrEmpty(viewModel.ReturnUrl))
                {
                    return Redirect("/");
                }
                return Redirect(viewModel.ReturnUrl);
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            await _signInManager.SignOutAsync();
            var logoutRequest = await _interactionService.GetLogoutContextAsync(logoutId);
            return Redirect(logoutRequest.PostLogoutRedirectUri);
        }
    }
}
