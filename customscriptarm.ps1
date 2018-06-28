Write-Output "inside custom script arm************";

Write-Output "hellorajesh**************";


gci env:* | sort-object name;

Get-ChildItem Env:WEBSITE_SITE_NAME;

Get-AzureRmResourceGroup -Name "$env:WEBSITE_SITE_NAME";
