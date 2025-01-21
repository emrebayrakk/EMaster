using EMaster.Domain.Responses;
using System.Text.Json;
using System.Threading.RateLimiting;

namespace EMaster.API.Middlewares
{
    public class RateLimiterMiddleware
    {
        private readonly RequestDelegate _next;

        public RateLimiterMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public void ConfigureRateLimiter(IServiceCollection services)
        {
            services.AddRateLimiter(options =>
            {
                options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpcontext =>
                    RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: httpcontext.Request.Headers.Host.ToString(),
                        factory: partition => new FixedWindowRateLimiterOptions
                        {
                            AutoReplenishment = true,
                            PermitLimit = 10,
                            Window = TimeSpan.FromMilliseconds(300)
                        }
                    ));

                options.OnRejected = async (context, token) =>
                {
                    context.HttpContext.Response.StatusCode = 429;
                    ApiResponse<string> apiResponse = new ApiResponse<string>(
                            false,
                            429,
                            "Error",
                            ""
                        );

                    if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
                    {
                        apiResponse.Data = $"İstek sınır sayısına ulaştınız. {retryAfter.TotalMinutes} dakika sonra tekrar deneyiniz.";
                    }
                    else
                    {
                        apiResponse.Data = "İstek sınırına ulaştınız. Daha sonra tekrar deneyin.";
                    }

                    string jsonResponse = JsonSerializer.Serialize(apiResponse);
                    context.HttpContext.Response.ContentType = "application/json";
                    await context.HttpContext.Response.WriteAsync(jsonResponse, cancellationToken: token);
                };
            });
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);
        }
    }
}
