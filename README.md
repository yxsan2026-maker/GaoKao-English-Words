# 高考英语单词背诵应用

一款基于 .NET MAUI 和 C# 开发的高考英语单词背诵应用，支持在 Android 手机上运行。

## 功能特性

✅ **智能单词学习**
- 人教版高考词汇库
- 多种学习模式（卡片翻转、选择题等）
- 实时进度跟踪

✅ **高效复习机制**
- 基于艾宾浩斯遗忘曲线的复习安排
- 按掌握度智能推送单词
- 标记重点词汇

✅ **详细统计分析**
- 学习进度可视化
- 掌握度统计
- 每日学习统计
- 长期学习数据追踪

✅ **个性化设置**
- 自定义每日学习目标
- 声音和通知提醒
- 主题切换

✅ **完全离线使用**
- 所有数据本地存储
- 无需网络连接
- SQLite 数据库支持

## 技术栈

- **框架**: .NET MAUI (Multi-platform App UI)
- **语言**: C#
- **数据库**: SQLite
- **MVVM 框架**: MVVM Community Toolkit
- **平台**: Android (API 21+)

## 项目结构

```
GaoKao-English-Words/
├── Models/                 # 数据模型
│   ├── Word.cs            # 单词模型
│   └── StudyRecord.cs     # 学习记录模型
├─�� Services/              # 业务逻辑层
│   ├── DatabaseService.cs # 数据库服务
│   ├── WordRepository.cs  # 单词仓储
│   └── StudyRepository.cs # 学习记录仓储
├── ViewModels/            # 视图模型
│   ├── MainViewModel.cs
│   ├── StudyViewModel.cs
│   ├── StatisticsViewModel.cs
│   └── SettingsViewModel.cs
├── Pages/                 # 页面
│   ├── StudyPage.xaml     # 学习页面
│   ├── StatisticsPage.xaml # 统计页面
│   └── SettingsPage.xaml  # 设置页面
├── MauiProgram.cs         # 应用配置
├── App.xaml               # 应用全局样式
└── AppShell.xaml          # 应用导航
```

## 快速开始

### 环境要求

- .NET 8.0 SDK 或更高版本
- Visual Studio 2022 或 Visual Studio Code
- Android SDK (API 21 或更高版本)
- 或使用 Android Studio

### 安装步骤

1. **克隆仓库**
   ```bash
   git clone https://github.com/yxsan2026-maker/GaoKao-English-Words.git
   cd GaoKao-English-Words
   ```

2. **恢复依赖**
   ```bash
   dotnet restore
   ```

3. **构建应用**
   ```bash
   dotnet build -f net8.0-android
   ```

4. **部署到 Android 手机**
   
   **使用 Visual Studio:**
   - 打开项目
   - 选择 Android 平台
   - 连接 Android 设备或启动模拟器
   - 按 F5 或点击「运行」

   **使用命令行:**
   ```bash
   dotnet build -f net8.0-android -c Release
   dotnet publish -f net8.0-android -c Release
   ```

### 首次运行

应用首次运行时会自动：n1. 创建 SQLite 数据库
2. 导入 3500+ 高考常用词汇
3. 初始化用户界面

## 使用说明

### 学习模式

1. **进入学习页面** - 点击底部「学习」标签
2. **查看单词** - 显示英文单词、音标、词性
3. **点击「显示含义」** - 查看中文翻译和例句
4. **评估掌握程度**
   - ✅ 认识 - 已掌握
   - ❌ 不会 - 未掌握
5. **标记重点** - 用 ⭐ 标记不熟悉的单词

### 统计分析

- **总词汇数** - 系统中所有单词总数
- **已掌握** - 掌握度 ≥ 80% 的单词
- **学习中** - 掌握度 0-80% 的单词
- **掌握率** - 已掌握单词占比
- **平均掌握度** - 所有单词的平均掌握度
- **今日学习** - 今天学习过的单词数
- **总学习次数** - 累计学习次数

## 数据结构

### Word 表

| 字段 | 类型 | 说明 |
|------|------|------|
| Id | int | 主键 |
| English | string | 英文单词 |
| Chinese | string | 中文翻译 |
| PartOfSpeech | string | 词性 |
| Phonetic | string | 音标 |
| Example | string | 例句 |
| Level | int | 难度等级 (1-5) |
| IsMarked | bool | 是否标记 |
| CreatedAt | datetime | 创建时间 |

### StudyRecord 表

| 字段 | 类型 | 说明 |
|------|------|------|
| Id | int | 主键 |
| WordId | int | 单词ID |
| Correct | int | 正确次数 |
| Total | int | 总次数 |
| Proficiency | double | 掌握度 (0-100) |
| ReviewCount | int | 复习次数 |
| LastReviewDate | datetime | 最后复习时间 |
| CreatedAt | datetime | 创建时间 |

## 扩展功能计划

- [ ] 发音功能 (TTS)
- [ ] 词根词缀学习
- [ ] 历年真题单词
- [ ] 云端同步
- [ ] 多设备同步
- [ ] 社区分享
- [ ] 排行榜
- [ ] 定时提醒

## 常见问题

**Q: 应用能在 iOS 上运行吗？**
A: 代码支持 iOS，但本项目主要针对 Android。如需 iOS 版本，需要配置 iOS 开发环境。

**Q: 如何导入自己的词汇库？**
A: 当前版本包含固定词汇库。后续版本将支持 CSV 导入功能。

**Q: 数据会同步到云端吗？**
A: 目前版本为离线单机应用，所有数据存储在手机本地。

**Q: 如何备份学习数据？**
A: 数据保存在 `app.db` 文件中，可通过文件管理器备份。

## 贡献指南

欢迎提交 Issue 和 Pull Request！

1. Fork 本仓库
2. 创建功能分支 (`git checkout -b feature/AmazingFeature`)
3. 提交更改 (`git commit -m 'Add some AmazingFeature'`)
4. 推送到分支 (`git push origin feature/AmazingFeature`)
5. 开启 Pull Request

## 许可证

MIT License - 详见 [LICENSE](LICENSE) 文件

## 联系方式

- GitHub: [@yxsan2026-maker](https://github.com/yxsan2026-maker)
- 邮箱: yxsan2026@gmail.com

## 致谢

- [.NET MAUI](https://github.com/dotnet/maui) - 跨平台 UI 框架
- [MVVM Community Toolkit](https://github.com/CommunityToolkit/dotnet) - MVVM 工具包
- [sqlite-net-pcl](https://github.com/praeclarum/sqlite-net) - SQLite 库
- 人教版高考英语词汇表

---

**祝你高考英语成功！**🎉
