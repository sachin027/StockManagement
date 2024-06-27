
﻿using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Net;
using System.Net.Http;
using Sachin_452_APIs.JWTAuthentication;

namespace Sachin_452_APIs.JwtAuthentication
{
    public class JwtAuthenticationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)

        {
            var request = actionContext.Request;

            var authHeader = request.Headers.Authorization;

            if (authHeader == null || authHeader.Scheme != "Bearer" || string.IsNullOrEmpty(authHeader.Parameter))
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Authorization header is missing or invalid.");
                return;
            }

            var token = authHeader.Parameter;
            var userName = Authentication.ValidateToken(token);
            if (userName == null)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Invalid token.");
                return;
            }

            base.OnActionExecuting(actionContext);
        }

    }

    //public class JwtAuthenticationAttribute : FilterAttribute

    //{

    // public static void OnAuthorization(HttpContext context)

    // {

    // var request = context.Request.Headers;

    // var token = request["Authorization"];

    // if (string.IsNullOrEmpty(token))

    // {

    // throw new UnauthorizedAccessException("Token is missing.");

    // }

    // // Token might be prefixed with "Bearer ", so we need to remove it

    // if (token.StartsWith("Bearer "))

    // {

    // token = token.Substring("Bearer ".Length).Trim();

    // }

    // if (string.IsNullOrEmpty(token))

    // {

    // throw new UnauthorizedAccessException("Token is null.");

    // }

    // var userName = Authentication.ValidateToken(token);

    // if (userName == null)

    // {

    // throw new UnauthorizedAccessException("Invalid token.");

    // }

    // }

    //}

}