FROM mono:4.4.1.0-onbuild
ADD . /src
RUN xbuild /src/Trains.sln
WORKDIR /src/Trains.Console/bin/Debug
