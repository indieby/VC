using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using vc.service;

namespace vc.Filters
{
    public class VacationExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception is VacationException)
            {
                actionExecutedContext.Response =
                    new HttpResponseMessage(HttpStatusCode.Forbidden)
                    {
                        ReasonPhrase = actionExecutedContext.Exception.Message
                    };
            }
        }
    }
}