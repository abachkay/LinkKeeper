using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace LinkKeeper.API.Filters
{
    public class BadRequestExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            context.Response = new HttpResponseMessage(HttpStatusCode.BadRequest);
        }
    }
}