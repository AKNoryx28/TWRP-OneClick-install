@echo off
set VERSION=v3.0
set AUTHOR=ASKAN AKMALA
set B_DATE=27/02/2021
set "Vbmeta=%~dp0vbmeta\*.img"
set "adb=%~dp0bin\adb.exe"
set "fastboot=%~dp0bin\fastboot.exe"
set "Recovery=%~dp0recovery\*.img"
goto menu

:menu
TITLE TWRP RECOVERY INSTALLER by Askan
color 5b
mode con:cols=80 lines=23
call :barner
echo   MENU )) 
echo.
echo  [1] Flash TWRP
echo.
echo  [2] Flash VBMETA				[X] Keluar
echo.
choice /C:12X /N /M "Pilih Nomor :"
if errorlevel 3 goto :x
if errorlevel 2 goto :vb
if errorlevel 1 goto :tw
:vb
cls
call :barner
echo   MENU )) Flash VBMETA ))
echo.
echo "Do you want to Continue flash VBMETA" %Vbmeta%
echo.
choice /C:YC /N /M "'Y'es, or 'C'ancel"
if errorlevel 2 goto :menu
if errorlevel 1 goto :vbflh
:vbflh
echo  Flashing . . .
echo.
%fastboot% --disable-verification flash vbmeta %Vbmeta%
echo.
echo All done ...
echo.
pause
goto menu
:tw
cls
call :barner
echo   MENU )) Flash TWRP ))
echo.
echo "Do you want to Continue flash RECOVERY" %Recovery%
echo.
choice /C:YC /N /M "'Y'es, or 'C'ancel :"
if errorlevel 2 goto :menu
if errorlevel 1 goto :twflh
:twflh
echo.
echo  Flashing . . .
echo.
%fastboot% flash recovery %Recovery%
echo.
echo All done ...
echo.
echo "Do you want to Reboot To Recovery mode?"
echo Maybe not work for some Device Must be Reboot manually
choice /C:YC /N /M "'Y'es, or 'C'ancel :"
if errorlevel 2 goto :menu
if errorlevel 1 goto :r
:r
echo.
echo Rebooting to recovery ...
echo.
echo (Press any key to continue)
pause>nul
%fastboot% boot %Recovery%
echo.
echo All done ...
echo.
choice /C:XB /N /M "'X'Exit, or 'B'ack to menu :"
if errorlevel 2 goto :menu
if errorlevel 1 goto :x
:x
cls
call :barner
echo   MENU ))  Keluar ))
echo.
echo Exit ...
pause
exit
:barner
echo  "[                                                     BUILD_DATE:%B_DATE%]"
echo  "[Nama Penguna  : %USERNAME%                                           dd/mm/yyyy]"
echo  "[Nama Komputer : %COMPUTERNAME%                                           ]"
echo  "[AUTHOR        : %AUTHOR%                                          v3.0]"
echo  "[==========================================================================]"
echo   ############################################################################
echo   #                                                                          #
echo   #                         TWRP Recovery Installer                          #
echo   #                                  %VERSION%                                    #
echo   #                                   By                                     #
echo   #                              %AUTHOR%                                #
echo   #                                                                          #
echo   ############################################################################
echo.
goto :eof