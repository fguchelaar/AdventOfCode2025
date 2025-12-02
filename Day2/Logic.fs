module Day2.Logic

/// Parses a range string in format "start-end" into a list of integers
let parse (s: string) =
    s.Split '-' |> Array.map int64 |> (fun arr -> [ arr[0] .. arr[1] ])

/// Checks if a string has even length
let isEvenLength str =
    str |> String.length |> (fun len -> len % 2 = 0)

/// Splits a string into two equal halves
let split str =
    let mid = String.length str / 2
    str[.. mid - 1], str[mid..]

/// Checks if a number is invalid (even length and first half equals second half)
let isInvalid x =
    let asString = x.ToString()

    isEvenLength asString
    && let firstHalf, secondHalf = split asString in
       firstHalf = secondHalf

/// Checks if a string is composed of a repeating pattern
let isRepeating str =
    let len = String.length str
    let isValidSize size = len % size = 0
    let segment size = str[.. size - 1]

    let isMatch size =
        String.replicate (len / size) (segment size) = str

    seq { 1 .. len / 2 } |> Seq.filter isValidSize |> Seq.exists isMatch

/// Checks if a number is invalid (has a repeating pattern in its string representation)
let isInvalid2 x =
    let asString = x.ToString()
    isRepeating asString

let prepare (input: string) =
    input.Split(',')
    |> Seq.map parse
    |> Seq.toList
    |> List.concat

let part1 (input: string) =
    prepare input
    |> List.filter isInvalid
    |> List.sum

let part2 (input: string) =
    prepare input
    |> List.filter isInvalid2
    |> List.sum
