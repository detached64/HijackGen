# 如何通过生成 def 文件实现 dll 劫持

## 环境

* MinGW-w64

## 准备工作

* 把需要劫持的 dll 文件放在目标目录下 (如无)，并重命名，例如：
  * `C:\Windows\SysWOW64\user32.dll` 复制到 `C:\Users\user\Desktop\test` 目录下，并重命名为 `user32_org.dll`
  * `C:\Users\user\Desktop\test\my.dll` 重命名为 `my_org.dll`

## 劫持 32 位 dll

1. 下载本项目，选择**重命名过**的 dll，例如 `user32_org.dll`。
2. 点击 `生成 def 文件` 按钮，生成 def 文件到目标目录下。
3. 把准备好的 c/cpp 源文件放到目标目录下。
4. 在目标目录打开 cmd，输入 `i686-w64-mingw32-gcc -shared -o user32.dll user32.c user32_org.def -s`，生成劫持 dll 文件。
   注：如果是 cpp 文件，使用 `i686-w64-mingw32-g++`

## 劫持 64 位 dll

1. 同上
2. 同上
3. 同上
4. 在目标目录打开 cmd，输入 `x86_64-w64-mingw32-gcc -shared -o user32.dll user32.c user32_org.def -s`，生成劫持 dll 文件。