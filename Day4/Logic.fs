module Day4.Logic

[<Struct>]
type Point = { X: float; Y: float }

let up point = { point with Y = point.Y - 1.0 }
let down point = { point with Y = point.Y + 1.0 }
let left point = { point with X = point.X - 1.0 }
let right point = { point with X = point.X + 1.0 }

let n8 point =
    [ up point
      up (right point)
      right point
      right (down point)
      down point
      down (left point)
      left point
      left (up point) ]

let parseGrid (input: string) : Map<Point, char> =
    input.Split '\n'
    |> Seq.mapi (fun y line -> line.ToCharArray() |> Seq.mapi (fun x ch -> { X = x; Y = y }, ch))
    |> Seq.concat
    |> Map.ofSeq

let isAccessible point grid =
    (Map.tryFind point grid = Some '@')
    && n8 point |> List.filter (fun p -> Map.tryFind p grid = Some '@') |> List.length < 4

let part1 input =
    let grid = parseGrid input
    grid |> Map.filter (fun p _ -> isAccessible p grid) |> Map.count

let part2 input = -1
