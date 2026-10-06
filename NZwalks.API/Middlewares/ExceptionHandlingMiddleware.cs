using System.Net;

namespace NZwalks.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> logger;
        private readonly RequestDelegate next;

        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger,RequestDelegate next)

        {
            this.logger = logger;
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpcontext)
        {
            try
            {
                await next(httpcontext);
            }

            catch(Exception ex)
            {
                var errorid = Guid.NewGuid();
                //Log this exception
                logger.LogError(ex, $"{errorid} : {ex.Message}");

                //return a custom error response
                httpcontext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpcontext.Response.ContentType = "application/json";

                var error = new
                {
                    Id = errorid,
                    errormessage = "Something went wrong"
                };
                await httpcontext.Response.WriteAsJsonAsync(error);
            }
        }
    }
}
