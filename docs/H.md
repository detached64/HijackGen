# Dll Hijacking with .h

## Environment

* Visual Studio 2022

## Hijack 32-bit dll

1. Create an empty dll project, for example `version`.
2. Download this project and select the dll to hijack, for example `C:\Windows\SysWOW64\version.dll`.
3. Click the `Generate .h` button, select `x86`, and generate the h file to the project directory.
4. Add `#include "version.h"` in `dllmain.cpp`.
5. Call the `InitHijack` and `FreeHijack` functions to implement the hijacking.
6. Compile the project, generate the dll file, and copy it to the target directory.

## Hijack 64-bit dll

1. Create an empty dll project, for example `version`.
2. Download this project and select the dll to hijack, for example `C:\Windows\System32\version.dll`.
3. Click the `Generate .h` button, select `x64`, and check `Generate .def simultaneously`, generate the h file and def file to the project directory.
4. Add `#include "version.h"` in `dllmain.cpp`.
5. Right-click the project, select `Properties`, `Configuration Properties`, `Linker`, `Input`, `Module Definition File`, and add `version.def`.
   ![](./img/h-1.png)
6. Call the `InitHijack` and `FreeHijack` functions to implement the hijacking.
7. Compile the project, generate the dll file, and copy it to the target directory.