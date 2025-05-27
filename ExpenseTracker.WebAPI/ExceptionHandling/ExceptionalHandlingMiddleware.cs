namespace ExpenseTracker.WebAPI.ExceptionHandling
{
    public class ExceptionalHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionalHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }

    public static class ExceptionHandlingMiddlewareExtensions {
        public static IApplicationBuilder UseExceptionalHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionalHandlingMiddleware>();
        }
    }
}
