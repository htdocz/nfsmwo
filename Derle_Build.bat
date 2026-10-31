@echo off
chcp 65001 >nul
echo NFSMW_Save_Bot.exe derleniyor...
"C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe" /target:winexe /optimize+ /platform:anycpu /win32icon:app.ico /out:NFSMW_Save_Bot.exe /r:System.dll /r:System.Drawing.dll /r:System.Windows.Forms.dll NFSMW_Save_Bot.cs
if %errorlevel% equ 0 (
    echo [BASARILI] NFSMW_Save_Bot.exe olusturuldu!
) else (
    echo [HATA] Derleme basarisiz oldu!
)
pause
