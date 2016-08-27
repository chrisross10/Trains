DOCKER_TAG=trains

default: build test

build:
	docker build -t $(DOCKER_TAG) .

test: test-integration test-unit

test-unit:
	docker run \
		$(DOCKER_TAG) \
		mono NUnit.ConsoleRunner.3.4.1/tools/nunit3-console.exe Trains.Tests.Unit.dll

test-integration:
	docker run \
		$(DOCKER_TAG) \
		mono NUnit.ConsoleRunner.3.4.1/tools/nunit3-console.exe Trains.Tests.Integration.dll

open:
	docker run -it $(DOCKER_TAG)
