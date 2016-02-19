# ASP.NET Core with PostgreSQL and Docker Demo

Application demo designed to show how ASP.NET Core and PostgreSQL can be run in Docker containers. 
The app uses Entity Framework to create a simple database that stores Docker commands and examples.

##To run the app with Docker Containers:

###Option 1: Manually run Docker commands

1. Install Docker Toolbox (http://docker.com/toolbox).

2. Open the `Docker QuickStart Terminal`. After VirtualBox starts in the terminal navigate to the app's folder.

3. Run the commands listed in `aspnetcore.dockerfile` (see the comments at the top of the file) in the `Docker QuickStart Terminal`.

4. Navigate to http://192.168.99.100:5000 in your browser to view the site. This assumes that's the IP assigned to VirtualBox - change if needed.

###Option 2: Use Docker Compose

1. Install Docker Toolbox (http://docker.com/toolbox).

2. Open the `Docker QuickStart Terminal`. After VirtualBox starts in the terminal navigate to the app's folder.

3. Run `docker-compose build`.

4. Run `docker-compose up`.

5. Navigate to http://192.168.99.100:5000 in your browser to view the site. This assumes that's the IP assigned to VirtualBox - change if needed.

##To run the app with ASP.NET Core and PostgreSQL (without Docker):

1. Install and start PostgreSQL:

  - Mac:     http://postgresapp.com (easiest way to get it going quickly)

  - Windows: http://www.postgresql.org/download/windows/

2. Open `appsettings.json` and adjust the connection string. Normally it'll look like the following if using the Postgres.app on Mac (follow the Windows installation instructions to get the user name and database name to use):

  - User ID=`<YourUserName>`;Password=;Server=127.0.0.1;Port=5432;Database=`<YourUserName>`;Pooling=true;

3. Follow the instructions on http://docs.asp.net to get `dnvm` installed for your OS.

4. Install ASP.NET Core (currently the app uses version 1.0.0-rc2-16351): 

  - `dnvm upgrade -u -r coreclr`

5. Navigate to the root of the app's folder in a terminal.

6. Run `dnu restore`.

7. Run `dnx web`.

8. Navigate to http://localhost:5000 in your browser.




