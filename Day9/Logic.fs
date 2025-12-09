module Day9.Logic

open System

type Point = { X: int64; Y: int64 }

let area (p1: Point) (p2: Point) : int64 =
    abs (p1.X - p2.X + 1L) * abs (p1.Y - p2.Y + 1L)

let parseLineToPoint (line: string) : Point =
    let parts = line.Trim().Split(',')

    { X = int64 parts[0]
      Y = int64 parts[1] }

let parseInputToPoints (input: string) : Point array =
    input.Split([| '\n'; '\r' |], StringSplitOptions.RemoveEmptyEntries)
    |> Array.map parseLineToPoint

let combinations (points: Point array) =
    seq {
        for i in 0 .. points.Length - 2 do
            for j in i + 1 .. points.Length - 1 do
                let p1 = points[i]
                let p2 = points[j]
                yield p1, p2
    }

let part1 (input: string) =
    let points = parseInputToPoints input

    combinations points |> Seq.map (fun (p1, p2) -> area p1 p2) |> Seq.max

let part2 (input: string) =
    let points = parseInputToPoints input
    0
