[CmdletBinding()]
param (
    [Parameter()]
    [bool]
    $publishAll = $false
)

$location = Get-Location

Set-Location ../src/Http.API
if (Test-Path ./publish) {
    Remove-Item ./publish -Recurse -Force
}
    
# build dotnet api
dotnet publish -c Release -o ./publish

# remove useless files like .pdb and .xml
Remove-Item ./publish/*.pdb -Force
Remove-Item ./publish/*.xml -Force
Remove-Item ./publish/.playwright -Recurse -Force
Remove-Item ./publish/templates -Recurse -Force


if (!$publishAll) {
    $keepFiles = @("appsettings.Production.json",
        "Http.API.dll", "Share.dll", "Entity.dll", "EntityFramework.dll", "Application.dll");

    # create temp folder
    if (Test-Path ./publish-temp) {
        Remove-Item ./publish-temp -Recurse -Force
    }
    New-Item -ItemType Directory -Force -Path ./publish-temp
    # copy files
    foreach ($file in $keepFiles) {
        Copy-Item -Path "./publish/$file" -Destination "./publish-temp/$file" -Force
    }
    # delete all files
    Remove-Item ./publish/* -Recurse -Force

    # copy back
    foreach ($file in $keepFiles) {
        Copy-Item -Path "./publish-temp/$file" -Destination "./publish/$file" -Force
    }

    if (Test-Path ./publish-temp) {
        Remove-Item ./publish-temp -Recurse -Force
    }

}

# copy file via scp
scp -r './publish/*' azure:/var/webapi/Dusi
    
# run command via ssh 
ssh niltor@40.82.150.217 "sudo systemctl restart Dusi.service"

Set-Location $location