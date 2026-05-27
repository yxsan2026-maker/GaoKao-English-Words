using GaoKao_English_Words.ViewModels;

namespace GaoKao_English_Words.Pages;

public partial class StatisticsPage : ContentPage
{
    private StatisticsViewModel _viewModel = null!;

    public StatisticsPage(StatisticsViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadStatisticsCommand.ExecuteAsync(null);
        UpdateUI();
    }

    private void OnRefreshClicked(object sender, EventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await _viewModel.LoadStatisticsCommand.ExecuteAsync(null);
            UpdateUI();
        });
    }

    private void UpdateUI()
    {
        TotalWordsLabel.Text = _viewModel.TotalWords.ToString();
        MasteredLabel.Text = _viewModel.MasteredWords.ToString();
        StudyingLabel.Text = _viewModel.StudyingWords.ToString();
        MasteryRateLabel.Text = $"{_viewModel.MasteryRate:F1}%";
        AverageProficiencyLabel.Text = $"{_viewModel.AverageProficiency:F1}%";
        TodayCountLabel.Text = $"{_viewModel.TodayCount} 个";
        TotalStudyLabel.Text = $"{_viewModel.TotalStudyCount} 次";
    }
}
