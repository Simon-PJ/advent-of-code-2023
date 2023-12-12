open System

let input = List.ofSeq(System.IO.File.ReadLines("input.txt"))

type HandBidAndScore = { hand: string; bid: int; score: int }

let numberOfMatchingCards (card: char) (hand: string) (includeJokers: bool)=
    Seq.filter (fun x -> card = x || (includeJokers && x = 'J')) hand |> Seq.length

let xOfAKind (hand: string) (x: int) (includeJokers: bool) = 
    let maxOfAKind = Seq.map (fun x -> numberOfMatchingCards x hand includeJokers) hand |> Seq.max
    maxOfAKind = x

let fiveOfAKind (hand: string) =
    xOfAKind hand 5 true

let fourOfAKind (hand: string) =
    xOfAKind hand 4 true

let threeOfAKind (hand: string) =
    xOfAKind hand 3 true

let xOfNumber (hand: string) (number: int) (x: int) (includeJokers: bool) =
    Seq.distinct hand
    |> Seq.map (fun a -> numberOfMatchingCards a hand includeJokers)
    |> Seq.filter (fun a -> a = number)
    |> Seq.length >= x

let xPairs (hand: string) (x: int) (includeJokers: bool) =
    xOfNumber hand 2 x includeJokers

let fullHouse (hand: string) =
    let numJokers = Seq.filter (fun x -> x = 'J') hand |> Seq.length
    if (numJokers = 0) then
        xOfNumber hand 3 1 false && xOfNumber hand 2 1 false
    elif (numJokers = 1) then
        xPairs hand 2 false
    else
        false

let twoPair (hand: string) =
    xPairs hand 2 false

let onePair (hand: string) =
    xPairs hand 1 true

let calculateScore (hand: string) =
    if fiveOfAKind hand then
        700
    elif fourOfAKind hand then
        600
    elif fullHouse hand then
        500
    elif threeOfAKind hand then
        400
    elif twoPair hand then
        300
    elif onePair hand then
        200
    else
        100
    
let parseHandBidAndScore (line: string) =
    let parts = line.Split(" ")
    let score = calculateScore parts[0]
    { hand = parts[0]; bid = int parts[1]; score = score }

let alphabettyHand (hand: string) =
    let pictureMap = Map.empty.Add('T', 'J').Add('J', '.').Add('Q', 'L').Add('K', 'M').Add('A', 'N')
    Seq.map (fun x -> if Char.IsDigit(x) then char (64 + int x - 48) else pictureMap[x]) hand
    |> fun x -> String.Join("", x)

let handsBidsAndScores = List.map parseHandBidAndScore input

let sortedHandsBidsAndScores = List.sortByDescending (fun x -> x.score, (alphabettyHand x.hand)) handsBidsAndScores |> List.rev

let totalWinnings = List.mapi (fun i x -> (i + 1) * x.bid) sortedHandsBidsAndScores |> Seq.sum