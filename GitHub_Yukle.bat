@echo off
chcp 65001 >nul
cd /d "C:\Users\ASUS\Desktop\NFSMW_Save_Bot_Acik_Kaynak"
echo GitHub hesabiniza (https://github.com/htdocz/nfsmwo) yukleniyor...
git push -u origin main --force
if %errorlevel% equ 0 (
    echo.
    echo [BASARILI] Tum dosyalar https://github.com/htdocz/nfsmwo adresine yuklendi!
) else (
    echo.
    echo [HATA] Yukleme basarisiz oldu. Lutfen acilan pencerede htdocz hesabiyla giris yapin.
)
pause
