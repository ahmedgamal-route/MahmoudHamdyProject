using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Store.Service.Services.CacheServices;
using System.Text;

namespace Store.Web.Helper
{
    public class CachAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timeToLiveInSeconds;

        public CachAttribute(int timeToLiveInSeconds)
        {
            _timeToLiveInSeconds = timeToLiveInSeconds;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var _CacheServices = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
            var cahekey = GenerateCacheKeyFromRequest(context.HttpContext.Request);
            var cacheResponse = await _CacheServices.GetCacheResponseAsync(cahekey);

            if (!string.IsNullOrEmpty(cacheResponse))
            {
                var contentResult = new ContentResult
                {
                    Content = cacheResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };
                context.Result = contentResult;
                return;
            }
            var executedContext = await next();
            if (executedContext.Result is OkObjectResult response)
                await _CacheServices.SetCacheResponseAsync(cahekey, response.Value, TimeSpan.FromSeconds(_timeToLiveInSeconds));
        }
        private string GenerateCacheKeyFromRequest(HttpRequest request)
        {
            StringBuilder cacheKey = new StringBuilder();
            cacheKey.Append($"{request.Path}");

            foreach (var (key, value) in request.Query.OrderBy(x => x.Key))
                cacheKey.Append($"|{key}-{value}");
            return cacheKey.ToString();
        }
    }
}
