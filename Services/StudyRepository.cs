using GaoKao_English_Words.Models;

namespace GaoKao_English_Words.Services;

public class StudyRepository
{
    private readonly DatabaseService _databaseService;

    public StudyRepository(DatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    public async Task<StudyRecord?> GetStudyRecordAsync(int wordId)
    {
        var db = _databaseService.GetConnection();
        return await db.Table<StudyRecord>().FirstOrDefaultAsync(r => r.WordId == wordId);
    }

    public async Task<int> GetTodayStudyCountAsync()
    {
        var db = _databaseService.GetConnection();
        var today = DateTime.Today;
        return await db.Table<StudyRecord>()
            .Where(r => r.LastReviewDate.Date == today)
            .CountAsync();
    }

    public async Task<int> GetTotalStudyCountAsync()
    {
        var db = _databaseService.GetConnection();
        return await db.Table<StudyRecord>().CountAsync();
    }

    public async Task<double> GetAverageProficiencyAsync()
    {
        var db = _databaseService.GetConnection();
        var records = await db.Table<StudyRecord>().ToListAsync();
        if (records.Count == 0) return 0;
        return records.Average(r => r.Proficiency);
    }

    public async Task RecordStudyAsync(int wordId, bool isCorrect)
    {
        var db = _databaseService.GetConnection();
        var record = await GetStudyRecordAsync(wordId) ?? new StudyRecord { WordId = wordId };

        record.Total++;
        if (isCorrect)
            record.Correct++;

        record.LastReviewDate = DateTime.Now;
        record.ReviewCount++;
        record.Proficiency = (double)record.Correct / record.Total * 100;

        if (record.Id == 0)
            await db.InsertAsync(record);
        else
            await db.UpdateAsync(record);
    }

    public async Task<List<StudyRecord>> GetStudyRecordsAsync()
    {
        var db = _databaseService.GetConnection();
        return await db.Table<StudyRecord>().ToListAsync();
    }
}
