FROM mcr.microsoft.com/dotnet/core/sdk AS build
WORKDIR /var/www/aspnetcoreapp

# copy csproj and restore as distinct layers
COPY ./*.csproj ./
RUN dotnet restore

# copy everything else and build app
COPY ./ ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet
ENV ASPNETCORE_URLS=http://+:5000
WORKDIR /var/www/aspnetcoreapp
COPY --from=build /var/www/aspnetcoreapp/out ./
ENTRYPOINT ["dotnet", "AspNetCorePostgreSQLDockerApp.dll"]



