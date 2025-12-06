module Day6.Logic

open System
open System.Linq

let add a b = a + b
let multiply a b = a * b
let perform func (sequence: int64 seq) = sequence |> Seq.reduce func

let useFunc (operator: string) =
    match operator.Trim() with
    | "+" -> add
    | "*" -> multiply
    | _ -> failwith $"Unsupported operator: '%s{operator}'"

let eval (sequence: string seq) =
    let length = Seq.length sequence
    let tokens = Seq.take (length - 1) sequence |> Seq.map int64
    let func = useFunc (Seq.last sequence |> string)
    perform func tokens

let parseInput (input: string) =
    input.Split("\n", StringSplitOptions.RemoveEmptyEntries)
    |> Seq.map (fun line -> line.Split(' ', StringSplitOptions.RemoveEmptyEntries))
    |> Seq.toArray
    |> Seq.transpose

let part1 input =
    input |> parseInput |> Seq.map eval |> Seq.sum

let parse2 (input: string) =
    input.Split("\n", StringSplitOptions.RemoveEmptyEntries)
    |> Seq.map _.ToCharArray()
    |> Seq.transpose

// take the operator of the first line. Then apply it to all lines, until a blank line is found. Then repeat.
let brute (lines: string seq) : int64 =
    let mutable total = 0L
    let mutable func = add
    let mutable currentNumbers = ResizeArray<int64>()

    // add an extra empty line to force final computation
    let lines = Seq.append lines (seq { yield " " })

    for line in lines do
        let operator = line.Last()

        if operator = '+' || operator = '*' then
            func <- useFunc (operator.ToString())
            currentNumbers.Clear()

        if line.Trim().Length = 0 then
            let currentTotal = perform func currentNumbers
            total <- total + currentTotal
        else
            let number = Int64.Parse(line.Trim('+', '*', ' '))
            currentNumbers.Add(number)

    total

let part2 input =
    input |> parse2 |> Seq.map (fun chars -> String(chars.ToArray())) |> brute
