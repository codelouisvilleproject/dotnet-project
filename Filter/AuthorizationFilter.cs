﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using dotnet_project.Models;
using System.Linq;
using System.Threading.Tasks;
using dotnetproject.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_project.Models
{
    public class AuthorizeUserFilter : IAuthorizationFilter
    {

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            String headerToken = context.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ","");
            ApplicationDbContext dbContext = context.HttpContext.RequestServices.GetService(typeof(ApplicationDbContext)) as ApplicationDbContext;
                       
            if (dbContext.AuthTokens.Any(x => x.Token == headerToken))
            {
                context.Result = new AcceptedResult();
            }
            else
            {
                context.Result = new UnauthorizedResult();
            }

        }
    }

    public class AuthorizationUserAttribute : TypeFilterAttribute
    {
        public AuthorizationUserAttribute() : base(typeof(AuthorizeUserFilter))
        {

        }
    }
}