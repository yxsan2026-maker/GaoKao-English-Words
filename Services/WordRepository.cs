using GaoKao_English_Words.Models;

namespace GaoKao_English_Words.Services;

public class WordRepository
{
    private readonly DatabaseService _databaseService;

    public WordRepository(DatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    public async Task<List<Word>> GetAllWordsAsync()
    {
        var db = _databaseService.GetConnection();
        return await db.Table<Word>().ToListAsync();
    }

    public async Task<Word?> GetWordByIdAsync(int id)
    {
        var db = _databaseService.GetConnection();
        return await db.FindAsync<Word>(id);
    }

    public async Task<List<Word>> GetUnmastered Words Async()
    {
        var db = _databaseService.GetConnection();
        // 掌握度低于80的单词
        return await db.QueryAsync<Word>(
            @"SELECT w.* FROM words w 
              LEFT JOIN study_records sr ON w.Id = sr.WordId
              WHERE COALESCE(sr.Proficiency, 0) < 80
              ORDER BY COALESCE(sr.LastReviewDate, w.CreatedAt) ASC"
        );
    }

    public async Task<List<Word>> GetMarkedWordsAsync()
    {
        var db = _databaseService.GetConnection();
        return await db.Table<Word>().Where(w => w.IsMarked).ToListAsync();
    }

    public async Task<List<Word>> SearchWordsAsync(string keyword)
    {
        var db = _databaseService.GetConnection();
        return await db.Table<Word>()
            .Where(w => w.English.Contains(keyword) || w.Chinese.Contains(keyword))
            .ToListAsync();
    }

    public async Task AddWordAsync(Word word)
    {
        var db = _databaseService.GetConnection();
        await db.InsertAsync(word);
    }

    public async Task UpdateWordAsync(Word word)
    {
        var db = _databaseService.GetConnection();
        await db.UpdateAsync(word);
    }

    public async Task DeleteWordAsync(int id)
    {
        var db = _databaseService.GetConnection();
        await db.DeleteAsync<Word>(id);
    }
}
