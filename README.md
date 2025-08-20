# Live Football World Cup Score Board

Live Football World Cup Score Board is a simple .net 8 library to manage matches at the football World Cup.
It enables adding a match to the scoreboard, updating match score, removing finished match from scoreboard and getting summary of all live matches 
ordered by total score and time started as a tie breaker.

## Features

- **Start Match** – Begin a new match with an initial score of `0 - 0`.  
- **Update Score** – Update a match with absolute home and away scores.  
- **Finish Match** – Remove a match from the scoreboard.  
- **Summary View** – Get all matches in progress ordered by total score (descending). Matches with the same total score are ordered by most recently started match.  

# Getting started 

## Prerequisites

- .NET 8 SDK

## Build the Project
```bash
dotnet build
```

## Run tests
```bash
dotnet test
```

Or use VisualStudio: Right click on Scoreboard.Test project and then click Run Tests

# Usage

```csharp
var scoreboard = new Scoreboard();

/* Start a new match
   If team names are empty or null ArgumentException is thrown
   If home team is the same as away team ArgumentException is thrown */
var matchId = scoreboard.StartMatch("Slovenia", "Croatia");

/* Update score
   If score is negative ArgumentException is thrown */
if (matchId.HasValue) scoreboard.UpdateScore(matchId.Value, 1, 0);

// Get live summary
var summary = scoreboard.GetSummary();
foreach (var match in summary)
{
    Console.WriteLine($"{match.HomeTeam} {match.HomeScore} - {match.AwayScore} {match.AwayTeam}");
}

// 4. Finish match
if (matchId.HasValue) scoreboard.FinishMatch(matchId.Value);
```

# Notes
Unit tests are written using xUnit.
For sorting and searching in list LINQ was used, because it is cleaner and more readable. It's slightly lesser performance has no impact on a data this small as world cup live matches