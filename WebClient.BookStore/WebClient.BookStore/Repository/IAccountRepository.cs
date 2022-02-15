using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using WebClient.BookStore.Models;

namespace WebClient.BookStore.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> CreateUserAsync(SignUpUserModel usermodel);
        Task<SignInResult> PasswordSignInAsync(SignInModel signInModel);
        Task SignOutAsyn();
        Task<IdentityResult> ChangePasswordAsync(ChangePasswordModel cmodel);
        Task<IdentityResult> ConfirmEmailAsync(string uid, string token);
        Task GenerateEmailConfirmationTokenAsync(ApplicationUser user);
        Task<ApplicationUser> GetUserByEmailAsync(string email);
        Task GenerateForgotPasswordTokenAsync(ApplicationUser user);
        Task<IdentityResult> ResetPasswordAsync(ResetPasswordModel model);
    }
}