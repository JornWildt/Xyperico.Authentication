rem del ..\..\Xyperico.Website\Xyperico.Website.Host\bin\Areas\Xyperico.Authentication.*
rem xcopy bin\*.* ..\..\Xyperico.Website\Xyperico.Website.Host\bin  /I /Y /D
xcopy bin\Xyperico.Authentication.* ..\..\Xyperico.Website\Xyperico.Website.Host\bin\Areas /I /Y /D
rem xcopy bin\Xyperico.Base.MongoDB.* ..\..\Xyperico.Website\Xyperico.Website.Host\bin /I /Y /D
rem xcopy bin\MongoDB.* ..\..\Xyperico.Website\Xyperico.Website.Host\bin /I /Y /D
xcopy bin\da\Xyperico.Authentication.* ..\..\Xyperico.Website\Xyperico.Website.Host\bin\Areas\da /I /Y /D

mkdir ..\..\Xyperico.Website\Xyperico.Website.Host\Areas\Account\Views
xcopy Areas\Account\Views\*.* ..\..\Xyperico.Website\Xyperico.Website.Host\Areas\Account\Views\ /I /Y /S /D

mkdir ..\..\Xyperico.Website\Xyperico.Website.Host\Areas\Account\Styles
xcopy Areas\Account\Styles\*.* ..\..\Xyperico.Website\Xyperico.Website.Host\Areas\Account\Styles\ /I /Y /S /D

mkdir ..\..\Xyperico.Website\Xyperico.Website.Host\Areas\Account\Scripts
xcopy Areas\Account\Scripts\*.* ..\..\Xyperico.Website\Xyperico.Website.Host\Areas\Account\Scripts\ /I /Y /S /D

rem AARRRGGG
rem xcopy Areas\Account\Scripts\*.* ..\..\Xyperico.Website\Xyperico.Website.Host\Areas\Account\Styles\ /I /Y /S /D
