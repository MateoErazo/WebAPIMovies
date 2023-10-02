using Microsoft.EntityFrameworkCore;

namespace WebAPIMovies.Helpers
{
  public static class HttpContextExtensions
  {
    public async static Task InsertParametersPagination<T>(
      this HttpContext httpContext,
      IQueryable<T> queryable,
      int amountRegistersByPage)
    {
      double amount = await queryable.CountAsync();
      double pagesAmount = Math.Ceiling(amount / amountRegistersByPage);
      httpContext.Response.Headers.Add("registersAmount", amount.ToString());
      httpContext.Response.Headers.Add("pagesAmount", pagesAmount.ToString());

    }
  }
}
