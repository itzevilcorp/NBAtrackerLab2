type Coach = {
    Name: string
    Team: string
}

type Stats = {
    GP: int
    Won: int
    Lost: int
    Pct: float
    Division: string
}

type Team = {
    Name: string
    Coach: Coach
    Stats: Stats
}

let coaches = [
    { Name = "Adrian Griffin"; Team = "Milwaukee Bucks" }
    { Name = "Chris Finch"; Team = "Minnesota Timberwolves" }
    { Name = "Willie Green"; Team = "New Orleans Pelicans" }
    { Name = "Tom Thibodeau"; Team = "New York Knicks" }
    { Name = "Mark Daigneault"; Team = "Oklahoma City Thunder" }
]

let teamStats = [
    { GP = 4443; Won = 2340; Lost = 2103; Pct = 0.527; Division = "Central" }
    { GP = 4524; Won = 2413; Lost = 2111; Pct = 0.533; Division = "Northwest" }
    //lost
    { GP = 2712; Won = 1091; Lost = 1621; Pct = 0.402; Division = "Northwest" }
    { GP = 1686; Won = 782; Lost = 904; Pct = 0.464; Division = "Southwest" }
    { GP = 6023; Won = 2924; Lost = 3099; Pct = 0.485; Division = "Atlantic" }
    //lost
]

let teams =
    List.map2 (fun coach stats -> { Name = stats.Division; Coach = coach; Stats = stats }) coaches teamStats

let winningTeams =
    teams
    |> List.filter (fun team -> team.Stats.Won > team.Stats.Lost)

// winning percentage 
let winningPercentage team =
    float team.Stats.Won / float (team.Stats.Won + team.Stats.Lost) * 100.0

let winningPercentages =
    winningTeams
    |> List.map winningPercentage

let losingPercentages =
    teams
    |> List.filter (fun team -> not (List.exists (fun st -> st = team) winningTeams))
    |> List.map (fun team -> 100.0 - winningPercentage team)

printfn "Teams:"
printfn ""
teams |> List.iter (fun team -> printfn "%s coached by %s" team.Coach.Team team.Coach.Name)
printfn ""

printfn "Game Wons and Loss:"
printfn ""
teams |> List.iter (fun team -> printfn "%s - Games Won: %d, Games Lost: %d" team.Coach.Team team.Stats.Won team.Stats.Lost)
printfn ""

printfn "Teams:"
teams |> List.iter (fun team ->
    if List.exists (fun st -> st = team) winningTeams then
        printfn "WIN - %s coached by %s" team.Coach.Team team.Coach.Name
    else
        printfn "LOST - %s coached by %s" team.Coach.Team team.Coach.Name)
printfn ""

printfn "Percentage"
printfn ""
List.iteri (fun i percentage -> printfn "%s: %.2f%%" (winningTeams.[i].Coach.Team) percentage) winningPercentages
List.iteri (fun i percentage -> printfn "%s: %.2f%%" (teams.[i].Coach.Team) percentage) losingPercentages
printfn ""
