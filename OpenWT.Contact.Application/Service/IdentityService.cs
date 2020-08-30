using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OpenWT.Contact.Application.Contract;
using OpenWT.Contact.Application.Data;
using OpenWT.Contact.Common.Exception;
using OpenWT.Contact.Data.Entity;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace OpenWT.Contact.Application.Service
{
    public class IdentityService : IIdentityService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<UserEntity> _userManager;

        public IdentityService(IConfiguration configuration, UserManager<UserEntity> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }
        
        public async Task CreateUser(NewUserDto user)
        {
            var userEntity = await _userManager.FindByEmailAsync(user.Email);
            if (userEntity != null)
            {
                throw new BadParameterException($"A user with the same email already exist.");
            }
            
            userEntity = new UserEntity()  
            {  
                Email = user.Email,  
                SecurityStamp = Guid.NewGuid().ToString(),  
                UserName = user.Username  
            };  
            var result = await _userManager.CreateAsync(userEntity, user.Password);
            if (!result.Succeeded)
            {
                throw new BadParameterException(string.Join(',', result.Errors.Select(error => error.Description)));
            }
        }

        public async Task<JwtDto> Login(LoginDto login)
        {
            var userEntity = await _userManager.FindByEmailAsync(login.Email);
            if (userEntity == null || !(await _userManager.CheckPasswordAsync(userEntity, login.Password)))
            {
                throw new BadParameterException("Authentication failed");
            }
            
            var authClaims = new List<Claim>  
            {  
                new Claim(ClaimTypes.Name, userEntity.UserName),  
                new Claim(ClaimTypes.NameIdentifier, userEntity.Id.ToString()),  
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),  
            }; 
            
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));  
  
            var token = new JwtSecurityToken(  
                issuer: _configuration["JWT:ValidIssuer"],  
                audience: _configuration["JWT:ValidAudience"],  
                expires: DateTime.Now.AddHours(3),  
                claims: authClaims,  
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)  
            );  
            
            return new JwtDto()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),  
                ExpireTo = token.ValidTo 
            };
        }
    }
}