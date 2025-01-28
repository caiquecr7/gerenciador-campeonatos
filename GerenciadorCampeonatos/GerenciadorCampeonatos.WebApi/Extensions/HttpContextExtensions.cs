using GerenciadorCampeonatos.Domain.Results;

namespace GerenciadorCampeonatos.WebApi.Extensions;

public static class HttpContextExtensions
{
    public static void AddPagedResultHeaders<T>(this HttpResponse httpResponse, PagedResult<T> result)
    {
        httpResponse.Headers.TryAdd("X-Page", result.PageIndex.ToString());
        httpResponse.Headers.TryAdd("X-Page-Size", result.PageSize.ToString());
        httpResponse.Headers.TryAdd("X-Total", result.TotalItems.ToString());
        httpResponse.Headers.TryAdd("X-Total-Pages", result.TotalPages.ToString());
    }
}
