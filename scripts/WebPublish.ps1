[CmdletBinding()]
$location = Get-Location

Set-Location ../src/Http.API/ClientApp
ng build -c production
scp ./dist/*.* niltor@40.82.150.217:/var/webapi/Dusi/wwwroot

Set-Location $location