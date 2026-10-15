using RevUp.Components;
using RevUp.Data;
using RevUp.Services;

namespace RevUp;

public static class Program
{
	public static async Task Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.
		builder
			.Services
			.AddRazorComponents()
			.AddInteractiveServerComponents();

		var connectionString = builder.Configuration.GetConnectionString("RevUp")
			?? throw new InvalidOperationException("The connection string 'RevUp' is missing in the configuration.");
		var databaseOptions = RevUpDatabase.CreateOptions(connectionString);

		builder.Services.AddSingleton(TimeProvider.System);
		builder.Services.AddSingleton(databaseOptions);
		builder.Services.AddSingleton<IMeetRepository, MeetRepository>();
		builder.Services.AddSingleton<IMeetService, MeetService>();

		var app = builder.Build();

		await RevUpDatabase.InitializeAsync(databaseOptions, app.Services.GetRequiredService<TimeProvider>());

		// Configure the HTTP request pipeline.
		if (!app.Environment.IsDevelopment())
		{
			app.UseExceptionHandler("/Error", createScopeForErrors: true);
			app.UseHsts();
		}

		app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
		app.UseHttpsRedirection();

		app.UseAntiforgery();

		app.MapStaticAssets();
		app.MapRazorComponents<App>()
			.AddInteractiveServerRenderMode();

		await app.RunAsync();
	}
}