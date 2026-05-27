using GaoKao_English_Words.ViewModels;

namespace GaoKao_English_Words.Pages;

public partial class StudyPage : ContentPage
{
    private StudyViewModel _viewModel = null!;

    public StudyPage(StudyViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadStudyListCommand.ExecuteAsync(null);
        
        // Bind properties
        ProgressBar.ProgressCommand = new Command(() => { });
    }

    private void OnShowMeaningClicked(object sender, EventArgs e)
    {
        _viewModel.ShowMeaningToggleCommand.Execute(null);
        MeaningStack.IsVisible = _viewModel.ShowMeaning;
        ShowMeaningButton.Text = _viewModel.ShowMeaning ? "隐藏含义" : "显示含义";
    }

    private void OnCorrectClicked(object sender, EventArgs e)
    {
        _viewModel.MarkAsCorrectCommand.Execute(null);
        UpdateUI();
    }

    private void OnIncorrectClicked(object sender, EventArgs e)
    {
        _viewModel.MarkAsIncorrectCommand.Execute(null);
        UpdateUI();
    }

    private void OnMarkClicked(object sender, EventArgs e)
    {
        _viewModel.ToggleMarkCommand.Execute(null);
        MarkButton.Text = _viewModel.CurrentWord?.IsMarked == true ? "⭐ 已标记" : "⭐ 标记";
    }

    private void UpdateUI()
    {
        if (_viewModel.CurrentWord != null)
        {
            EnglishLabel.Text = _viewModel.CurrentWord.English.ToUpper();
            PhoneticLabel.Text = _viewModel.CurrentWord.Phonetic;
            PosLabel.Text = _viewModel.CurrentWord.PartOfSpeech;
            ChineseLabel.Text = _viewModel.CurrentWord.Chinese;
            ExampleLabel.Text = _viewModel.CurrentWord.Example;
            MeaningStack.IsVisible = false;
            ShowMeaningButton.Text = "显示含义";
        }

        if (_viewModel.StudyComplete)
        {
            CompleteStack.IsVisible = true;
            ProgressBar.IsVisible = false;
            ProgressLabel.IsVisible = false;
            EnglishLabel.IsVisible = false;
            PhoneticLabel.IsVisible = false;
            PosLabel.IsVisible = false;
            MeaningStack.IsVisible = false;
            CorrectButton.IsVisible = false;
            IncorrectButton.IsVisible = false;
            MarkButton.IsVisible = false;
        }
        else
        {
            ProgressBar.Progress = _viewModel.Progress / 100.0;
            ProgressLabel.Text = _viewModel.ProgressText;
        }
    }
}
