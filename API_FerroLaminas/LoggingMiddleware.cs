namespace API_FerroLaminas
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // print information of requested method in console
            Console.WriteLine($"Request Method: {context.Request.Method}, Path: {context.Request.Path}");

            // called next pipeline 
            await _next(context);

            // Print response of server
            Console.WriteLine($"Response Status Code: {context.Response.StatusCode}");
        }
    }
}
