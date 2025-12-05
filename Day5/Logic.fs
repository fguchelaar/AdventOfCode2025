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

let merge (ranges: range list) : range list =
    let sorted = List.sortBy _.X ranges
    let rec merge acc remaining =
        match remaining with
        | [] -> List.rev acc
        | r::rs ->
            match acc with
            | [] -> merge [r] rs
            | last::rest ->
                if r.X <= last.Y + 1L then
                    let merged = { X = last.X; Y = max last.Y r.Y }
                    merge (merged::rest) rs
                else
                    merge (r::acc) rs
    merge [] sorted

let part2 (input: string) : int64 =
    let data = parse (input.Trim())
    data.Ranges
    |> merge
    |> List.map (fun r -> r.Y - r.X + 1L)
    |> List.sum
