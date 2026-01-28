# Body2 Anatomy Viewer - Feature Summary

## 项目概述 (Project Overview)

基于Unity开发的移动端人体解剖模型查看系统，参考了Z-Anatomy项目，实现了骨骼和肌肉的分层显示与交互功能。

A Unity-based mobile anatomy model viewing system, inspired by Z-Anatomy, featuring layered display and interactive capabilities for bones and muscles.

---

## ✅ 已实现功能 (Implemented Features)

### 1. 分层显示控制 (Layer-Based Display Control)

#### 骨骼层 (Bone Layer)
- ✅ 独立的骨骼层显示控制
- ✅ 可单独显示/隐藏骨骼系统

#### 肌肉层 (Muscle Layers 1-7)
- ✅ 7个独立的肌肉层
- ✅ 从表层到深层的肌肉分层
- ✅ 每层可独立控制显示/隐藏

#### 层控制功能 (Layer Control Features)
- ✅ 单独切换任意层
- ✅ 一键显示全部层
- ✅ 一键隐藏全部层
- ✅ 仅显示指定层组合
- ✅ UI界面层控制面板

**核心类 (Core Classes):**
- `LayerManager.cs` - 层管理器
- `LayerControlUI.cs` - 层控制UI

---

### 2. 点击选择与信息显示 (Click Selection & Information Display)

#### 交互选择 (Interactive Selection)
- ✅ 点击/触摸选择骨骼或肌肉
- ✅ 选中部位高亮显示
- ✅ 支持PC鼠标和移动触摸
- ✅ 射线检测选择系统

#### 信息面板 (Information Panel)
- ✅ 显示解剖部位名称
- ✅ 显示详细说明/描述
- ✅ 显示类型（骨骼/肌肉）
- ✅ 显示所属层级
- ✅ 自动弹出信息面板

**核心类 (Core Classes):**
- `SelectionController.cs` - 选择控制器
- `InfoPanelUI.cs` - 信息显示UI
- `AnatomyPart.cs` - 解剖部位基类

---

### 3. 相机控制 (Camera Controls)

#### 旋转功能 (Rotation)
- ✅ 鼠标拖拽旋转（PC端）
- ✅ 单指拖动旋转（移动端）
- ✅ 环绕模型360度旋转
- ✅ 垂直角度限制（避免翻转）
- ✅ 平滑阻尼效果

#### 缩放功能 (Zoom)
- ✅ 鼠标滚轮缩放（PC端）
- ✅ 双指捏合缩放（移动端）
- ✅ 最小/最大距离限制
- ✅ 平滑缩放过渡

#### 移动功能 (Pan)
- ✅ 鼠标中键拖动（PC端）
- ✅ 双指拖动平移（移动端）
- ✅ 任意方向移动视角

#### 其他功能 (Other Features)
- ✅ 重置相机到默认位置
- ✅ 聚焦到指定点
- ✅ 可配置的相机参数

**核心类 (Core Classes):**
- `CameraController.cs` - 相机控制器

---

### 4. 按名称搜索 (Search by Name)

#### 搜索功能 (Search Features)
- ✅ 实时搜索解剖部位
- ✅ 模糊匹配部位名称和描述
- ✅ 搜索结果按相关度排序
- ✅ 最小搜索字符数设置

#### 搜索结果 (Search Results)
- ✅ 结果列表显示
- ✅ 点击结果自动选择部位
- ✅ 自动显示所属层级
- ✅ 高亮搜索结果
- ✅ 无结果提示

#### 搜索UI (Search UI)
- ✅ 搜索输入框
- ✅ 搜索按钮
- ✅ 清除按钮
- ✅ 结果容器
- ✅ 结果项目预制体

**核心类 (Core Classes):**
- `SearchManager.cs` - 搜索管理器
- `SearchUI.cs` - 搜索UI控制器

---

## 🎯 额外功能 (Additional Features)

### 示例数据系统 (Sample Data System)
- ✅ 15个骨骼示例数据
- ✅ 跨7层的肌肉示例数据
- ✅ ScriptableObject数据库
- ✅ 可扩展的数据结构

**核心类 (Core Classes):**
- `AnatomyDatabase.cs` - 数据库
- `SampleAnatomyData.cs` - 示例数据生成器

### 材质和视觉系统 (Material & Visual System)
- ✅ 骨骼默认材质（米白色）
- ✅ 肌肉分层颜色（7种红色层次）
- ✅ 高亮材质系统
- ✅ 透明材质支持
- ✅ 材质工具类

**核心类 (Core Classes):**
- `Utilities.cs` (MaterialUtility) - 材质工具

### 多语言支持 (Localization)
- ✅ 中文界面
- ✅ 英文界面
- ✅ 可切换语言
- ✅ 解剖术语中英对照

**核心类 (Core Classes):**
- `LocalizationManager.cs` - 本地化管理器

### Unity编辑器工具 (Unity Editor Tools)
- ✅ 一键创建管理器对象
- ✅ 自动设置相机
- ✅ 生成示例解剖部位
- ✅ 创建数据库资源
- ✅ 批量配置工具
- ✅ 自定义Inspector界面

**核心类 (Core Classes):**
- `AnatomyViewerEditor.cs` - 编辑器工具

