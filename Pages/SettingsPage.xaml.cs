using GaoKao_English_Words.ViewModels;

namespace GaoKao_English_Words.Pages;

public partial class SettingsPage : ContentPage
{
    private SettingsViewModel _viewModel = null!;

    public SettingsPage(SettingsViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        SoundSwitch.IsToggled = _viewModel.SoundEnabled;
        NotificationSwitch.IsToggled = _viewModel.NotificationEnabled;
        DailyGoalEntry.Text = _viewModel.DailyGoal.ToString();
        ThemePicker.SelectedItem = _viewModel.Theme;
    }

    private void OnSoundToggled(object sender, ToggledEventArgs e)
    {
        _viewModel.SoundEnabled = e.Value;
    }

    private void OnNotificationToggled(object sender, ToggledEventArgs e)
    {
        _viewModel.NotificationEnabled = e.Value;
    }

    private void OnThemeChanged(object sender, EventArgs e)
    {
        if (ThemePicker.SelectedItem is string theme)
        {
            _viewModel.Theme = theme;
        }
    }

    private void OnSaveClicked(object sender, EventArgs e)
    {
        if (int.TryParse(DailyGoalEntry.Text, out var goal))
        {
            _viewModel.DailyGoal = goal;
        }
        _viewModel.SaveSettingsCommand.Execute(null);
        DisplayAlert("提示", "设置已保存", "确定");
    }
}
