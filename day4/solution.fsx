open System

type Card = { winningNumbers: List<int>; numbers: List<int> }

let input = List.ofSeq(System.IO.File.ReadLines("input.txt"))

let parseNumbers (numbers: string) =
    numbers.Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries) |> List.ofSeq |> List.map int

let parseCard (line: string) =
    let numberPart = line.Split(":")[1]
    let numberParts = numberPart.Split("|")
    let winningNumbers = parseNumbers numberParts[0]
    let numbers = parseNumbers numberParts[1]
    { winningNumbers = winningNumbers; numbers= numbers }

let cards = List.map parseCard input

let getCardPoints (card: Card) =
    card.numbers
    |> List.filter (fun x -> List.contains x card.winningNumbers)
    |> List.length
    |> fun x -> pown 2 (x - 1)

let cardPoints = List.map getCardPoints cards |> List.sum