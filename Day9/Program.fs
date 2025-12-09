open System.IO
open Day9.Logic

let input = File.ReadAllText "input.txt"

input |> part1 |> printfn "Part 1: %d"

input |> part2 |> printfn "Part 2: %d"
