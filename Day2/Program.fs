open System.IO

let isEvenLength str =
    str |> String.length |> (fun len -> len % 2 = 0)

let split str =
    let mid = String.length str / 2
    str[.. mid - 1], str[mid..]

let isInvalid x =
    let asString = x.ToString()

    isEvenLength asString
    && let firstHalf, secondHalf = split asString in
       firstHalf = secondHalf

let parse (s: string) : int64 list =
    s.Split '-' |> Array.map int64 |> (fun arr -> [ arr[0] .. arr[1] ])

let input = "input.txt"

let ranges =
    (File.ReadAllText input).Split(',')
    |> Seq.map parse
    |> Seq.toList

ranges
|> List.concat
|> List.filter isInvalid
|> List.sum
|> printf "Part 1: %d"
