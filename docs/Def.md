# Dll Hijacking with .def

## Environment

* MinGW-w64

## Preparation

* Put the dll file to be hijacked in the target directory (if not), and rename it, for example:
  * Copy `C:\Windows\SysWOW64\user32.dll` to the `C:\Users\user\Desktop\test` directory, and rename it to `user32_org.dll`
  * Rename `C:\Users\user\Desktop\test\my.dll` to `my_org.dll`

## Hijack 32-bit dll

1. Download this project, select the **renamed** dll, such as `user32_org.dll`.
2. Click the `Generate .def` button to generate the def file to the target directory.
3. Put the prepared c/cpp source file in the target directory.
4. Open cmd in the target directory, enter `i686-w64-mingw32-gcc -shared -o user32.dll user32.c user32_org.def -s`, to generate the hijacked dll file.
   Note: If it is a cpp file, use `i686-w64-mingw32-g++`

## Hijack 64-bit dll

1. Same as above
2. Same as above
3. Same as above
4. Open cmd in the target directory, enter `x86_64-w64-mingw32-gcc -shared -o user32.dll user32.c user32_org.def -s`, to generate the hijacked dll file.