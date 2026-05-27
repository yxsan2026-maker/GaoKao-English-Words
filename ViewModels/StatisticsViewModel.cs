using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GaoKao_English_Words.Services;

namespace GaoKao_English_Words.ViewModels;

public partial class StatisticsViewModel : ObservableObject
{
    private readonly StudyRepository _studyRepository;
    private readonly WordRepository _wordRepository;

    [ObservableProperty]
    private int totalWords = 0;

    [ObservableProperty]
    private int masteredWords = 0;

    [ObservableProperty]
    private int studyingWords = 0;

    [ObservableProperty]
    private double masteryRate = 0;

    [ObservableProperty]
    private double averageProficiency = 0;

    [ObservableProperty]
    private int todayCount = 0;

    [ObservableProperty]
    private int totalStudyCount = 0;

    public StatisticsViewModel(StudyRepository studyRepository, WordRepository wordRepository)
    {
        _studyRepository = studyRepository;
        _wordRepository = wordRepository;
    }

    [RelayCommand]
    public async Task LoadStatistics()
    {
        var words = await _wordRepository.GetAllWordsAsync();
        var records = await _studyRepository.GetStudyRecordsAsync();

        TotalWords = words.Count;
        MasteredWords = records.Count(r => r.Proficiency >= 80);
        StudyingWords = records.Count(r => r.Proficiency < 80 && r.Proficiency > 0);
        MasteryRate = TotalWords > 0 ? (double)MasteredWords / TotalWords * 100 : 0;
        AverageProficiency = await _studyRepository.GetAverageProficiencyAsync();
        TodayCount = await _studyRepository.GetTodayStudyCountAsync();
        TotalStudyCount = await _studyRepository.GetTotalStudyCountAsync();
    }
}
