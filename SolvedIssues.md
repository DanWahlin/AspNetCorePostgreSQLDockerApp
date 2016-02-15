## Sporadic Errors Connecting to PostgreSQL

Had to update to the latest build of coreclr due to a bug:

dnvm upgrade -u -r coreclr


## How to run migrations with a specific DbContext

dnx ef migrations add -c DockerCommandsDbContext FunWithDocker

dnx ef migrations database update


## How to override default feed for dnu restore?

Add NuGet.config to root of project with the proper feed URLs:

<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
    <clear />
    <add key="NuGet" value="https://nuget.org/api/v2/" />
    <add key="AspNetVNext" value="https://www.myget.org/F/aspnetvnext/" />
  </packageSources>
</configuration>

Can also update the NuGet.config file at ~/.config/NuGet/NuGet.config


## Where are the NpgSql unstable builds?

https://www.myget.org/gallery/npgsql-unstable

Feed: https://www.myget.org/F/npgsql-unstable/api/v3/index.json

dnu command: dnu install -s https://www.myget.org/F/npgsql-unstable/api/v3/index.json EntityFramework7.npgsql 3.1.0-unstable0415



## How to uninstall a dnx version

dnvm uninstall 1.0.0-rc2-16312 -r coreclr



## How to fix error on Linux:  File name: 'System.Net.NetworkInformation, Version=4.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a' ---> System.IO.FileNotFoundException: Could not load the specified file.

Add "Microsoft.NETCore.Platforms": "1.0.0-rc2-*" to project.json. Missing "runtime.linux.System.Net.NetworkInformation": "4.1.0...." probably otherwise.