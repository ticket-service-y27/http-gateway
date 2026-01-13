using Grpc.Core;

namespace HttpGateway.Middleware;

public class ExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (RpcException e)
        {
            string message =
                $"Exception occured while processing request, type = {e.GetType().Name}, message = {e.Message}";

            context.Response.StatusCode = e.StatusCode.MapHttpStatus();
            await context.Response.WriteAsJsonAsync(new { Message = message });
        }
    }
}