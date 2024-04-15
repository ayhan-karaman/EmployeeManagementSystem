using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ServerLibrary.Data;
using ServerLibrary.Helpers;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations
{
    public class UserAccountRepository : IUserAccount
    {
        private readonly IOptions<JwtSection> _jwtSection;
        private readonly AppDbContext _appDbContext;

        public UserAccountRepository(IOptions<JwtSection> jwtSection, AppDbContext appDbContext)
        {
            _jwtSection = jwtSection;
            _appDbContext = appDbContext;
        }

        public async Task<GeneralResponse> CreateAsync(Register user)
        {
            if(user == null )
              return  new GeneralResponse(false, "Model is empty!");
            var checkUser = await FindUserByEmail(user.Email!);
            if(checkUser != null)
                return new GeneralResponse(false, "User registered already");
            ApplicationUser applicationUser =  await AddToDatabase(new ApplicationUser
            {
                FullName = user.FullName,
                Email = user.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(user.Password)
            });
            var checkAdminRole = await _appDbContext.SystemRoles.FirstOrDefaultAsync(x => x.Name!.Equals(Constants.Admin));
            if(checkAdminRole == null)
            {
                var createAdminRole = await AddToDatabase(new SystemRole{Name = Constants.Admin});
                await AddToDatabase(new UserRole{RoleId = createAdminRole.Id, UserId = applicationUser.Id});
                return new GeneralResponse(true, "Account created!");
            }
            var checkUserRole = await _appDbContext.SystemRoles.FirstOrDefaultAsync(x => x.Name!.Equals(Constants.User));
            SystemRole response = new();
            if(checkUserRole == null)
            {
                response = await AddToDatabase(new SystemRole{Name = Constants.User});
                await AddToDatabase(new UserRole{RoleId = response.Id, UserId= applicationUser.Id});
            }
            else
                await AddToDatabase(new UserRole{RoleId = checkUserRole.Id, UserId= applicationUser.Id});
            return new GeneralResponse(true, "Account created!");
        }


        public async Task<LoginResponse> SignInAsync(Login user)
        {
            if(user is null)
                return new LoginResponse(false,  "Model is empty!");
            
            var applicationUser = await FindUserByEmail(user.Email);
            if(applicationUser is null) 
                return new LoginResponse(false, "User not found");

            if(!BCrypt.Net.BCrypt.Verify(user.Password, applicationUser.Password))
                return new LoginResponse(false, "Email or Password not valid.");
            
            UserRole? getUserRole = await _appDbContext.UserRoles.FirstOrDefaultAsync(x => x.UserId == applicationUser.Id);
            if(getUserRole == null)
                 return new LoginResponse(false, "User role not found");

            SystemRole? getRoleName = await _appDbContext.SystemRoles.FirstOrDefaultAsync(x => x.Id == getUserRole!.RoleId);
            
            string jwtToken = GenerateToken(applicationUser, getRoleName!.Name);
            string jwtRefreshToken = GenerateRefreshToken();
            return new LoginResponse(true, "Login successfully", jwtToken, jwtRefreshToken);
        }

        private string GenerateToken(ApplicationUser user, string? role)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSection.Value.Key!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FullName!),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Role, role!)
            };

            var token = new JwtSecurityToken
            (
                issuer: _jwtSection.Value.Issuer,
                audience: _jwtSection.Value.Audience,
                claims: userClaims,
                expires:DateTime.Now.AddDays(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public static string GenerateRefreshToken() => Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        
        private async Task<ApplicationUser> FindUserByEmail(string? email)
         =>  await _appDbContext.ApplicationUsers.FirstOrDefaultAsync(x => x.Email!.ToLower().Equals(email!.ToLower()));
       
        private async Task<T> AddToDatabase<T>(T model)
        {
            var result =  _appDbContext.Add(model!);
            await _appDbContext.SaveChangesAsync();
            return (T)result.Entity;
        }
    }
}