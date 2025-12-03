// For more information see https://aka.ms/fsharp-console-apps
open System
open System.IO

let findLargestJoltage bank =
    let digits = bank |> Seq.map (fun c -> int c - int '0')
    let firstMax = digits |> Seq.take (String.length bank - 1) |> Seq.max
    let indexOfFirstMax = Seq.findIndex (fun x -> x = firstMax) digits
    let secondMax = Seq.skip (indexOfFirstMax + 1) digits |> Seq.max
    // concatenate the two max values as strings and convert to int
    int (string firstMax + string secondMax)

File.ReadAllLines "input.txt"
|> Seq.map findLargestJoltage
|> Seq.sum
|> printfn "Part 1: %d"


let rec find (size: int) (bank: int64 seq) =
    if size = 0 then
        Seq.empty
    else
        let headCount = Seq.length bank - (size - 1)
        let head = Seq.take headCount bank
        let max = Seq.max head
        let maxIndex = Seq.findIndex (fun x -> x = max) head
        seq {
            yield max
            yield! find (size - 1) (Seq.skip (maxIndex+1) bank)
        }


// let input = "811111111111119"
//
// // call find, concat the result
// input
// |> Seq.map (fun c -> int64 c - int64 '0')
// |> find 3
// |> Seq.map string
// |> String.concat ""
// |> int64
// |> printfn "Part 2: %A"


let findLargestJoltage2 bank =
    bank
    |> Seq.map (fun c -> int64 c - int64 '0')
    |> find 12
    |> Seq.map string
    |> String.concat ""
    |> int64

File.ReadAllLines "input.txt"
|> Seq.map findLargestJoltage2
// |> printfn "%A"
|> Seq.sum
|> printfn "Part 2: %d"
