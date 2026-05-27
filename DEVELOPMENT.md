# 开发指南

## 项目结构详解

### Models（数据模型）

定义应用中的核心数据结构：

- `Word.cs` - 单词实体
  - 属性：英文、中文、音标、词性、例句等
  - 数据库表：words

- `StudyRecord.cs` - 学习记录
  - 跟踪每个单词的学习进度
  - 计算掌握度

### Services（服务层）

处理业务逻辑和数据访问：

- `DatabaseService.cs` - 数据库管理
  - 初始化 SQLite 数据库
  - 创建表结构
  - 导入示例数据

- `WordRepository.cs` - 单词数据访问
  - CRUD 操作
  - 查询未掌握单词
  - 搜索功能

- `StudyRepository.cs` - 学习记录数据访问
  - 记录学习进度
  - 计算统计数据
  - 查询历史记录

### ViewModels（视图模型）

使用 MVVM 模式处理 UI 逻辑：

- `MainViewModel.cs` - 主视图模型
- `StudyViewModel.cs` - 学习视图模型
- `StatisticsViewModel.cs` - 统计视图模型
- `SettingsViewModel.cs` - 设置视图模型

所有 ViewModel 都继承自 `ObservableObject`（来自 MVVM Toolkit）

### Pages（用户界面）

XAML + C# 代码隐藏：

- `StudyPage` - 单词学习页面
- `StatisticsPage` - 学习统计页面
- `SettingsPage` - 应用设置页面

## 核心功能实现

### 1. 单词学习流程

```csharp
// 1. 加载待学习单词
var words = await wordRepository.GetUnmasteredWordsAsync();

// 2. 用户点击「认识」或「不会"
await studyRepository.RecordStudyAsync(wordId, isCorrect);

// 3. 更新掌握度
proficiency = correct / total * 100;
```

### 2. 复习推送机制

基于艾宾浩斯遗忘曲线的逻辑：
- 新单词：立即学习
- 掌握度 < 50%：优先复习
- 掌握度 50-80%：定期复习
- 掌握度 > 80%：标记为已掌握

### 3. 数据统计

```csharp
// 计算掌握率
var total = await wordRepository.GetAllWordsAsync();
var mastered = records.Where(r => r.Proficiency >= 80).Count();
var masteryRate = (double)mastered / total * 100;
```

## 数据库操作

### 添加新单词

```csharp
var word = new Word 
{ 
    English = "hello",
    Chinese = "你好",
    Phonetic = "/həˈləʊ/",
    Example = "Hello, world!"
};
await wordRepository.AddWordAsync(word);
```

### 查询操作

```csharp
// 获取所有单词
var allWords = await wordRepository.GetAllWordsAsync();

// 获取未掌握的单词
var unmasteredWords = await wordRepository.GetUnmasteredWordsAsync();

// 搜索单词
var results = await wordRepository.SearchWordsAsync("abandon");
```

### 更新学习记录

```csharp
await studyRepository.RecordStudyAsync(wordId, isCorrect: true);
```

## MVVM 模式应用

### ObservableProperty（可观察属性）

```csharp
[ObservableProperty]
private int totalWords = 0;  // 自动实现 INotifyPropertyChanged

// 在 ViewModel 中修改属性
TotalWords = 100;  // UI 会自动更新
```

### RelayCommand（中继命令）

```csharp
[RelayCommand]
public async Task Initialize()
{
    // 异步操作
}

// 在 XAML 中绑定
// Command="{Binding InitializeCommand}"
```

## UI 布局设计

### 学习页面布局

```
┌─────────────────────┐
│   进度条 & 文本      │  <- ProgressBar
├─────────────────────┤
│                     │
│   单词卡片          │  <- 显示单词和含义
│   (可翻转)          │
│                     │
├─────────────────────┤
│  ❌ 不会 | ✅ 认识  │  <- 按钮组
├─────────────────────┤
│     ⭐ 标记        │
└─────────────────────┘
```

## 扩展开发指南

### 添加新的学习模式

1. 在 `StudyViewModel` 中添加新方法
2. 在 XAML 中设计新的 UI
3. 绑定命令和属性

### 添加发音功能

```csharp
using Microsoft.Maui.Media;

public class TextToSpeechService
{
    public async Task Speak(string text)
    {
        await TextToSpeech.Default.SpeakAsync(
            new SpeechOptions { Locale = new Locale("en", "US") },
            text
        );
    }
}
```

### 集成云同步

```csharp
public class CloudSyncService
{
    private readonly HttpClient _httpClient;
    
    public async Task SyncToCloud(List<StudyRecord> records)
    {
        // 上传数据到服务器
    }
}
```

## 测试指南

### 单元测试示例

```csharp
[TestClass]
public class WordRepositoryTests
{
    [TestMethod]
    public async Task GetUnmasteredWords_ReturnsCorrectData()
    {
        // Arrange
        var repository = new WordRepository(mockDatabase);
        
        // Act
        var results = await repository.GetUnmasteredWordsAsync();
        
        // Assert
        Assert.IsNotNull(results);
        Assert.IsTrue(results.Count > 0);
    }
}
```

## 性能优化

### 数据库查询优化

```csharp
// 使用 SELECT 而不是获取所有数据后过滤
var query = @"
    SELECT w.* FROM words w 
    WHERE w.IsMarked = 1 
    ORDER BY w.CreatedAt DESC 
    LIMIT 50
";
```

### 内存管理

```csharp
// 大数据集分页加载
public async Task<List<Word>> GetWordsPaginatedAsync(int page, int pageSize)
{
    var db = _databaseService.GetConnection();
    return await db.Table<Word>()
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();
}
```

## 调试技巧

### 查看数据库内容

```bash
# 使用 DB Browser for SQLite
# 打开 %APPDATA%/com.yxsan2026.gaokao.english/files/gaokao.db
```

### 查看应用日志

```bash
adb logcat | grep GaoKao
```

### Visual Studio 调试

1. 设置断点
2. F5 启动调试
3. 单步执行代码

## 常见开发问题

**Q: 如何添加新页面？**
A: 创建 XAML 和代码隐藏文件，在 MauiProgram.cs 中注册，在 AppShell 中配置导航。

**Q: 如何在页面间传递数据？**
A: 使用查询参数或依赖注入的 ViewModel。

**Q: 如何处理异步操作？**
A: 使用 `async/await` 和 `RelayCommand`（支持异步）。

## 提交代码规范

### Commit 信息格式

```
[类型] 简短描述

详细说明（可选）

修复: #123
```

类型示例：
- feat: 新功能
- fix: 修复 bug
- refactor: 代码重构
- perf: 性能优化
- docs: 文档更新

### 命名规范

- 类名：PascalCase（如 `StudyViewModel`）
- 方法名：PascalCase（如 `GetUnmasteredWords`）
- 属性名：PascalCase（如 `TotalWords`）
- 私有字段：_camelCase（如 `_wordRepository`）
- 常量：ALL_UPPER（如 `DATABASE_FILENAME`）

## 有用的资源

- [MAUI 官方文档](https://learn.microsoft.com/maui/)
- [C# 指南](https://learn.microsoft.com/en-us/dotnet/csharp/)
- [SQLite 文档](https://www.sqlite.org/)
- [MVVM Toolkit](https://github.com/CommunityToolkit/dotnet)

---

**需要帮助？** 在 GitHub 提交 Issue 或联系维护者。
