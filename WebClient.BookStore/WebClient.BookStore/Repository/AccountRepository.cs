using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebClient.BookStore.Models;
using WebClient.BookStore.Service;

namespace WebClient.BookStore.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserService _userService=null;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IUserService userService,IEmailService emailService,IConfiguration configuration,RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
            _emailService = emailService;
            _configuration = configuration;
            _roleManager = roleManager;
        }
        public async Task<IdentityResult> CreateUserAsync(SignUpUserModel usermodel)
        {
            var user = new ApplicationUser()
            {
                FirstName=usermodel.FirstName,
                LastName=usermodel.LastName,
                DateOfBirth=usermodel.DateOfBirth,
                Email = usermodel.Email,
                UserName = usermodel.Email
            };
            var result = await _userManager.CreateAsync(user, usermodel.Password);
            if (result.Succeeded) {
                await GenerateEmailConfirmationTokenAsync(user);
            }
            return result;
        }
        public async Task GenerateEmailConfirmationTokenAsync(ApplicationUser user) {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            if (!string.IsNullOrEmpty(token))
            {
                await SendEmailConfirmation(user, token);
            }
        }
        public async Task<ApplicationUser> GetUserByEmailAsync(string email) {
            return await _userManager.FindByEmailAsync(email);
        }
        public async Task<SignInResult> PasswordSignInAsync(SignInModel signInModel) {
            //var result= await _signInManager.PasswordSignInAsync(signInModel.Email,signInModel.Password,signInModel.RememberMe,false);
            var result = await _signInManager.PasswordSignInAsync(signInModel.Email, signInModel.Password, signInModel.RememberMe, true);
            return result;
        }
        public async Task SignOutAsyn() {
            await _signInManager.SignOutAsync();
        }
        public async Task<IdentityResult> ChangePasswordAsync(ChangePasswordModel cmodel) {
            var userId = _userService.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            return await _userManager.ChangePasswordAsync(user,cmodel.CurrentPassword,cmodel.NewPassword);
            
        }
        public async Task<IdentityResult> ConfirmEmailAsync(string uid,string token)
        {
            return await _userManager.ConfirmEmailAsync(await _userManager.FindByIdAsync(uid),token);
        }
        private async Task SendEmailConfirmation(ApplicationUser user,string token) {

            string appDomain = _configuration.GetSection("Application:AppDomain").Value;
            string confirmationLink = _configuration.GetSection("Application:EmailConfirmation").Value;
            UserEmailOptions options = new UserEmailOptions()
            {
                ToMails = new List<string>() { user.Email },
                PlaceHolders = new List<KeyValuePair<string, string>>()
            {
            new KeyValuePair<string,string>("{{UserName}}",user.FirstName +" "+user.LastName),
            new KeyValuePair<string,string>("{{Link}}",string.Format(appDomain+confirmationLink,user.Id,token))

            }
            };
            await _emailService.SendEmailForEmailConfirmation(options);

        }
        public async Task GenerateForgotPasswordTokenAsync(ApplicationUser user)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            if (!string.IsNullOrEmpty(token))
            {
                await SendForgotPasswordEmail(user, token);
            }
        }
        private async Task SendForgotPasswordEmail(ApplicationUser user, string token)
        {

            string appDomain = _configuration.GetSection("Application:AppDomain").Value;
            string confirmationLink = _configuration.GetSection("Application:ForgotPassword").Value;
            UserEmailOptions options = new UserEmailOptions()
            {
                ToMails = new List<string>() { user.Email },
                PlaceHolders = new List<KeyValuePair<string, string>>()
            {
            new KeyValuePair<string,string>("{{UserName}}",user.FirstName +" "+user.LastName),
            new KeyValuePair<string,string>("{{Link}}",string.Format(appDomain+confirmationLink,user.Id,token))

            }
            };
            await _emailService.SendEmailForgotPassword(options);
        }
        public async Task<IdentityResult> ResetPasswordAsync(ResetPasswordModel model)
        {
            return await _userManager.ResetPasswordAsync(await _userManager.FindByIdAsync(model.UserId), model.Token, model.NewPassword);
        }
    }
}
