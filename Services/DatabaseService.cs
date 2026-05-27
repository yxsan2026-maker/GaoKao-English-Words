using SQLite;
using GaoKao_English_Words.Models;

namespace GaoKao_English_Words.Services;

public class DatabaseService
{
    private SQLiteAsyncConnection? _db;
    private const string DatabaseFilename = "gaokao.db";

    private string DbPath => Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);

    public async Task InitializeAsync()
    {
        if (_db != null)
            return;

        _db = new SQLiteAsyncConnection(DbPath,
            SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache);

        await _db.CreateTableAsync<Word>();
        await _db.CreateTableAsync<StudyRecord>();

        // 初始化示例数据
        var count = await _db.Table<Word>().CountAsync();
        if (count == 0)
        {
            await InitializeSampleData();
        }
    }

    private async Task InitializeSampleData()
    {
        var sampleWords = new[]
        {
            new Word { English = "abandon", Chinese = "放弃", PartOfSpeech = "v.", Phonetic = "/ə'bændən/", Example = "Don't abandon your dreams.", Level = 2 },
            new Word { English = "ability", Chinese = "能力", PartOfSpeech = "n.", Phonetic = "/ə'bɪlɪti/", Example = "She has great ability.", Level = 1 },
            new Word { English = "absence", Chinese = "缺席", PartOfSpeech = "n.", Phonetic = "/ˈæbsəns/", Example = "His absence was noticed.", Level = 2 },
            new Word { English = "absolute", Chinese = "绝对的", PartOfSpeech = "adj.", Phonetic = "/ˈæbsəluːt/", Example = "That's absolute nonsense.", Level = 2 },
            new Word { English = "absorb", Chinese = "吸收", PartOfSpeech = "v.", Phonetic = "/əb'zɔːb/", Example = "Plants absorb water.", Level = 2 },
            new Word { English = "abstract", Chinese = "抽象的", PartOfSpeech = "adj.", Phonetic = "/ˈæbstrækt/", Example = "Abstract art is interesting.", Level = 3 },
            new Word { English = "abundance", Chinese = "丰富", PartOfSpeech = "n.", Phonetic = "/ə'bʌndəns/", Example = "There is an abundance of food.", Level = 3 },
            new Word { English = "academic", Chinese = "学术的", PartOfSpeech = "adj.", Phonetic = "/ˌækə'demɪk/", Example = "Academic study is important.", Level = 2 },
            new Word { English = "accelerate", Chinese = "加速", PartOfSpeech = "v.", Phonetic = "/ik'seləreɪt/", Example = "The car accelerated rapidly.", Level = 3 },
            new Word { English = "accent", Chinese = "口音", PartOfSpeech = "n.", Phonetic = "/ˈæksent/", Example = "He has a British accent.", Level = 2 }
        };

        await _db!.InsertAllAsync(sampleWords);
    }

    public SQLiteAsyncConnection GetConnection()
    {
        if (_db == null)
            throw new InvalidOperationException("Database not initialized. Call InitializeAsync first.");
        return _db;
    }
}
