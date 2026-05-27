# 安装和部署指南

## 环境准备

### 1. 安装 .NET SDK

下载并安装 .NET 8.0 SDK：
- 官网地址: https://dotnet.microsoft.com/download
- 下载 ".NET 8.0 SDK" 版本
- 安装完后验证：
  ```bash
  dotnet --version
  ```

### 2. 安装开发工具

选择以下任一开发工具：

#### 选项 A: Visual Studio 2022
- 下载地址: https://visualstudio.microsoft.com/vs/
- 安装时选择「.NET Multi-platform App UI (MAUI)」工作负载
- 同时需要 Android SDK

#### 选项 B: Visual Studio Code
- 下载地址: https://code.visualstudio.com/
- 安装扩展：
  - C# 扩展
  - .NET MAUI 扩展

### 3. 安装 Android SDK

#### 方式 1: 通过 Android Studio（推荐）
1. 下载 Android Studio: https://developer.android.com/studio
2. 安装时选择包含 Android SDK
3. 安装 Android SDK API 21 或更高版本

#### 方式 2: 命令行安装
```bash
# 设置 ANDROID_HOME 环境变量
# Windows:
set ANDROID_HOME=%APPDATA%\Android\Sdk

# macOS/Linux:
export ANDROID_HOME=$HOME/Library/Android/Sdk

# 下载 SDK
cd $ANDROID_HOME
./cmdline-tools/latest/bin/sdkmanager "platforms;android-33" "build-tools;33.0.0"
```

## 获取源代码

### 方式 1: 克隆 GitHub 仓库
```bash
git clone https://github.com/yxsan2026-maker/GaoKao-English-Words.git
cd GaoKao-English-Words
```

### 方式 2: 下载 ZIP
1. 访问 https://github.com/yxsan2026-maker/GaoKao-English-Words
2. 点击 "Code" → "Download ZIP"
3. 解压文件

## 编译构建

### 使用 Visual Studio 2022

1. 打开 `GaoKao-English-Words.csproj`
2. 在解决方案资源管理器中：
   - 确保选择了正确的启动项目
   - 右键 → "构建"
3. 等待编译完成

### 使用命令行

```bash
# 恢复依赖
dotnet restore

# 构建 Debug 版本
dotnet build -f net8.0-android -c Debug

# 构建 Release 版本
dotnet build -f net8.0-android -c Release
```

## 部署到 Android

### 准备 Android 设备

**方式 1: 使用真实设备**
1. 在 Android 手机上启用「开发者选项"：
   - 设置 → 关于手机
   - 连续点击「版本号」7 次
   - 返回设置 → 系统 → 开发者选项
2. 启用「USB 调试"
3. 用 USB 线连接电脑
4. 在弹出的授权窗口中选择允许

**方式 2: 使用 Android 模拟器**
1. 打开 Android Studio
2. 点击 "AVD Manager"
3. 创建虚拟设备
4. 启动模拟器

### 使用 Visual Studio 部署

1. 在工具栏选择目标设备
   - 若为真机，选择你的设备
   - 若为模拟器，选择相应模拟器
2. 按 F5 或点击「运行"
3. 等待应用部署和运行

### 使用命令行部署

```bash
# 列出已连接的设备
adb devices

# 构建并部署
dotnet publish -f net8.0-android -c Release

# 或使用 maui 命令
dotnet maui build -f net8.0-android
```

## 生成 APK 安装包

### 生成调试 APK

```bash
dotnet publish -f net8.0-android -c Debug
```

APK 文件位置：
```
bin/Release/net8.0-android/publish/
```

### 生成发布 APK（有签名）

#### 1. 创建密钥库
```bash
keytool -genkey -v -keystore my-release-key.jks \
  -keyalg RSA -keysize 2048 -validity 10000 \
  -alias my-key-alias
```

#### 2. 配置项目文件

编辑 `GaoKao-English-Words.csproj`，在 `PropertyGroup` 中添加：

```xml
<AndroidKeyStore>true</AndroidKeyStore>
<AndroidSigningKeyStore>my-release-key.jks</AndroidSigningKeyStore>
<AndroidSigningKeyAlias>my-key-alias</AndroidSigningKeyAlias>
<AndroidSigningKeyPass>PASSWORD</AndroidSigningKeyPass>
<AndroidSigningStorePass>PASSWORD</AndroidSigningStorePass>
```

#### 3. 发布
```bash
dotnet publish -f net8.0-android -c Release
```

生成的 APK 文件在：
```
bin/Release/net8.0-android/publish/com.yxsan2026.gaokao.english-Signed.apk
```

## 手动安装 APK

### 使用 ADB 安装

```bash
adb install path/to/app.apk
```

### 使用文件管理器安装

1. 将 APK 文件复制到手机
2. 打开文件管理器，找到 APK 文件
3. 点击安装
4. 按照提示完成安装

## 故障排查

### 问题 1: 找不到 Android SDK

**解决方案:**
- 检查 `ANDROID_HOME` 环境变量
- 确保 Android SDK 已安装
- 重启 IDE 或命令行

### 问题 2: 编译错误

**解决方案:**
```bash
# 清理缓存
dotnet clean

# 恢复依赖
dotnet restore

# 重新构建
dotnet build -f net8.0-android -c Debug
```

### 问题 3: 设备未显示

**解决方案:**
- 检查 USB 线是否正常连接
- 确保启用了 USB 调试
- 在手机上确认授权弹窗
- 重新启动 ADB：`adb kill-server` → `adb start-server`

### 问题 4: 应用崩溃

**查看日志:**
```bash
adb logcat | grep GaoKao
```

## 优化建议

### 发布前检查清单

- [ ] 更新版本号
- [ ] 测试所有功能
- [ ] 检查数据库逻辑
- [ ] 优化应用大小
- [ ] 检查权限配置
- [ ] 测试不同 Android 版本

### 应用大小优化

```bash
# 启用链接器
# 在项目文件中添加
<PublishTrimmed>true</PublishTrimmed>
<PublishReadyToRun>true</PublishReadyToRun>
```

## 常见问题

**Q: APK 文件很大？**
A: .NET MAUI 应用通常 50-100MB。可通过启用 Trimming 和 AOT 编译来减小大小。

**Q: 如何更新已安装的应用？**
A: 构建新版本 APK 并运行 `adb install -r app.apk`（-r 表示覆盖安装）。

**Q: 支持的最低 Android 版本是多少？**
A: 当前配置为 API 21（Android 5.0）。

## 进阶配置

### 开启 ProGuard 混淆

```xml
<PropertyGroup>
  <PublishTrimmed>true</PublishTrimmed>
  <EnableProguard>true</EnableProguard>
</PropertyGroup>
```

### 配置应用图标和启动屏

图标文件位置：`Resources/AppIcon/`
启动屏：`Resources/Splash/`

## 更多资源

- [.NET MAUI 文档](https://learn.microsoft.com/maui/)
- [Android 开发文档](https://developer.android.com/)
- [GitHub 项目](https://github.com/yxsan2026-maker/GaoKao-English-Words)

---

**需要帮助？**

在 GitHub Issues 中提交问题或发送邮件至 yxsan2026@gmail.com
