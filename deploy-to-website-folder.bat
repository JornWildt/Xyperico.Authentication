set dest=%1

if "%dest%"=="" goto Error

set src=Xyperico.Authentication.Web

xcopy "%src%\bin\Xyperico.Authentication.*" "%dest%\bin\Areas" /I /Y /D
xcopy "%src%\bin\da\Xyperico.Authentication.*" "%dest%\bin\Areas\da" /I /Y /D

xcopy "%src%\Areas\Account\Views\*.*" "%dest%\Areas\Account\Views\" /I /Y /S /D
xcopy "%src%\Areas\Account\Styles\*.*" "%dest%\Areas\Account\Styles\" /I /Y /S /D
xcopy "%src%\Areas\Account\Scripts\*.*" "%dest%\Areas\Account\Scripts\" /I /Y /S /D


:Error
echo Usage: Deploy.bat dest-folder