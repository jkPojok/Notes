using IdentityServer4.Extensions;
using IdentityServer4.Services;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotesContracts;
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

        private readonly IPublishEndpoint _publishEndpoint;

        public AuthController(
            SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            IIdentityServerInteractionService interactionService,
            IPublishEndpoint publishEndpoint)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _interactionService = interactionService;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
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
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.Action("ConfirmEmail",
                    "Auth",
                    new { userId = user.Id, code = code },
                    protocol: HttpContext.Request.Scheme);

                ElasticEmailClient.Api.ApiKey = "14D38BF18177D2A6C2DBD2E75D2A161B3BFFEDEAE396D906F330F129262FF1AA9EF9CAF8BF925B8582A2D588C1C14565";
                string html = "<!DOCTYPE html>\r\n<html lang=\"en\" xmlns=\"http://www.w3.org/1999/xhtml\" xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:o=\"urn:schemas-microsoft-com:office:office\">\r\n\r\n<head>\r\n  <meta charset=\"utf-8\">\r\n  <meta name=\"viewport\" content=\"width=device-width\">\r\n  <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">\r\n  <meta name=\"x-apple-disable-message-reformatting\">\r\n  <meta name=\"format-detection\" content=\"telephone=no,address=no,email=no,date=no,url=no\">\r\n  <meta name=\"color-scheme\" content=\"light\">\r\n  <meta name=\"supported-color-schemes\" content=\"light\">\r\n  <title></title>\r\n\r\n  <!--[if !mso]><!-->\r\n  <style type=\"text/css\">\r\n    @import url(https://fonts.googleapis.com/css2?family=Open+Sans:wght@300;400;600&display=swap);\r\n  </style>\r\n  <link href=\"https://fonts.googleapis.com/css?family=Open+Sans:400,400i,600,600i,700,700i &subset=cyrillic,latin-ext\" data-name=\"open_sans\" rel=\"stylesheet\" type=\"text/css\">\r\n\r\n  <style>\r\n    :root {\r\n      color-scheme: light;\r\n      supported-color-schemes: light;\r\n    }\r\n\r\n    html,\r\n    body {\r\n      margin: 0 auto !important;\r\n      padding: 0 !important;\r\n      height: 100% !important;\r\n      width: 100% !important;\r\n    }\r\n\r\n    * {\r\n      -ms-text-size-adjust: 100%;\r\n      -webkit-text-size-adjust: 100%;\r\n    }\r\n\r\n    div[style*=\"margin: 16px 0\"] {\r\n      margin: 0 !important;\r\n    }\r\n\r\n    #MessageViewBody,\r\n    #MessageWebViewDiv {\r\n      width: 100% !important;\r\n    }\r\n\r\n    table,\r\n    td {\r\n      mso-table-lspace: 0pt !important;\r\n      mso-table-rspace: 0pt !important;\r\n    }\r\n\r\n    th {\r\n      font-weight: normal;\r\n    }\r\n\r\n    table {\r\n      border-spacing: 0 !important;\r\n      border-collapse: collapse !important;\r\n      table-layout: fixed !important;\r\n      margin: 0 auto !important;\r\n    }\r\n\r\n    a {\r\n      text-decoration: none;\r\n    }\r\n\r\n    @media only screen and (min-device-width: 320px) and (max-device-width: 374px) {\r\n      u~div .email-container {\r\n        min-width: 320px !important;\r\n      }\r\n    }\r\n\r\n    @media only screen and (min-device-width: 375px) and (max-device-width: 413px) {\r\n      u~div .email-container {\r\n        min-width: 375px !important;\r\n      }\r\n    }\r\n\r\n    @media only screen and (min-device-width: 414px) {\r\n      u~div .email-container {\r\n        min-width: 414px !important;\r\n      }\r\n    }\r\n  </style>\r\n\r\n  <style>\r\n    .button-td,\r\n    .button-a {\r\n      transition: all 100ms ease-in;\r\n    }\r\n\r\n    .button-td-primary:hover,\r\n    .button-a-primary:hover {\r\n      background: #5582ff !important;\r\n      border-color: #5582ff !important;\r\n    }\r\n\r\n    .email-container p.column-header {\r\n      padding-top: 30px;\r\n    }\r\n\r\n    @media screen and (max-width: 600px) {\r\n\r\n      .email-container {\r\n        width: 100% !important;\r\n        margin: auto !important;\r\n      }\r\n\r\n      .stack-column,\r\n      .stack-column-center {\r\n        display: block !important;\r\n        width: 100% !important;\r\n        max-width: 100% !important;\r\n        direction: ltr !important;\r\n      }\r\n\r\n      .stack-column-center {\r\n        text-align: center !important;\r\n      }\r\n\r\n      .center-on-narrow {\r\n        text-align: center !important;\r\n        display: block !important;\r\n        margin-left: auto !important;\r\n        margin-right: auto !important;\r\n        float: none !important;\r\n      }\r\n\r\n      table.center-on-narrow {\r\n        display: inline-block !important;\r\n      }\r\n\r\n      .email-container p {\r\n        font-size: 15px !important;\r\n      }\r\n\r\n      .email-container p.header-text {\r\n        font-size: 32px !important;\r\n      }\r\n\r\n      .email-container p.column-header {\r\n        font-size: 19px !important;\r\n        padding-top: 32px;\r\n      }\r\n    }\r\n  </style>\r\n\r\n</head>\r\n\r\n<body width=\"100%\" style=\"margin: 0; padding: 0 !important; mso-line-height-rule: exactly; background-color: #F5F6F8;\">\r\n  <center role=\"article\" aria-roledescription=\"email\" lang=\"en\" style=\"width: 100%; background-color: #F5F6F8;\">\r\n    <table align=\"center\" role=\"presentation\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\" width=\"640\" style=\"margin: auto;\" class=\"email-container\">\r\n      <tr>\r\n        <td style=\"padding: 48px 32px 20px; text-align: center; background-color: #ffffff;\">\r\n          <p class=\"header-text\" style=\"height: auto; margin: 15px 0; background: #ffffff; font-family: Open Sans; text-align: center; font-size: 32px; line-height: 34px; color: #000000; background-color: #ffffff;\">\r\n            Подтверждение электронной почты\r\n          </p>\r\n          <p style=\"height: auto; margin: 28px 0 15px; background: #ffffff; text-align: center; font-family: Open Sans; font-size: 15px; line-height: 27px; color: #5F5F5F; background-color: #ffffff;\">\r\n           " + $"Перейдите по ссылке для подтверждения регистрации: {callbackUrl}" + "\r\n          </p>\r\n        </td>\r\n      </tr>\r\n    </table>\r\n  </center>\r\n</body>\r\n\r\n</html>";
                await ElasticEmailClient.Api.Email.SendAsync(
                    "Email Verification",
                    "no-reply@mynotesx.ru",
                    "MyNotesX", msgTo: new string[] { user.Email! },
                    bodyHtml: html);

                await _publishEndpoint.Publish(new UserCreated { Id = Guid.Parse(user.Id), Email = user.Email, Username = user.UserName });

                return View("EmailConfirmationPage");
            }

            return View(viewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.ConfirmEmailAsync(user, code);

            if (result.Succeeded)
            {
                return RedirectToAction("Login");
            }

            return RedirectToAction("Register");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
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
                ModelState.AddModelError("Username", "Пользователь с таким именем не найден");
                return View(viewModel);
            }

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                ModelState.AddModelError("Username", "Ваша почта не подтверждена");
                return View(viewModel);
            }

            var result = await _signInManager.PasswordSignInAsync(user, viewModel.Password!, false, false);

            if (result.Succeeded)
            {
                if (string.IsNullOrEmpty(viewModel.ReturnUrl))
                {
                    return Redirect("http://localhost:3000");
                }
                return Redirect(viewModel.ReturnUrl);
            }

            if (!await _userManager.CheckPasswordAsync(user, viewModel.Password!))
            {
                ModelState.AddModelError("Password", "Не верный пароль");
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

        [HttpGet]
        public async Task<IActionResult> PasswordChanging()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PasswordChanging(PasswordChangingViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == User.GetSubjectId());
            var result = await _userManager.ChangePasswordAsync(user, viewModel.CurrentPassword!, viewModel.NewPassword!);
            if (result.Succeeded)
            {
                return Redirect("http://localhost:3000");
            }

            return View(viewModel);
        }
    }
}
