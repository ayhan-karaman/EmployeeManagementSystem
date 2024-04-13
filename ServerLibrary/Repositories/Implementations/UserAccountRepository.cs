using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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


        public Task<LoginResponse> SignInAsync(Login login)
        {
            throw new NotImplementedException();
        }
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