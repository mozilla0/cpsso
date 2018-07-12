SLEEP 50

SQLCMD -S %APPSETTING_key1% -d cpssodb -U %APPSETTING_key2% -P %APPSETTING_key3% -i "D:\home\site\wwwroot\DBScripts\Database_Schema.sql" -o "D:\home\LogFiles\kudu\deployment\DB_Schema_OutputLog.txt" -b

IF %ERRORLEVEL% == 1 (
ECHO THERE WAS AN ERROR - to file >> D:\home\LogFiles\kudu\deployment\DB_Schema_OutputLog.txt
ECHO THERE WAS AN ERROR - on screen)

SQLCMD -S %APPSETTING_key1% -d cpssodb -U %APPSETTING_key2% -P %APPSETTING_key3% -i "D:\home\site\wwwroot\DBScripts\config_data.sql" -o "D:\home\LogFiles\kudu\deployment\Config_Data_OutputLog.txt" -b
IF %ERRORLEVEL% == 1 (
ECHO THERE WAS AN ERROR - to file >> D:\home\LogFiles\kudu\deployment\Config_Data_OutputLog.txt
ECHO THERE WAS AN ERROR - on screen)

sqlcmd -S %APPSETTING_key1% -d cpssodb -U %APPSETTING_key2% -P %APPSETTING_key3% -Q "UPDATE [dbo].[Configs] SET Value ='%APPSETTING_key7%' WHERE Id=23" -o "D:\home\LogFiles\kudu\deployment\UpdateAllowedReseller_OutputLog.txt" -b

sqlcmd -S %APPSETTING_key1% -d cpssodb -U %APPSETTING_key2% -P %APPSETTING_key3% -Q "UPDATE [dbo].[Configs] SET Value ='%APPSETTING_key8%' WHERE Id=9" -o "D:\home\LogFiles\kudu\deployment\UpdateClientId_OutputLog.txt" -b

sqlcmd -S %APPSETTING_key1% -d cpssodb -U %APPSETTING_key2% -P %APPSETTING_key3% -Q "UPDATE [dbo].[Configs] SET Value ='%APPSETTING_key9%' WHERE Id=7" -o "D:\home\LogFiles\kudu\deployment\UpdateClientSReseller_OutputLog.txt" -b

sqlcmd -S %APPSETTING_key1% -d cpssodb -U %APPSETTING_key2% -P %APPSETTING_key3% -Q "UPDATE [dbo].[Configs] SET Value ='%APPSETTING_key10%' WHERE Id=17" -o "D:\home\LogFiles\kudu\deployment\UpdateResellerId_OutputLog.txt" -b

sqlcmd -S %APPSETTING_key1% -d cpssodb -U %APPSETTING_key2% -P %APPSETTING_key3% -Q "UPDATE [dbo].[Configs] SET Value ='%APPSETTING_key11%' WHERE Id=10" -o "D:\home\LogFiles\kudu\deployment\UpdateSOIN_OutputLog.txt" -b

sqlcmd -S %APPSETTING_key1% -d cpssodb -U %APPSETTING_key2% -P %APPSETTING_key3% -Q "UPDATE [dbo].[Configs] SET Value ='%APPSETTING_key12%' WHERE Id=21" -o "D:\home\LogFiles\kudu\deployment\UpdateRName_OutputLog.txt" -b

SET appURL=https://%WEBSITE_HOSTNAME%

sqlcmd -S %APPSETTING_key1% -d cpssodb -U %APPSETTING_key2% -P %APPSETTING_key3% -Q "UPDATE [dbo].[Configs] SET Value ='%appURL%' WHERE Id=24" -o "D:\home\LogFiles\kudu\deployment\UpdateAppURL_OutputLog.txt" -b