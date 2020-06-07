FROM mcr.microsoft.com/dotnet/core/sdk

LABEL author="Dan Wahlin"

ENV ASPNETCORE_URLS=http://+:5000

WORKDIR /var/www/aspnetcoreapp

COPY . .

EXPOSE 5000

ENTRYPOINT ["/bin/bash", "-c", "dotnet restore && dotnet run"]

# Note that this is only for demo and is intended to keep things simple. 
# A multi-stage dockerfile would normally be used here to build the .dll and use
# the mcr.microsoft.com/dotnet/core/aspnet image for the final image

# Legacy linking commands. While these work, they aren't the preferred way now.
# Instead, use networks (see the docker-compose.yml file for an example).

# docker build -f aspnetcore.dockerfile -t danwahlin/aspnetcore .
# docker run -d --name my-postgres -e POSTGRES_PASSWORD=password postgres
# docker run -d -p 5000:5000 --link my-postgres:postgres danwahlin/aspnetcore