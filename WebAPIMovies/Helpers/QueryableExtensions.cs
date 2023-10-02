using WebAPIMovies.DTOs.Pagination;

namespace WebAPIMovies.Helpers
{
  public static class QueryableExtensions
  {
    public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> queryable, PaginationDTO paginationDTO)
    {
      return queryable
        .Skip((paginationDTO.Page - 1) * paginationDTO.AmountRegistersByPage)
        .Take(paginationDTO.AmountRegistersByPage);
    }
  }
}
