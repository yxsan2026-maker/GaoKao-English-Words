using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GaoKao_English_Words.Services;

namespace GaoKao_English_Words.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly DatabaseService _databaseService;
    private readonly WordRepository _wordRepository;
    private readonly StudyRepository _studyRepository;

    [ObservableProperty]
    private int totalWords = 0;

    [ObservableProperty]
    private int masteredWords = 0;

    [ObservableProperty]
    private int todayStudyCount = 0;

    [ObservableProperty]
    private double proficiency = 0;

    public MainViewModel(DatabaseService databaseService, WordRepository wordRepository, StudyRepository studyRepository)
    {
        _databaseService = databaseService;
        _wordRepository = wordRepository;
        _studyRepository = studyRepository;
    }

    [RelayCommand]
    public async Task Initialize()
    {
        await _databaseService.InitializeAsync();
        await RefreshStats();
    }

    public async Task RefreshStats()
    {
        var words = await _wordRepository.GetAllWordsAsync();
        TotalWords = words.Count;

        var records = await _studyRepository.GetStudyRecordsAsync();
        MasteredWords = records.Count(r => r.Proficiency >= 80);
        TodayStudyCount = await _studyRepository.GetTodayStudyCountAsync();
        Proficiency = await _studyRepository.GetAverageProficiencyAsync();
    }
}
