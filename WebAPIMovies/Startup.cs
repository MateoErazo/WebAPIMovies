using Microsoft.EntityFrameworkCore;
using WebAPIMovies.Services;

namespace WebAPIMovies
{
  public class Startup
  {
    public Startup(IConfiguration configuration) {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }


    public void ConfigureServices(IServiceCollection services)
    {
      services.AddAutoMapper(typeof(Startup));

      services.AddDbContext<ApplicationDbContext>(options =>
      {
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
      });

      services.AddControllers();

      services.AddTransient<IFileStorage, FileStorageAzure>();

      services.AddHttpContextAccessor();

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      app.UseHttpsRedirection();

      app.UseStaticFiles();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints => 
      {
        endpoints.MapControllers();
      });
    }
  }
}
