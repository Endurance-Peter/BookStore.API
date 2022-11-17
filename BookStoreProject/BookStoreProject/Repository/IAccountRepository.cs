using BookStoreProject.Model;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BookStoreProject.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SignUpAsync(SignUp signUp);
        Task<string> SignIn(SignIn signIn);
    }
}