FROM mono:4.4.1.0-onbuild
ADD . /src

RUN curl https://api.nuget.org/downloads/nuget.exe -o nuget.exe
RUN mono nuget.exe install NUnit
RUN mono nuget.exe install NUnit.Runners

