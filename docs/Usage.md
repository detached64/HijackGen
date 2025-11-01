# Usage

- [Dll Hijacking with .def](Def.md)
- [Dll Hijacking with .h](H.md)

## Knowledge

- 32-bit system dlls are in `C:\Windows\SysWOW64`, 64-bit system dlls are in `C:\Windows\System32`.
- When a 32-bit program accesses the `C:\Windows\System32` directory, the system will automatically redirect to the `C:\Windows\SysWOW64` directory; 64-bit programs do not have similar redirection.
