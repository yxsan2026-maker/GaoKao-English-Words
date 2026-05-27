using SQLite;

namespace GaoKao_English_Words.Models;

[Table("study_records")]
public class StudyRecord
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [Indexed]
    public int WordId { get; set; }

    public int Correct { get; set; } = 0; // 正确次数

    public int Total { get; set; } = 0; // 总次数

    public int ReviewCount { get; set; } = 0; // 复习次数

    public DateTime LastReviewDate { get; set; } = DateTime.Now;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public double Proficiency { get; set; } = 0; // 掌握度 0-100
}
