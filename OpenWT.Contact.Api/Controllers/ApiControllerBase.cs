using System;
using Microsoft.AspNetCore.Mvc;
using OpenWT.Contact.Common.Exception;

namespace OpenWT.Contact.Api.Controllers
{
    public class ApiControllerBase : ControllerBase
    {
        protected void CheckSecurity<TArg1>(Func<TArg1, bool> func, TArg1 arg1)
        {
            if (!func(arg1))
            {
                throw new ForbiddenException("Unauthorized");
            }
        }
    }
}