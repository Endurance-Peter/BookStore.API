using BookStoreProject.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreProject.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signIn;

        public AccountRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signIn)
        {
            _userManager = userManager;
            _signIn = signIn;
        }
        public async Task<IdentityResult> SignUpAsync(SignUp signUp)
        {
            var user = new ApplicationUser
            {
                FirstName = signUp.FirstName,
                LastName = signUp.LastName,
                Email = signUp.Email,
                UserName = signUp.Email
            };
            return await _userManager.CreateAsync(user, signUp.Password);
        }
        public async Task<string> SignIn(SignIn signIn)
        {
            var result = await _signIn.PasswordSignInAsync(signIn.Email, signIn.Password, false, false);

            if (!result.Succeeded)
            {
                result = null;
            }

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, signIn.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var authSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["Jwt:Secret"]));
            var token = new JwtSecurityToken
            (
                issuer: Configuration["Jwt:ValidIssuer"],
                audience: Configuration["Jwt:ValidAudience"],
                expires: DateTime.Now.AddDays(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256Signature)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public IConfiguration Configuration { get; set; }
    }
}
