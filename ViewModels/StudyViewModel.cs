using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GaoKao_English_Words.Models;
using GaoKao_English_Words.Services;

namespace GaoKao_English_Words.ViewModels;

public partial class StudyViewModel : ObservableObject
{
    private readonly WordRepository _wordRepository;
    private readonly StudyRepository _studyRepository;
    private List<Word> _studyList = new();
    private int _currentIndex = 0;

    [ObservableProperty]
    private Word? currentWord;

    [ObservableProperty]
    private bool showMeaning = false;

    [ObservableProperty]
    private int progress = 0;

    [ObservableProperty]
    private string progressText = "";

    [ObservableProperty]
    private bool studyComplete = false;

    public StudyViewModel(WordRepository wordRepository, StudyRepository studyRepository)
    {
        _wordRepository = wordRepository;
        _studyRepository = studyRepository;
    }

    [RelayCommand]
    public async Task LoadStudyList()
    {
        _studyList = await _wordRepository.GetUnmasteredWords Async();
        _currentIndex = 0;
        StudyComplete = false;
        ShowMeaning = false;
        
        if (_studyList.Count > 0)
        {
            CurrentWord = _studyList[_currentIndex];
            UpdateProgress();
        }
    }

    [RelayCommand]
    public void ShowMeaningToggle()
    {
        ShowMeaning = !ShowMeaning;
    }

    [RelayCommand]
    public async Task MarkAsCorrect()
    {
        if (CurrentWord != null)
        {
            await _studyRepository.RecordStudyAsync(CurrentWord.Id, true);
            NextWord();
        }
    }

    [RelayCommand]
    public async Task MarkAsIncorrect()
    {
        if (CurrentWord != null)
        {
            await _studyRepository.RecordStudyAsync(CurrentWord.Id, false);
            NextWord();
        }
    }

    [RelayCommand]
    public void ToggleMark()
    {
        if (CurrentWord != null)
        {
            CurrentWord.IsMarked = !CurrentWord.IsMarked;
            _wordRepository.UpdateWordAsync(CurrentWord);
        }
    }

    private void NextWord()
    {
        _currentIndex++;
        if (_currentIndex < _studyList.Count)
        {
            CurrentWord = _studyList[_currentIndex];
            ShowMeaning = false;
            UpdateProgress();
        }
        else
        {
            StudyComplete = true;
        }
    }

    private void UpdateProgress()
    {
        Progress = (int)((_currentIndex + 1) / (double)_studyList.Count * 100);
        ProgressText = $"{_currentIndex + 1} / {_studyList.Count}";
    }
}
