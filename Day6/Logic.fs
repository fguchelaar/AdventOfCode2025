module Day6.Logic

let add a b = a + b
let multiply a b = a * b
let perform func (sequence: int64 seq) = sequence |> Seq.reduce func

let useFunc operator =
    match operator with
    | "+" -> add
    | "*" -> multiply
    | _ -> failwith "Unsupported operator"

let eval (sequence: string seq) =
    let length = Seq.length sequence
    let tokens = Seq.take (length - 1) sequence |> Seq.map int64
    let func = useFunc (Seq.last sequence |> string)
    perform func tokens

let parseInput (input: string) =
    input.Split("\n")
    |> Seq.map (fun line -> line.Split(" ", System.StringSplitOptions.RemoveEmptyEntries) |> Seq.toArray)
    |> Seq.transpose

let part1 input =
    input |> parseInput |> Seq.map eval |> Seq.sum

let part2 input = -1
