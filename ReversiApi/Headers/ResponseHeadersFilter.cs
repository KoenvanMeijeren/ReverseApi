// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the MIT license.  See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc.Filters;

namespace ReversiApi.Headers;

public class ResponseHeadersFilter : ActionFilterAttribute
{
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        base.OnActionExecuted(context);

        context.HttpContext.Response.Headers.Add("X-Content-Type-Options", "nosniff");
        context.HttpContext.Response.Headers.Add("Cache-control", "no-cache, no-store, must-revalidate");
    }
}
