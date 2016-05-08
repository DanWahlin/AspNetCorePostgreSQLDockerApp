# NOTE: Set the DOTNET_VERSION environment variable to the desired version.

# Build the image:
# docker build -t spboyer/dotnet-preview:1.0.0-rc2-002697 .
# docker push 

FROM buildpack-deps:trusty-scm

MAINTAINER Shayne Boyer

# DotNet CLI version
ENV DOTNET_VERSION=1.0.0-preview1-002697

# Work around https://github.com/dotnet/cli/issues/1582 until Docker releases a
# fix (https://github.com/docker/docker/issues/20818). This workaround allows
# the container to be run with the default seccomp Docker settings by avoiding
# the restart_syscall made by LTTng which causes a failed assertion.
ENV LTTNG_UST_REGISTER_TIMEOUT 0

RUN echo "deb [arch=amd64] http://apt-mo.trafficmanager.net/repos/dotnet/ trusty main" > /etc/apt/sources.list.d/dotnetdev.list \
    && apt-key adv --keyserver apt-mo.trafficmanager.net --recv-keys 417A0893 \
    && apt-get update \
    && apt-get install -y --no-install-recommends dotnet-dev-${DOTNET_VERSION} \
    && rm -rf /var/lib/apt/lists/*