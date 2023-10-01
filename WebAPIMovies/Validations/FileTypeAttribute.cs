using System.ComponentModel.DataAnnotations;

namespace WebAPIMovies.Validations
{
  public class FileTypeAttribute:ValidationAttribute
  {
    private readonly string[] typeFilesAcepted;

    public FileTypeAttribute(string[] typeFilesAcepted) 
    {
      this.typeFilesAcepted = typeFilesAcepted;
    }

    public FileTypeAttribute(GroupFileType groupFileType)
    {
      if (groupFileType == GroupFileType.Picture)
      {
        typeFilesAcepted = new[] { "image/jpeg","image/png","image/gif" };
      }
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

      if (!typeFilesAcepted.Contains(formFile.ContentType))
      {
        return new ValidationResult($"The file type must be any of the next types: {string.Join(", ",typeFilesAcepted)}");
      }

       return ValidationResult.Success;
    }
  }
}
