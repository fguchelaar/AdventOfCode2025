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

let brute (lines: string seq) =
    let groups =
        lines
        |> Seq.map _.Trim()
        |> Seq.fold (fun (acc, cur) s -> if s = "" then (cur :: acc, []) else (acc, s :: cur)) ([], [])
        |> fun (acc, cur) -> if List.isEmpty cur then acc else cur :: acc
        |> List.map List.rev

    let eval (group: string list) =
        let op = group.First() |> _.Last() |> string |> useFunc
        let nums = group |> List.map (fun s -> Int64.Parse(s.Trim('+', '*', ' ')))
        perform op nums

    groups |> List.sumBy eval |> int64

let part2 input =
    input |> parse2 |> Seq.map (fun chars -> String(chars.ToArray())) |> brute
