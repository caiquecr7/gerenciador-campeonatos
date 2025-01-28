using Microsoft.EntityFrameworkCore;

namespace GerenciadorCampeonatos.Domain.Results
{
    public class PagedResult<T>
    {
        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages => (int)Math.Ceiling(TotalItems / (double)PageSize);
        public int TotalItems { get; private set; }
        public List<T> Data { get; private set; } = new List<T>();
        public PagedResult(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex > 1 ? pageIndex : 1;
            PageSize = pageSize;
            TotalItems = count;

            Data.AddRange(items);
        }

        public static async Task<PagedResult<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            pageIndex = NormalizePageIndex(pageIndex);
            pageSize = NormalizePageSize(pageSize);

            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagedResult<T>(items, count, pageIndex, pageSize);
        }

        private static int NormalizePageSize(int pageSize)
        {
            if (pageSize < 1 || pageSize > 100)
                pageSize = 100;
            return pageSize;
        }

        private static int NormalizePageIndex(int pageIndex)
        {
            if (pageIndex < 1)
                pageIndex = 1;
            return pageIndex;
        }
    }
}
