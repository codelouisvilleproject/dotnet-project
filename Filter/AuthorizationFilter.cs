using Microsoft.AspNetCore.Authorization;
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
            String headerToken = context.HttpContext.Request.Headers["Authorization"];
            ApplicationDbContext dbToken = context.HttpContext.RequestServices.GetService(typeof(ApplicationDbContext)) as ApplicationDbContext;
            //Need to get rid of Bearer Type  

            if (dbToken.AuthTokens.Any(token => token.Token == headerToken))
            {
                context.Result = new AcceptedResult();
            }
            else
            {
                context.Result = new UnauthorizedResult();
            }

            //ApplicationDbContext dbContext = context.HttpContext.RequestServices.GetService(typeof(ApplicationDbContext)) as ApplicationDbContext; 
            //dbContext.Activities.Add(new Activity(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), 60)); 
        }
    }

    public class AuthorizationUserAttribute : TypeFilterAttribute
    {
        public AuthorizationUserAttribute() : base(typeof(AuthorizeUserFilter))
        {

        }
    }
}