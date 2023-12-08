open System

let input = List.ofSeq(System.IO.File.ReadLines("input.txt"))

type HandBidAndScore = { hand: string; bid: int; score: int }

let numberOfMatchingCards (card: char) (hand: string)=
    Seq.filter (fun x -> card = x) hand |> Seq.length

let xOfAKind (hand: string) (x: int) = 
    let maxOfAKind = Seq.map (fun x -> numberOfMatchingCards x hand) hand |> Seq.max
    maxOfAKind = x

let fiveOfAKind (hand: string) =
    xOfAKind hand 5

let fourOfAKind (hand: string) =
    xOfAKind hand 4

let threeOfAKind (hand: string) =
    xOfAKind hand 3

let xOfNumber (hand: string) (number: int) (x: int) =
    Seq.distinct hand
    |> Seq.map (fun a -> numberOfMatchingCards a hand)
    |> Seq.filter (fun a -> a = number)
    |> Seq.length = x

let xPairs (hand: string) (x: int) =
    xOfNumber hand 2 x

let fullHouse (hand: string) =
    xOfNumber hand 3 1 && xOfNumber hand 2 1

let twoPair (hand: string) =
    xPairs hand 2

let onePair (hand: string) =
    xPairs hand 1

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
    let pictureMap = Map.empty.Add('T', 'J').Add('J', 'K').Add('Q', 'L').Add('K', 'M').Add('A', 'N')
    Seq.map (fun x -> if Char.IsDigit(x) then char (64 + int x - 48) else pictureMap[x]) hand
    |> fun x -> String.Join("", x)

let handsBidsAndScores = List.map parseHandBidAndScore input

let sortedHandsBidsAndScores = List.sortByDescending (fun x -> x.score, (alphabettyHand x.hand)) handsBidsAndScores |> List.rev

let totalWinnings = List.mapi (fun i x -> (i + 1) * x.bid) sortedHandsBidsAndScores |> Seq.sum