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

let accessiblePoints grid =
    grid |> Map.filter (fun p _ -> isAccessible p grid) |> Map.toSeq |> Seq.map fst

let part1 input =
    parseGrid input |> accessiblePoints |> Seq.length

let allAccessiblePoints grid =
    let rec loop removed =
        // Remove already known removed points
        let cleanedGrid = grid |> Map.filter (fun p _ -> not (Set.contains p removed))

        let toRemove = accessiblePoints cleanedGrid |> Set.ofSeq // convert once

        if Set.isEmpty toRemove then
            removed
        else
            Set.union removed toRemove |> loop

    accessiblePoints grid |> Set.ofSeq |> loop

let part2 input =
    let grid = parseGrid input
    allAccessiblePoints grid |> Seq.length
