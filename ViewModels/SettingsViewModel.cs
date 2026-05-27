using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace GaoKao_English_Words.ViewModels;

public partial class SettingsViewModel : ObservableObject
{
    [ObservableProperty]
    private bool soundEnabled = true;

    [ObservableProperty]
    private bool notificationEnabled = true;

    [ObservableProperty]
    private int dailyGoal = 50;

    [ObservableProperty]
    private string theme = "Light";

    public SettingsViewModel()
    {
        LoadSettings();
    }

    private void LoadSettings()
    {
        SoundEnabled = Preferences.Get("sound_enabled", true);
        NotificationEnabled = Preferences.Get("notification_enabled", true);
        DailyGoal = Preferences.Get("daily_goal", 50);
        Theme = Preferences.Get("theme", "Light");
    }

    [RelayCommand]
    public void SaveSettings()
    {
        Preferences.Set("sound_enabled", SoundEnabled);
        Preferences.Set("notification_enabled", NotificationEnabled);
        Preferences.Set("daily_goal", DailyGoal);
        Preferences.Set("theme", Theme);
    }
}
