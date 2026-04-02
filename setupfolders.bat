@echo off
echo Setting up Bamboo Enterprise Folder Hierarchy...

IF NOT EXIST "Data" mkdir "Data"
IF NOT EXIST "Reports" mkdir "Reports"
IF NOT EXIST "Reports\PDF" mkdir "Reports\PDF"
IF NOT EXIST "Reports\Excel" mkdir "Reports\Excel"
IF NOT EXIST "Reports\Text" mkdir "Reports\Text"

echo.
echo Folders created successfully!
echo ------------------------------------------
echo Location: %cd%
echo ------------------------------------------
pause