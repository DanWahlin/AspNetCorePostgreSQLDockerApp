## How to override default feed for dnu restore?

Add NuGet.config to root of project with the proper feed URLs:

<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
    <clear />
    <add key="AspNetCI" value="https://www.myget.org/F/aspnetcirelease/api/v3/index.json" />
    <add key="NuGet.org" value="https://api.nuget.org/v3/index.json" />
    <add key="NpgsqlUnstable" value="https://www.myget.org/F/npgsql-unstable/api/v3/index.json" />
  </packageSources>
</configuration>

Can also update the NuGet.config file at ~/.config/NuGet/NuGet.config


## Where are the NpgSql unstable builds?

https://www.myget.org/gallery/npgsql-unstable

Feed: https://www.myget.org/F/npgsql-unstable/api/v3/index.json

dnu command: dnu install -s https://www.myget.org/F/npgsql-unstable/api/v3/index.json EntityFramework7.npgsql 3.1.0-unstable0415

