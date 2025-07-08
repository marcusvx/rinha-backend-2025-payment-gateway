.PHONY: run test build clean

APP_PROJECT = PaymentMediator/PaymentMediator.csproj
TEST_PROJECT = PaymentMediator.Tests/PaymentMediator.Tests.csproj

run:
	@echo "Running the application..."
	@dotnet run --project $(APP_PROJECT)

test:
	@echo "Running tests..."
	@dotnet run --project $(TEST_PROJECT)

build:
	@echo "Building projects..."
	@dotnet build

clean:
	@echo "Cleaning projects..."
	@dotnet clean
