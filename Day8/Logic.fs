module Day8.Logic

open System.Linq
open System.Collections.Generic

type Point3D =
    { X: int64
      Y: int64
      Z: int64 }

    static member (+)(a: Point3D, b: Point3D) =
        { X = a.X + b.X
          Y = a.Y + b.Y
          Z = a.Z + b.Z }

    static member (-)(a: Point3D, b: Point3D) =
        { X = a.X - b.X
          Y = a.Y - b.Y
          Z = a.Z - b.Z }

    static member Zero = { X = 0; Y = 0; Z = 0 }

    member this.distanceTo(other: Point3D) =
        let dx = this.X - other.X
        let dy = this.Y - other.Y
        let dz = this.Z - other.Z
        sqrt (float (dx * dx + dy * dy + dz * dz))

let parseLineToPoint (line: string) =
    let parts = line.Trim().Split(',')

    { X = int parts[0]
      Y = int parts[1]
      Z = int parts[2] }

let parseInputToPoints (input: string) =
    input.Trim().Split('\n') |> Seq.map parseLineToPoint

let combinations (points: Point3D seq) =
    let pointList = points |> Seq.toList

    seq {
        for i in 0 .. pointList.Length - 1 do
            for j in i + 1 .. pointList.Length - 1 do
                yield (pointList[i], pointList[j])
    }

let distanceBetweenPoints (p1: Point3D, p2: Point3D) = (p1, p2, p1.distanceTo p2)

let connectPoints (connectCount: int) (pairs: (Point3D * Point3D) seq) =
    let groups = List<HashSet<Point3D>>()

    for p1, p2 in pairs.Take(connectCount) do
        let group1 = groups |> Seq.tryFind _.Contains(p1)
        let group2 = groups |> Seq.tryFind _.Contains(p2)

        match group1, group2 with
        | Some g1, Some g2 when g1 <> g2 ->
            // Merge groups
            g1.UnionWith(g2)
            groups.Remove(g2) |> ignore
        | Some g1, None -> g1.Add(p2) |> ignore
        | None, Some g2 -> g2.Add(p1) |> ignore
        | None, None ->
            let newGroup = HashSet<Point3D>()
            newGroup.Add(p1) |> ignore
            newGroup.Add(p2) |> ignore
            groups.Add(newGroup)
        | Some _, Some _ -> () // both points already in the same group, do nothing

    groups

let part1 (connectCount: int) (input: string) =
    parseInputToPoints input
    |> combinations
    |> Seq.sortBy (fun (p1, p2) -> p1.distanceTo p2)
    |> connectPoints connectCount
    |> Seq.sortByDescending (fun g -> g.Count)
    |> Seq.take 3
    |> Seq.map (fun g -> g.Count)
    |> Seq.fold (*) 1

let connectAllPointsAndReturnLastTwo (total: int) (pairs: (Point3D * Point3D) seq) =
    let groups = List<HashSet<Point3D>>()
    let mutable last1: Point3D = Point3D.Zero
    let mutable last2: Point3D = Point3D.Zero

    pairs
    |> Seq.takeWhile (fun _ -> groups.Count <> 1 || groups[0].Count <> total)
    |> Seq.iter (fun (p1, p2) ->
        let group1 = groups |> Seq.tryFind _.Contains(p1)
        let group2 = groups |> Seq.tryFind _.Contains(p2)
        last1 <- p1
        last2 <- p2

        match group1, group2 with
        | Some g1, Some g2 when g1 <> g2 ->
            // Merge groups
            g1.UnionWith(g2)
            groups.Remove(g2) |> ignore
        | Some g1, None -> g1.Add(p2) |> ignore
        | None, Some g2 -> g2.Add(p1) |> ignore
        | None, None ->
            let newGroup = HashSet<Point3D>()
            newGroup.Add(p1) |> ignore
            newGroup.Add(p2) |> ignore
            groups.Add(newGroup)
        | Some _, Some _ -> () // both points already in the same group, do nothing
    )

    last1, last2

let part2 (input: string) =
    let total = input |> parseInputToPoints |> Seq.length

    parseInputToPoints input
    |> combinations
    |> Seq.sortBy (fun (p1, p2) -> p1.distanceTo p2)
    |> connectAllPointsAndReturnLastTwo total
    |> fun (p1, p2) -> p1.X * p2.X
