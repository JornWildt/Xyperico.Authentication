del ..\..\Xyperico.Website\Xyperico.Website.Host\bin\Areas\Xyperico.Authentication.*
xcopy bin\Xyperico.Authentication.* ..\..\Xyperico.Website\Xyperico.Website.Host\bin\Areas /I /Y

mkdir ..\..\Xyperico.Website\Xyperico.Website.Host\Areas\Login\Views
xcopy Areas\Login\Views\*.* ..\..\Xyperico.Website\Xyperico.Website.Host\Areas\Login\Views\ /I /Y /S
