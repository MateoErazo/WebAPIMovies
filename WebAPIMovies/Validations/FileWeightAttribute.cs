using System.ComponentModel.DataAnnotations;

namespace WebAPIMovies.Validations
{
  public class FileWeightAttribute:ValidationAttribute
  {
    private readonly int maximumWeightInMegaBytes;

    public FileWeightAttribute(int maximumWeightInMegaBytes) 
    {
      this.maximumWeightInMegaBytes = maximumWeightInMegaBytes;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
      if (value == null)
      {
        return ValidationResult.Success;
      }

      IFormFile formFile = value as IFormFile;

      if (formFile is null)
      {
        return ValidationResult.Success;
      }

      if (formFile.Length > maximumWeightInMegaBytes * 1024 * 1024)
      {
        return new ValidationResult($"The weight of the file cannot be biggest of {maximumWeightInMegaBytes}mb.");
      }

      return ValidationResult.Success;
    }
  }
}
