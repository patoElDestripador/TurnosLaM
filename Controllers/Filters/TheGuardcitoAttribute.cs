using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
namespace TurnosLaM.Filters
{
    public class TheGuardcitoAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var TheSupport = context.HttpContext.Session.GetString("UserId");
            
                if (!String.IsNullOrEmpty(TheSupport))
                {
                    base.OnActionExecuting(context);
                }
                else
                {
                    context.Result = new RedirectResult("Index");
                }
        }
    }
}

