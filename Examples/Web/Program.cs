using SimpleGPIO.Boards;

namespace SimpleGPIO.Examples.Web;

public static class Program
{
	public static async Task Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		ConfigureServices(builder.Services);

		var app = builder.Build();
		Configure(app);

		await app.RunAsync();
	}

	private static void ConfigureServices(IServiceCollection services)
	{
		services.AddMvc();
		services.AddSingleton<RaspberryPi>();
	}

	private static void Configure(IApplicationBuilder app)
	{
		app.UseDefaultFiles();
		app.UseStaticFiles();
		app.UseRouting();
		app.UseEndpoints(c => c.MapControllers());
	}
}