using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebClient.BookStore.Models;
using WebClient.BookStore.Repository;
namespace WebClient.BookStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        [Route("signup")]
        public IActionResult Signup()
        {
            return View();
        }
        [HttpPost("signup")]
        public async Task<IActionResult> signup(SignUpUserModel signupmodel)
        {
            if (ModelState.IsValid)
            {
                var results = await _accountRepository.CreateUserAsync(signupmodel);
                if (!results.Succeeded)
                {
                    foreach (var errorMessage in results.Errors)
                    {
                        ModelState.AddModelError("", errorMessage.Description);
                    }
                    return View(signupmodel);

                }
                ModelState.Clear();
                return RedirectToAction("ConfirmEmail", new { email = signupmodel.Email });
            }
            return View(signupmodel);
        }

        [HttpGet("login")]
        public IActionResult login()
        {
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> login(SignInModel signInModel, string returnURL)
        {

            if (ModelState.IsValid)
            {
                var result = await _accountRepository.PasswordSignInAsync(signInModel);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnURL))
                    {
                        return LocalRedirect(returnURL);
                    }
                    return RedirectToAction("Index", "Home");
                }
                else if (result.IsNotAllowed)
                {
                    ModelState.AddModelError("", "Email Confirmation needed to login!");
                }
                else if (result.IsLockedOut)
                {
                    ModelState.AddModelError("", "Account blocked. Try after sometime!");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Credentials");
                }
            }
            return View(signInModel);

        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await _accountRepository.SignOutAsyn();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("changepassword")]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost("changepassword")]
        public async Task<IActionResult> ChangePasswordAsync(ChangePasswordModel cmodel)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountRepository.ChangePasswordAsync(cmodel);
                if (result.Succeeded)
                {
                    ViewBag.IsSuccess = true;
                    ModelState.Clear();
                    return View();
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }
            return View(cmodel);
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string uid, string token, string email)
        {
            EmailConfirmModel model = new EmailConfirmModel() { Email = email };
            if (!string.IsNullOrEmpty(uid) && !string.IsNullOrEmpty(token))
            {
                token = token.Replace(' ', '+');
                var result = await _accountRepository.ConfirmEmailAsync(uid, token);
                if (result.Succeeded)
                {
                    model.EmailVerified = true;
                }
            }
            return View(model);
        }
        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(EmailConfirmModel model)
        {
            var user = await _accountRepository.GetUserByEmailAsync(model.Email);
            if (user != null)
            {
                if (user.EmailConfirmed)
                {
                    model.EmailVerified = true;
                    return View(model);
                }
                await _accountRepository.GenerateEmailConfirmationTokenAsync(user);
                model.EmailSent = true;
                ModelState.Clear();
            }
            else
            {
                ModelState.AddModelError("", "Somethine went wrong!");
            }
            return View(model);
        }

        [AllowAnonymous, HttpGet("forgot-password")]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [AllowAnonymous, HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {

                var user = await _accountRepository.GetUserByEmailAsync(model.Email);
                if (user != null)
                {
                    await _accountRepository.GenerateForgotPasswordTokenAsync(user);
                }
                ModelState.Clear();
                model.EmailSent = true;
            }
            return View(model);
        }

        [AllowAnonymous, HttpGet("reset-password")]
        public IActionResult ResetPassword(string uid, string token)
        {
            ResetPasswordModel model = new ResetPasswordModel()
            {
                UserId = uid,
                Token = token
            };

            return View(model);
        }

        [AllowAnonymous, HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                model.Token = model.Token.Replace(' ', '+');
                var result = await _accountRepository.ResetPasswordAsync(model);
                if (result.Succeeded)
                {
                    ModelState.Clear();
                    model.IsSuccess = true;
                    return View(model);
                }
                foreach (var error in result.Errors)
                {
                    if (result.Errors.FirstOrDefault().Code == "InvalidToken")
                    {
                        ModelState.AddModelError("", "Token has expired or " +error.Description);
                    }
                    else {
                        ModelState.AddModelError("", error.Description);
                    }
                    
                }
            }
            return View(model);
        }
    }

}
