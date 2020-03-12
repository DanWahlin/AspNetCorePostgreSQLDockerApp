# ASP.NET Core with PostgreSQL and Docker Demo

Application demo designed to show how ASP.NET Core and PostgreSQL can be run in Docker containers. The app uses Entity Framework to create a simple database that stores Docker commands and examples. It also shows how Angular can be integrated with Web API to display customer information.

### Running the App with Docker Compose

1. Install `Docker Desktop for Mac` or `Docker Desktop for Windows`.

1. Navigate to the `AspNetCorePostgreSQLDockerApp` subfolder in a console window.

1. Open the `Client` folder in a terminal window and run the following commands at the root of the folder (requires Node.js):

    - `npm install`
    - `npm install -g @angular/cli`
    - `ng build`

1. Move back up a level to the `AspNetCorePostgreSQLDockerApp` in the terminal window:

    - Run `docker-compose build`

    - Run `docker-compose up`

1. Navigate to http://localhost:5000 in your browser to view the site.

## Using the Web App for Container Services on Azure

1. Run `docker-compose -f docker-compose.prod.yml build`.
1. Tag the `aspnetcoreapp` image as `[yourDockerHubUserAccount]/aspnetcoreapp`. Make sure you substitute your Docker Hub user account for `[yourDockerHubUserAccount]`.
1. Push the image to Docker Hub using `docker push [yourDockerHubUserAccount]/aspnetcoreapp`.
1. Open `docker-compose azure.yml` file and change the image for the `web` service to `[yourDockerHubUserAccount]/aspnetcoreapp`.
1. Create a new `Web App for Containers` service in Azure. You'll need to add it to a new or existing Resource Group.
1. On the `Docker` tab, switch `Options` to `Docker Compose`, `Image Source` to `Docker Hub` and upload the `docker-compose azure.yml` file using the `Configuration File` section of the screen.
1. Wait for the service to start (it may take a few minutes to pull the image and fire up the Web App Service) and then click the web link it provides in the `Overview` section to hit the app.
