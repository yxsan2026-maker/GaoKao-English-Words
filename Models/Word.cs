using SQLite;

namespace GaoKao_English_Words.Models;

[Table("words")]
public class Word
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [Indexed]
    public string English { get; set; } = string.Empty;

    public string Chinese { get; set; } = string.Empty;

    public string PartOfSpeech { get; set; } = string.Empty; // 词性

    public string Phonetic { get; set; } = string.Empty; // 音标

    public string Example { get; set; } = string.Empty; // 例句

    public int Level { get; set; } = 1; // 难度等级 1-5

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public bool IsMarked { get; set; } = false; // 是否标记
}
