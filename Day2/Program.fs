open System.IO

let input = "input.txt"
// let input = "test.txt"

let parse (s: string) =
    s.Split '-' |> Array.map int64 |> (fun arr -> [ arr[0] .. arr[1] ])

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

let isRepeating str =
    let len = String.length str

    seq {
        for size in 1 .. len / 2 do
            if len % size = 0 then
                let segment = str[.. size - 1]
                let repetitions = len / size

                if String.replicate repetitions segment = str then
                    yield true
    }
    |> Seq.isEmpty |> not

let isInvalid2 x =
    let asString = x.ToString()
    isRepeating asString

// Parse the input, make a list of all codes in the ranges
let ranges =
    (File.ReadAllText input).Split(',')
    |> Seq.map parse
    |> Seq.toList
    |> List.concat

ranges |> List.filter isInvalid |> List.sum |> printfn "Part 1: %d"

ranges |> List.filter isInvalid2 |> List.sum |> printfn "Part 2: %d"
