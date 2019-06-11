# Microsoft has a new "Hub" location for images that 
# can be used for ASP.NET Core 2+
# FROM mcr.microsoft.com/dotnet/core/sdk:2.2

FROM microsoft/dotnet:2.2-sdk

LABEL author="Dan Wahlin"

ENV ASPNETCORE_URLS=http://*:5000

WORKDIR /var/www/aspnetcoreapp

COPY . .

EXPOSE 5000

ENTRYPOINT ["/bin/bash", "-c", "dotnet restore && dotnet run"]

# Note that this is only for demo and is intended to keep things simple. 
# A multi-stage dockerfile would normally be used here to build the .dll

# FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
# WORKDIR /app
# EXPOSE 80

# FROM microsoft/dotnet:2.2-sdk AS build
# WORKDIR /src
# COPY AspNetCorePostgreSQLDockerApp.csproj AspNetCorePostgreSQLDockerApp/
# RUN dotnet restore AspNetCorePostgreSQLDockerApp/AspNetCorePostgreSQLDockerApp.csproj
# COPY . .
# WORKDIR /src/AspNetCorePostgreSQLDockerApp
# RUN dotnet build AspNetCorePostgreSQLDockerApp.csproj -c Release -o /app

# FROM build AS publish
# RUN dotnet publish AspNetCorePostgreSQLDockerApp.csproj -c Release -o /app

# FROM base AS final
# WORKDIR /app
# COPY --from=publish /app .
# ENTRYPOINT ["dotnet", "AspNetCorePostgreSQLDockerApp.dll"]







# Build the image:
# docker build -f aspnetcore.production.dockerfile -t [yourDockerHubID]/dotnet:1.0.0
# docker push

# Option 1
# Start PostgreSQL and ASP.NET Core (link ASP.NET core to ProgreSQL container with legacy linking)
 
# docker run -d --name my-postgres -e POSTGRES_PASSWORD=password postgres
# docker run -d -p 5000:5000 --link my-postgres:postgres [yourDockerHubID]/dotnet:1.0.0

# Option 2: Create a custom bridge network and add containers into it

# docker network create --driver bridge isolated_network
# docker run -d --net=isolated_network --name postgres -e POSTGRES_PASSWORD=password postgres
# docker run -d --net=isolated_network --name aspnetcoreapp -p 5000:5000 [yourDockerHubID]/dotnet:1.0.0