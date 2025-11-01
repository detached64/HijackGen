# HijackGen

A simple tool to generate dll source code for dll hijacking.

![interface](./docs/img/interface.png)

## Features

* Support x86 and x64 PE files.
* Support generating x86 and x64 dll source code.
* Support dll proxying.
* Support invalid characters detection in function names.
* Support custom invalid characters by editing `InvalidChars.txt`.

## Download

[Release](https://github.com/detached64/HijackGen/releases/latest)

Get ci builds [here](https://github.com/detached64/HijackGen/actions/workflows/build.yml).

## Requirements

* [.NET 9 Runtime](https://dotnet.microsoft.com/download/dotnet/9.0) or download the self-contained version.

## Usage

[English](./docs/Usage.md) | [中文](./docs/Usage_zh-CN.md)

## Reference

* [PeNet](https://github.com/secana/PeNet)

* [dll-hijack-by-proxying](https://github.com/tothi/dll-hijack-by-proxying)

* [AheadLib-x86-x64](https://github.com/strivexjun/AheadLib-x86-x64)
