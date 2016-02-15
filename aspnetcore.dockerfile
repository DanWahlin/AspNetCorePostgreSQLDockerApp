# Build: docker build -f aspnetcore.dockerfile -t danwahlin/aspnetcore .

# Start PostgreSQL (https://hub.docker.com/_/postgres)
# docker run -d --name my-postgres -e POSTGRES_PASSWORD=password postgres

# Start Asp.Net Core
# Run:   docker run -d -p 5000:5000 --link my-postgres:postgres danwahlin/aspnetcore

FROM debian:jessie

MAINTAINER Dan Wahlin

ENV DNX_VERSION 1.0.0-rc2-16357
ENV DNX_USER_HOME /opt/DNX_BRANCH
#Currently the CLR packages don't have runtime ids to handle debian:jessie but
#we are making sure that the dependencies are the right versions and are opting for
#the smaller base image. So we use this variable to overwrite the default detection.
ENV DNX_RUNTIME_ID ubuntu.14.04-x64

# In order to address an issue with running a sqlite3 database on aspnet-docker-linux
# a version of sqlite3 must be installed that is greater than or equal to 3.7.15
# which is not available on the default apt sources list in this image.
# ref: 	https://github.com/aspnet/EntityFramework/issues/3089
# 		https://github.com/aspnet/aspnet-docker/issues/121
RUN printf "deb http://ftp.us.debian.org/debian jessie main\n" >> /etc/apt/sources.list

RUN apt-get -qq update && apt-get -qqy install unzip curl libicu-dev libunwind8 gettext libssl-dev libcurl3-gnutls zlib1g  sqlite3 libsqlite3-dev && rm -rf /var/lib/apt/lists/*

RUN curl -sSL https://raw.githubusercontent.com/aspnet/Home/dev/dnvminstall.sh | DNX_USER_HOME=$DNX_USER_HOME DNX_BRANCH=v$DNX_VERSION sh
RUN bash -c "source $DNX_USER_HOME/dnvm/dnvm.sh \
	&& dnvm install $DNX_VERSION -alias default -r coreclr -u \
	&& dnvm alias default | xargs -i ln -s $DNX_USER_HOME/runtimes/{} $DNX_USER_HOME/runtimes/default"

# Install libuv for Kestrel from source code (binary is not in wheezy and one in jessie is still too old)
# Combining this with the uninstall and purge will save us the space of the build tools in the image
RUN LIBUV_VERSION=1.4.2 \
	&& apt-get -qq update \
	&& apt-get -qqy install autoconf automake build-essential libtool \
	&& curl -sSL https://github.com/libuv/libuv/archive/v${LIBUV_VERSION}.tar.gz | tar zxfv - -C /usr/local/src \
	&& cd /usr/local/src/libuv-$LIBUV_VERSION \
	&& sh autogen.sh && ./configure && make && make install \
	&& rm -rf /usr/local/src/libuv-$LIBUV_VERSION \
	&& ldconfig \
	&& apt-get -y purge autoconf automake build-essential libtool \
	&& apt-get -y autoremove \
	&& apt-get -y clean \
	&& rm -rf /var/lib/apt/lists/*

ENV PATH $PATH:$DNX_USER_HOME/runtimes/default/bin

COPY    . /var/www/AspNetCorePostgreSQLDockerApp
WORKDIR /var/www/AspNetCorePostgreSQLDockerApp

RUN ["dnu", "restore", "--source", "https://nuget.org/api/v2/", "--source", "https://www.myget.org/F/aspnetvnext/", "--source", "https://www.myget.org/F/npgsql-unstable/api/v3/index.json"] 

EXPOSE 5000

ENTRYPOINT ["dnx", "web"]