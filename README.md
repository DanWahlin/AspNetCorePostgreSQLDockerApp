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
