using Syncfusion.Maui.Core.Hosting;
using Syncfusion.Maui.DataGrid.Hosting;
using TraCuuLmh.UI.Service;
using TraCuuLmh.UI.ViewModel;

namespace TraCuuLmh.UI
{
	public static class MauiProgram
	{
		public static MauiApp CreateMauiApp()
		{
			var builder = MauiApp.CreateBuilder();
			builder
				.UseMauiApp<App>()
				.ConfigureFonts(fonts =>
				{
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
					fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				});
			builder.ConfigureSyncfusionCore();
			builder.ConfigureSyncfusionDataGrid();
			builder.Services.AddSingleton<MainPage>();
			builder.Services.AddSingleton<MainViewModel>();
			builder.Services.AddTransient<DataService>();

			return builder.Build();
		}
	}
}