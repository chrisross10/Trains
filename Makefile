DOCKER_TAG=trains

default: build

build:
	docker build -t $(DOCKER_TAG) .

test: test-unit test-integration

test-unit:
	docker run \
		$(DOCKER_TAG) \
		mono NUnit.ConsoleRunner.3.4.1/tools/nunit3-console.exe Trains.Tests.Unit.dll

test-integration:
	docker run \
		$(DOCKER_TAG) \
		mono NUnit.ConsoleRunner.3.4.1/tools/nunit3-console.exe Trains.Tests.Integration.dll
