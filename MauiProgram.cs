using GaoKao_English_Words.Pages;
using GaoKao_English_Words.Services;
using GaoKao_English_Words.ViewModels;

namespace GaoKao_English_Words;

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

        // Register Services
        builder.Services.AddSingleton<DatabaseService>();
        builder.Services.AddSingleton<WordRepository>();
        builder.Services.AddSingleton<StudyRepository>();

        // Register ViewModels
        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddSingleton<StudyViewModel>();
        builder.Services.AddSingleton<StatisticsViewModel>();
        builder.Services.AddSingleton<SettingsViewModel>();

        // Register Pages
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<StudyPage>();
        builder.Services.AddSingleton<StatisticsPage>();
        builder.Services.AddSingleton<SettingsPage>();

        return builder.Build();
    }
}
