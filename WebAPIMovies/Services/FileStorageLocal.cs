namespace WebAPIMovies.Services
{
  public class FileStorageLocal : IFileStorage
  {
    private readonly IWebHostEnvironment env;
    private readonly IHttpContextAccessor httpContextAccessor;

    public FileStorageLocal(
      IWebHostEnvironment env,
      IHttpContextAccessor httpContextAccessor)
    {
      this.env = env;
      this.httpContextAccessor = httpContextAccessor;
    }

    public Task DeleteFile(string path, string container)
    {
      if (path != null)
      {
        var nameFile = Path.GetFileName(path);
        string fileDirectory = Path.Combine(env.WebRootPath,container, nameFile);

        if (File.Exists(fileDirectory))
        {
          File.Delete(fileDirectory);
        }
      }

      return Task.FromResult(0);
    }

    public async Task<string> EditFile(byte[] content, string extension, string container, string path, string contentType)
    {
      await DeleteFile(path, container);
      return await SaveFile(content, extension, container, contentType);
    }

    public async Task<string> SaveFile(byte[] content, string extension, string container, string contentType)
    {
      var nameFile = $"{Guid.NewGuid()}{extension}";
      string folder = Path.Combine(env.WebRootPath, container);

      if (!Directory.Exists(folder))
      {
        Directory.CreateDirectory(folder);
      }

      string path = Path.Combine(folder,nameFile);
      await File.WriteAllBytesAsync(path,content);

      var actualUrl = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";
      var urlForDB = Path.Combine(actualUrl, container, nameFile).Replace("\\","/");
      return urlForDB;
    }
  }
}
