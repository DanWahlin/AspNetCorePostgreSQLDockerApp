# ASP.NET Core with PostgreSQL and Docker Demo

Application demo designed to show how ASP.NET Core and PostgreSQL can be run in Docker containers. 
The app uses Entity Framework to create a simple database that stores Docker commands and examples.

##To run the app with Docker Containers:

###Option 1: Use Docker Compose

1. Install Docker Toolbox (http://docker.com/toolbox).

2. Open the `Docker QuickStart Terminal`. After VirtualBox starts in the terminal navigate to the app's folder.

3. Run `docker-compose build`

4. Run `docker-compose up`

5. Navigate to http://192.168.99.100:5000 in your browser to view the site. This assumes that's the IP assigned to VirtualBox - change if needed.


###Option 2: Manually run Docker commands

1. Install Docker Toolbox (http://docker.com/toolbox).

2. Open the `Docker QuickStart Terminal`. After VirtualBox starts in the terminal navigate to the app's folder.

3. Run the commands listed in `aspnetcore.dockerfile` (see the comments at the top of the file) in the `Docker QuickStart Terminal`.

4. Navigate to http://192.168.99.100:5000 in your browser to view the site. This assumes that's the IP assigned to VirtualBox - change if needed.



