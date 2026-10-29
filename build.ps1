$ErrorActionPreference = 'Stop'
Set-Location $PSScriptRoot
$compiler = Join-Path $env:WINDIR 'Microsoft.NET\Framework64\v4.0.30319\csc.exe'
& $compiler /nologo /target:winexe /optimize+ /win32icon:MD5Tool.ico /out:MD5Tool.exe /reference:System.Windows.Forms.dll /reference:System.Drawing.dll Md5Tool.cs
if ($LASTEXITCODE -ne 0) { throw 'Build failed' }
