rem del ..\..\Xyperico.Website\Xyperico.Website.Host\bin\Areas\Xyperico.Authentication.*
xcopy bin\Xyperico.Authentication.* ..\..\Xyperico.Website\Xyperico.Website.Host\bin\Areas /I /Y
xcopy bin\da\Xyperico.Authentication.* ..\..\Xyperico.Website\Xyperico.Website.Host\bin\Areas\da /I /Y

mkdir ..\..\Xyperico.Website\Xyperico.Website.Host\Areas\Login\Views
xcopy Areas\Login\Views\*.* ..\..\Xyperico.Website\Xyperico.Website.Host\Areas\Login\Views\ /I /Y /S

mkdir ..\..\Xyperico.Website\Xyperico.Website.Host\Areas\Account\Views
xcopy Areas\Account\Views\*.* ..\..\Xyperico.Website\Xyperico.Website.Host\Areas\Account\Views\ /I /Y /S
