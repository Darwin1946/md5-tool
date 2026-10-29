# MD5 文件校验工具

适用于 Windows 的轻量桌面工具，支持拖入文件、计算文件内容的 MD5、一键复制结果。

## 使用

下载 `MD5Tool.exe` 后双击运行。拖入一个文件或点击“选择文件”，再点击“计算 MD5”和“复制 MD5”。计算在后台进行。

运行需要 .NET Framework 4.5 或以上版本。Windows 10/11 通常已内置兼容版本。

## 构建

在 Windows PowerShell 中运行 `./build.ps1`。无需 Python 即可编译程序。

`Md5Tool.cs` 为程序源码；`MD5Tool.ico` 为多尺寸图标。可选的 `make_icon.py` 使用 Pillow 重新生成图标。

MD5 用于文件一致性校验，不适合安全签名或密码存储。
