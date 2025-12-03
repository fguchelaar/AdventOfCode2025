open System.IO

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
            yield! find (size - 1) (Seq.skip (maxIndex + 1) bank)
        }

let findLargestJoltage size bank =
    bank
    |> Seq.map (fun c -> int64 c - int64 '0')
    |> find size
    |> Seq.map string
    |> String.concat ""
    |> int64

File.ReadAllLines "input.txt"
|> Seq.map (findLargestJoltage 2)
|> Seq.sum
|> printfn "Part 1: %d"

File.ReadAllLines "input.txt"
|> Seq.map (findLargestJoltage 12)
|> Seq.sum
|> printfn "Part 2: %d"
