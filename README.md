# Azureference-App

A reference Azure "cloud native" app for interfacing with Azure infrastructure libraries.

Includes:

_More To Come_

## Setup

### Build

- execute `setup.ps1` to initialize environment
- run `./build.cmd` for subsequent builds

```shell

pwsh ./setup.ps1

./build.cmd
```

### Local Settings

Override default app settings with the [dotnet secret manager](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-3.1&tabs=windows#set-a-secret):

```shell
dotnet user-secrets set "Some:Setting" "MySomeSettingValue" --project .\src\Azureference.Web\
```
