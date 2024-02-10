build:
	dotnet build WorkTimeTracker.sln --debug

rebuild:
	dotnet clean WorkTimeTracker.sln
	dotnet restore WorkTimeTracker.sln
	dotnet build WorkTimeTracker.sln --debug

run:
	dotnet run --project Tracker.App/Tracker.App.csproj
