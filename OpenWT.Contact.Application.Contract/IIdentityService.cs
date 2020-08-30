using System;
using System.Threading.Tasks;
using OpenWT.Contact.Application.Data;

namespace OpenWT.Contact.Application.Contract
{
    public interface IIdentityService
    {
        Task CreateUser(NewUserDto user);
        Task<JwtDto> Login(LoginDto login);
    }
}