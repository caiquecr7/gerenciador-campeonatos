namespace GerenciadorCampeonatos.Domain.Results
{
    public class PagedSeachModel
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 100;
        public string OrderBy { get; set; }
        public bool OrderByAscending { get; set; } = true;
    }
}
