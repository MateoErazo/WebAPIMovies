using WebAPIMovies.DTOs.Pagination;

namespace WebAPIMovies.DTOs.Movie
{
  public class MovieFilterDTO
  {
    public int Page { get; set; } = 1;
    public int AmountRegistersByPage { get; set; } = 10;
    public PaginationDTO Pagination
    {
      get { return new PaginationDTO() { Page = Page, AmountRegistersByPage = AmountRegistersByPage }; }
    }

    public string Title { get; set; }
    public int GenderId { get; set; }
    public bool IsInCinema { get; set; }
    public bool ComingSoon { get; set; }
  }
}
