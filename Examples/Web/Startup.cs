using SimpleGPIO.Boards;

namespace SimpleGPIO.Examples.Web;

public sealed class Startup
{
	public IConfiguration Configuration { get; }

	public Startup(IConfiguration configuration) => Configuration = configuration;

	public static void ConfigureServices(IServiceCollection services)
	{
		services.AddMvc();
		services.AddSingleton<RaspberryPi>();
	}

	public static void Configure(IApplicationBuilder app)
	{
		app.UseDefaultFiles();
		app.UseStaticFiles();
		app.UseRouting();
		app.UseEndpoints(c => c.MapControllers());
	}
}