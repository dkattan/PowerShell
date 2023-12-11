
Push-Location $PSScriptRoot
Import-Module .\build.psm1 -Force;
Clear-PSRepo;
Start-PSBuild -TypeGen -Runtime wasi-wasm -Detailed


