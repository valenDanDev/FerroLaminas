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
            // Aquí puedes imprimir información sobre la solicitud HTTP entrante
            Console.WriteLine($"Request Method: {context.Request.Method}, Path: {context.Request.Path}");

            // Llama al siguiente middleware en el pipeline
            await _next(context);

            // Aquí puedes imprimir información sobre la respuesta HTTP saliente
            Console.WriteLine($"Response Status Code: {context.Response.StatusCode}");
        }
    }
}
