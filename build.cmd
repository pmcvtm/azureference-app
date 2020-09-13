@echo off

pwsh -NoProfile -ExecutionPolicy Bypass -Command "invoke-psake %* -properties @{"version"="'%2'"}; if ($psake.build_success -eq $false) { exit 1 } else { exit 0 }"