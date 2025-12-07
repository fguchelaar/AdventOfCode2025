module Day7.Logic

open System.Linq

let eval (line: string) (beams: int seq) =
    let numberOfSplitters = beams |> Seq.filter (fun b -> line[b] = '^') |> Seq.length

    let newBeams =
        beams
        |> Seq.collect (fun b ->
            if line[b] = '^' then
                seq {
                    b - 1
                    b + 1
                }
            else
                seq { b })
        |> Seq.distinct

    newBeams, numberOfSplitters

let part1 (input: string) =
    let lines = input.Split('\n', System.StringSplitOptions.RemoveEmptyEntries)
    let initialBeams = [ lines.First().IndexOf('S') ]

    lines.Skip(2)
    |> Seq.fold
        (fun (beams, splitters) line ->
            let newBeams, numberOfSplitters = eval line beams
            newBeams, splitters + numberOfSplitters)
        (initialBeams, 0)
    |> snd


let eval2 (line: string) (beams: (int * int64) seq) =
    let newBeams =
        beams
        |> Seq.collect (fun b ->
            let b, c = b

            if line[b] = '^' then
                seq {
                    b - 1, c
                    b + 1, c
                }
            else
                seq { b, c })

    // group by position and sum counts
    newBeams
    |> Seq.groupBy fst
    |> Seq.map (fun (pos, group) -> pos, group |> Seq.sumBy snd)
    |> Seq.toList

let part2 (input: string) =
    let lines = input.Split('\n', System.StringSplitOptions.RemoveEmptyEntries)
    let initialBeams = [ lines.First().IndexOf('S') |> fun b -> (b, 1L) ]

    // run over all lines and sum counts of beams at the end
    lines.Skip(2)
    |> Seq.fold (fun beams line -> eval2 line beams) initialBeams
    |> Seq.sumBy snd
