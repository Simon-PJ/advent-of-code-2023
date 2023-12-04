open System

type Card = { id: int; winningNumbers: List<int>; numbers: List<int>; instances: int }

let input = List.ofSeq(System.IO.File.ReadLines("input.txt"))

let parseNumbers (numbers: string) =
    numbers.Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries) |> List.ofSeq |> List.map int

let parseCard (line: string) =
    let id = (line.Split(":")[0]).Split(" ", StringSplitOptions.RemoveEmptyEntries)[1] |> int
    let numberPart = line.Split(":")[1]
    let numberParts = numberPart.Split("|")
    let winningNumbers = parseNumbers numberParts[0]
    let numbers = parseNumbers numberParts[1]
    { id = id; winningNumbers = winningNumbers; numbers = numbers; instances = 1 }

let cards = List.map parseCard input

let matchingNumbers (card: Card) =
    card.numbers
    |> List.filter (fun x -> List.contains x card.winningNumbers)
    |> List.length

let getCardPoints (card: Card) =
    card
    |> matchingNumbers
    |> fun x -> pown 2 (x - 1)

let cardPoints = List.map getCardPoints cards |> List.sum

let getUpdatedCard (startNumber: int) (matchCount: int) (instances: int) (card: Card) =
    if card.id > startNumber && card.id <= startNumber + matchCount
    then { card with instances = card.instances + instances }
    else card
    
let rec task2 (cards: List<Card>) =
    if List.length cards = 1 then
        cards
    else
        let card = List.head cards
        let matchCount = matchingNumbers card
        let restOfDeck = cards |> List.map (getUpdatedCard card.id matchCount card.instances) |> List.skip 1 |> task2
        List.append [card] restOfDeck

let cardCount = cards |> task2 |> List.map (fun x -> x.instances) |> List.sum