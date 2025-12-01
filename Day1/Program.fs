open System.IO

type Dir =
    | L
    | R

type Instr = Dir * int

let number_of_positions = 100
let starting_position = 50

let parse (s: string) : Instr =
    let d = if s[0] = 'L' then L else R
    d, int s[1..]

let step pos (dir, n) =
    let delta = if dir = L then -n else n

    (pos + delta) % number_of_positions
    |> fun x -> if x < 0 then x + number_of_positions else x

let countZeros instrs =
    instrs |> List.scan step starting_position |> List.filter ((=) 0) |> List.length


let step2 (pos, _) (dir, n) =
    let delta = if dir = L then -n else n
    let new_pos = pos + delta

    let passed_zeros = if new_pos = 0 then 1 else abs new_pos / number_of_positions

    let passed_zeros =
        if pos > 0 && new_pos < 0 then passed_zeros + 1
        else if pos < 0 && new_pos > 0 then passed_zeros + 1
        else passed_zeros

    new_pos % number_of_positions
    |> fun x -> if x < 0 then x + number_of_positions else x
    |> fun p -> (p, passed_zeros)

let countZeros2 instrs =
    instrs |> List.scan step2 (starting_position, 0) |> List.map snd |> List.sum

let input = "input.txt"

let instructions = File.ReadLines input |> Seq.map parse |> Seq.toList

instructions |> countZeros |> printfn "Part 1: %d"
instructions |> countZeros2 |> printfn "Part 2: %d"
