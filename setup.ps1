# Setup build environment to use psake
Write-Host 'Trusting PS Gallery'
Set-PSRepository -Name "PSGallery" -InstallationPolicy Trusted

Write-Host 'Installing psake'
Install-Module -Name psake -Scope CurrentUser -Force
