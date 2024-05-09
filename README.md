<div align="center">

# Refleter Cloud Key Vault

<br />

[![license](https://img.shields.io/badge/license-Apache%20V2-blue)](LICENSE)

<br />

</div>

## Getting Started

### Prerequisites

- Clone the repository: https://github.com/mishakrpv/refleter
- (Windows only) Install Visual Studio. Visual Studio contains tooling support for .NET Aspire that you will want to have. [Visual Studio 2022 version 17.10 Preview](https://visualstudio.microsoft.com/vs/preview/).
    - During installation, ensure that the following are selected:
        - `ASP.NET and web development` workload.
        - `.NET Aspire SDK` component in `Individual components`.
- Install the latest [.NET 8 SDK](https://github.com/dotnet/installer#installers-and-binaries)
- On Mac/Linux (or if not using Visual Studio), install the Aspire workload with the following commands:
```powershell
dotnet workload update
dotnet workload install aspire
dotnet restore Refleter.sln
```
- Install & start Docker Desktop:  https://docs.docker.com/engine/install/

### Running the solution

> [!WARNING]
> Remember to ensure that Docker is started

* (Windows only) Run the application from Visual Studio:
- Open the `Refleter.sln` file in Visual Studio
- Ensure that `Refleter.AppHost.csproj` is your startup project
- Hit Ctrl-F5 to launch Aspire

* Or run the application from your terminal:
```powershell
dotnet run --project src/Refleter.AppHost/Refleter.AppHost.csproj
```
then look for lines like this in the console output in order to find the URL to open the Aspire dashboard:
```sh
Now listening on: http://localhost:15070
```
