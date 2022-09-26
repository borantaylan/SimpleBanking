using Banking.Storage.Exceptions;
using System.Net;
using System.Text.Json;

namespace Banking.WebAPI.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IWebHostEnvironment env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IWebHostEnvironment env)
        {
            _logger = logger;
            this.env = env;
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(new EventId(ex.HResult), ex, ex.Message);
                if (env.IsDevelopment())
                {
                    if (ex.GetType() == typeof(EntityNotFoundException))
                    {
                        context.Response.ContentType = "application/json";
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            ErrorCode = ex.GetType().Name,
                            Message = "No entity found for given parameters"
                        }.ToString());
                    }
                    else
                    {
                        context.Response.ContentType = "application/json";
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            ErrorCode = ex.GetType().Name,
                            Message = ex.Message
                        }.ToString());
                    }
                }
                //Hide error if production
                else if (env.IsProduction())
                {
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    await context.Response.WriteAsync(new ErrorDetails()
                    {
                        Message = "Something went wrong!"
                    }.ToString());
                }
            }
        }
    }

    public class ErrorDetails
    {
        public string ErrorCode { get; set; }
        public string Message { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
