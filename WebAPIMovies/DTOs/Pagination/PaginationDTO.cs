namespace WebAPIMovies.DTOs.Pagination
{
  public class PaginationDTO
  {
    public int Page { get; set; } = 1;

    private int amountRegistersByPage = 10;
    private readonly int amountMaximumRegistersByPage = 50;

    public int AmountRegistersByPage
    {
      get => amountRegistersByPage;

      set
      {
        amountRegistersByPage =(value > amountMaximumRegistersByPage) ? amountMaximumRegistersByPage : value;
      }
    }
  }
}
