module Day5.Logic

type range = { X: int64; Y: int64 }
type input = { Ranges: range list; Ids: int64 list }

let rangeOf (s: string) =
    match s.Split '-' with
    | [| x; y |] -> { X = int64 x; Y = int64 y }
    | _ -> invalidArg "s" $"Invalid range: {s}"

let contains range id = range.X <= id && id <= range.Y

let parse (s: string) : input =
    let sections = s.Split("\n\n")
    let ranges = sections.[0].Split '\n' |> Array.map rangeOf |> Array.toList
    let ids = sections.[1].Split '\n' |> Array.map int64 |> Array.toList
    { Ranges = ranges; Ids = ids }

let part1 (input: string) : int =
    let data = parse (input.Trim())
    data.Ids
    |> List.filter (fun id -> data.Ranges |> List.exists (fun range -> contains range id))
    |> List.length

let part2 (input: string) : int = 0
