# 如何利用 h 文件实现 dll 劫持

## 环境

- Visual Studio 2022

## 劫持系统 dll

注：此方法不适用于导出含 `?`,`@` 等特殊字符的函数。

### 劫持 32 位 dll

1. 新建一个空 dll 项目，例如 `version`。
2. 下载本项目，选择要劫持的 dll，例如 `C:\Windows\SysWOW64\version.dll`。
3. 点击 `生成 h 文件` 按钮，选择 `x86`，生成 h 文件到项目目录下。
4. 在 `dllmain.cpp` 中添加 `#include "version.h"`。
5. 调用 `InitHijack` 和 `FreeHijack` 函数实现劫持。
6. 编译项目，生成 dll 文件，复制到目标目录下。

### 劫持 64 位 dll

1. 新建一个空 dll 项目，例如 `version`。
2. 下载本项目，选择要劫持的 dll，例如 `C:\Windows\System32\version.dll`。
3. 点击 `生成 h 文件` 按钮，选择 `x64`，并勾选 `同时生成def文件`，生成 h 文件和 def 文件到项目目录下。
4. 在 `dllmain.cpp` 中添加 `#include "version.h"`。
5. 右键项目，选择 `属性`，`配置属性`，`链接器`，`输入`，`模块定义文件`，添加 `version.def`。
   ![](./img/h-1_zh-CN.png)
6. 调用 `InitHijack` 和 `FreeHijack` 函数实现劫持。
7. 编译项目，生成 dll 文件，复制到目标目录下。

## 劫持自定义 dll

此方法也被称为 `dll proxy`，适用于劫持任何 dll，尤其适合需要原始 dll 存在的场景。

1. 新建一个空 dll 项目。
2. 重命名要劫持的 dll ，例如：
   - `C:\Users\user\Desktop\test\my.dll` 重命名为 `my_org.dll`
3. 下载本项目，选择**重命名后**的 dll，点击 `生成 h 文件` 按钮，选择 `自定义dll`，生成 h 文件到项目目录下。
4. 在 `dllmain.cpp` 中 `include` 生成的 h 文件。
5. 编译项目，生成 dll 文件，重命名为 `my.dll`，复制到目标目录下。
   注：原始的 dll `my_org.dll` 必须和劫持的 dll `my.dll` 在同一目录下。
