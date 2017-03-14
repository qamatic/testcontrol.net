del /q /s ship\*.*
mkdir ship
mkdir ship\redist
msbuild QAMatic.TestControl.sln /p:Configuration=Release /target:clean /target:build
xcopy /Y libs\*.dll ship
xcopy /Y build\release\*.dll ship
xcopy /Y build\release\*.exe ship
xcopy /Y build\release\*.config ship
rem xcopy /Y redist2012 ship\redist


cd ship 
del app.config
"C:\Program Files\WinRAR\rar.exe" a TestControl-Rel-v3.zip *.* redist
cd ..