### 配置系统 (Configuration System)
- ✅ 可配置的查看器预设
- ✅ 默认可见层设置
- ✅ 相机参数配置
- ✅ 搜索参数配置
- ✅ UI显示配置

**核心类 (Core Classes):**
- `ViewerConfiguration.cs` - 配置管理器

### 工具类 (Utility Classes)
- ✅ 输入处理工具（统一PC/移动输入）
- ✅ 解剖工具（获取特定类型部位）
- ✅ 边界计算工具
- ✅ 材质创建工具

**核心类 (Core Classes):**
- `Utilities.cs` - 工具集合

---

## 📁 项目结构 (Project Structure)

```
body2/
├── Assets/
│   ├── Editor/
│   │   └── AnatomyViewerEditor.cs          # Unity编辑器工具
│   ├── Scripts/
│   │   ├── AnatomyPart.cs                  # 解剖部位基类
│   │   ├── AnatomyDatabase.cs              # 数据库
│   │   ├── AnatomyViewerManager.cs         # 主管理器
│   │   ├── CameraController.cs             # 相机控制
│   │   ├── LayerManager.cs                 # 层管理
│   │   ├── SelectionController.cs          # 选择控制
│   │   ├── SearchManager.cs                # 搜索管理
│   │   ├── InfoPanelUI.cs                  # 信息面板UI
│   │   ├── LayerControlUI.cs               # 层控制UI
│   │   ├── SearchUI.cs                     # 搜索UI
│   │   ├── LocalizationManager.cs          # 本地化
│   │   ├── ViewerConfiguration.cs          # 配置
│   │   ├── Utilities.cs                    # 工具类
│   │   ├── SampleAnatomyData.cs            # 示例数据
│   │   └── ExampleSetup.cs                 # 示例设置
│   ├── Prefabs/                            # 预制体
│   ├── Scenes/                             # 场景
│   ├── Materials/                          # 材质
│   ├── UI/                                 # UI资源
│   └── Resources/                          # 运行时资源
├── README.md                                # 项目说明
├── UsageGuide.md                           # 使用指南
├── API_Documentation.md                    # API文档
├── ProjectConfiguration.md                 # 项目配置
└── .gitignore                              # Git忽略文件
```

---

## 🚀 快速开始 (Quick Start)

### 在Unity编辑器中 (In Unity Editor)

1. **使用设置向导 (Using Setup Wizard):**
   ```
   Tools > Anatomy Viewer > Setup Wizard
   ```
   - 点击 "Create Manager GameObject"
   - 点击 "Setup Camera"
   - 点击 "Generate Sample Anatomy Parts"

2. **运行场景 (Run the Scene):**
   - 按下Play按钮
   - 使用鼠标/触摸与模型交互
   - 点击部位查看信息
   - 使用层控制面板
   - 尝试搜索功能

### 构建移动应用 (Build for Mobile)

#### Android:
```
File > Build Settings > Android
- Switch Platform
- Player Settings配置
- Build
```

#### iOS:
```
File > Build Settings > iOS
- Switch Platform
- Player Settings配置
- Build (生成Xcode项目)
```

---

## 📖 核心操作 (Core Operations)

### PC控制 (PC Controls)
- **左键拖动**: 旋转
- **中键拖动**: 平移
- **滚轮**: 缩放
- **左键点击**: 选择

### 移动控制 (Mobile Controls)
- **单指拖动**: 旋转
- **双指捏合**: 缩放
- **双指拖动**: 平移
- **点击**: 选择

---

## 🎓 编程接口示例 (Programming Examples)

### 控制层显示 (Control Layer Visibility)
```csharp
LayerManager layerMgr = FindObjectOfType<LayerManager>();
layerMgr.ShowOnlyLayers(AnatomyLayer.Bone, AnatomyLayer.Muscle1);
```

### 搜索部位 (Search Parts)
```csharp
SearchManager searchMgr = FindObjectOfType<SearchManager>();
searchMgr.Search("humerus");
```

### 监听选择事件 (Listen to Selection)
```csharp
SelectionController selector = FindObjectOfType<SelectionController>();
selector.onPartSelected.AddListener((part) =>
{
    Debug.Log($"Selected: {part.partName}");
});
```

---

## 📚 文档 (Documentation)

- **README.md** - 项目概述和功能介绍
- **UsageGuide.md** - 详细使用指南
- **API_Documentation.md** - 完整API文档
- **ProjectConfiguration.md** - 项目配置说明

---

## ✨ 技术特点 (Technical Highlights)

1. **模块化设计** - 各组件独立可复用
2. **事件驱动** - 松耦合的组件通信
3. **移动优化** - 针对触摸输入优化
4. **可扩展性** - 易于添加新功能和数据
5. **编辑器集成** - 提供Unity编辑器工具
6. **多语言支持** - 中英文界面
7. **完整文档** - 详细的API和使用文档

---

## 🔧 系统要求 (Requirements)

- Unity 2020.3 LTS 或更高版本
- TextMeshPro 包
- Android API Level 21+ (移动端)
- iOS 11.0+ (移动端)

---

## 📝 许可 (License)

本项目仅供教育和开发使用。

This project is provided for educational and development purposes.

---

## 🙏 参考 (References)

基于 Z-Anatomy 项目概念: https://github.com/LluisV/Z-Anatomy

Based on Z-Anatomy concept: https://github.com/LluisV/Z-Anatomy
