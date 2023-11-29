[CmdletBinding()]
$location = Get-Location

Set-Location ../src/Http.API/ClientApp
ng build -c production
scp ./dist/*.* azure:/var/webapi/Dusi/wwwroot

Set-Location $location