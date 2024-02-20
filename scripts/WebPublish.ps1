[CmdletBinding()]
$location = Get-Location

Set-Location ../src/Http.API/ClientApp
ng build -c production
scp -r ./dist/browser/** azure:/var/webapi/Dusi/wwwroot

Set-Location $location