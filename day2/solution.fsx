type CubeInfo = { colour: string; count: int }
type Set = List<CubeInfo>
type Game = { id: int; sets: List<Set> }

let input = List.ofSeq(System.IO.File.ReadLines("input.txt"))

let splitAndGetNth (splitter: string) nth (input: string) = (input.Split splitter |> List.ofSeq)[nth]

let parseCubeInfo (input: string) =
    let parts = input.Trim().Split(" ")
    { colour = parts[1]; count = parts[0] |> int }

let parseSet (input: string) =
    let cubeInfo = input.Split(",") |> List.ofSeq
    List.map parseCubeInfo cubeInfo

let parseSets (input: string) =
    input.Split(";") |> List.ofSeq |> List.map parseSet

let parseGame (line: string) = 
    let parsedId = line |> splitAndGetNth ":" 0 |> splitAndGetNth " " 1  |> int
    let parsedSets = line |> splitAndGetNth ":" 1 |> parseSets
    let game = { id = parsedId; sets = parsedSets }
    game

let games = List.map parseGame input

let isValid (set: Set) (colour: string) (max: int) =
    let red = List.tryFind (fun x -> x.colour = colour) set
    match red with
    | Some x -> x.count <= max
    | None -> true

let isValidSet (set: Set) =
    let redValid = isValid set "red" 12
    let greenValid = isValid set "green" 13
    let blueValid = isValid set "blue" 14
    redValid && greenValid && blueValid

let isValidGame (game: Game) =
    List.forall (fun x -> isValidSet x) game.sets

let validGames = games |> List.filter isValidGame

let countInSet (colour: string) (set: Set) = 
    let colour = List.tryFind (fun x -> x.colour = colour) set
    match colour with
    | Some x -> x.count
    | None -> 0

let minOfColour (game: Game) (colour: string) =
    List.map (countInSet colour) game.sets |> List.max

let power (game: Game) =
    let minRed = minOfColour game "red"
    let minGreen = minOfColour game "green"
    let minBlue = minOfColour game "blue"
    minRed * minGreen * minBlue

let sumPowers = List.map power games |> List.sum

let idCountOfValidGames = List.map (fun x -> x.id) validGames |> List.sum