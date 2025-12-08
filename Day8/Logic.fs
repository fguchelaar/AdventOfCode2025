module Day8.Logic

open System

type Point3D =
    { X: int64
      Y: int64
      Z: int64 }

    static member Zero = { X = 0L; Y = 0L; Z = 0L }

let distance (p1: Point3D) (p2: Point3D) =
    let dx = p1.X - p2.X
    let dy = p1.Y - p2.Y
    let dz = p1.Z - p2.Z
    sqrt (float (dx * dx + dy * dy + dz * dz))

let parseLineToPoint (line: string) : Point3D =
    let parts = line.Trim().Split(',')

    { X = int64 parts[0]
      Y = int64 parts[1]
      Z = int64 parts[2] }

let parseInputToPoints (input: string) : Point3D array =
    input.Split([| '\n'; '\r' |], StringSplitOptions.RemoveEmptyEntries)
    |> Array.map parseLineToPoint

/// All unique unordered pairs (i < j)
let combinations (points: Point3D array) =
    seq {
        for i in 0 .. points.Length - 2 do
            for j in i + 1 .. points.Length - 1 do
                let p1 = points[i]
                let p2 = points[j]
                yield p1, p2
    }

/// Step function: integrate a pair (p1, p2) into the current list of disjoint groups.
let private stepGroups (p1: Point3D) (p2: Point3D) (groups: Set<Point3D> list) : Set<Point3D> list =
    // Partition groups into: those containing p1, those containing p2, and the rest
    let g1, others1 = groups |> List.partition (Set.contains p1)
    let g2, others2 = others1 |> List.partition (Set.contains p2)

    match g1, g2 with
    | [], [] ->
        // Neither point is in any group yet → create a new group
        Set.ofList [ p1; p2 ] :: groups

    | g1 :: _, [] ->
        // p1 in g1, p2 in no group → add p2 to g1
        Set.add p2 g1 :: others1

    | [], g2 :: _ ->
        // p2 in g2, p1 in no group → add p1 to g2
        Set.add p1 g2 :: others2

    | g1 :: _, g2 :: _ when g1 = g2 ->
        // Both points already in the same group → nothing changes
        groups

    | g1 :: _, g2 :: _ ->
        // Points in different groups → merge
        Set.union g1 g2 :: others2

/// Build groups by taking the first `connectCount` pairs (in whatever order they are given).
let connectPoints (connectCount: int) (pairs: (Point3D * Point3D) seq) =
    pairs
    |> Seq.truncate connectCount
    |> Seq.fold (fun groups (p1, p2) -> stepGroups p1 p2 groups) []
    |> List.toSeq

let part1 (connectCount: int) (input: string) =
    let points = parseInputToPoints input

    combinations points
    |> Seq.sortBy (fun (p1, p2) -> distance p1 p2)
    |> connectPoints connectCount
    |> Seq.sortByDescending (fun g -> g |> Set.count)
    |> Seq.truncate 3
    |> Seq.map Set.count
    |> Seq.fold (*) 1

/// Recursively connect points until we have a single group of size `total`,
/// returning the last pair that was used.
let rec private connectUntilSingleGroup
    (total: int)
    (groups: Set<Point3D> list)
    (last1: Point3D, last2: Point3D)
    (pairs: (Point3D * Point3D) list)
    : Point3D * Point3D =

    match groups with
    | [ g ] when Set.count g = total ->
        // Fully connected
        last1, last2

    | _ ->
        match pairs with
        | [] ->
            // No more pairs; just return whatever last was (shouldn't happen for valid input)
            last1, last2
        | (p1, p2) :: rest ->
            let newGroups = stepGroups p1 p2 groups
            connectUntilSingleGroup total newGroups (p1, p2) rest

let part2 (input: string) =
    let points = parseInputToPoints input
    let total = points.Length

    let sortedPairs =
        combinations points |> Seq.sortBy (fun (p1, p2) -> distance p1 p2) |> Seq.toList

    let p1, p2 =
        connectUntilSingleGroup total [] (Point3D.Zero, Point3D.Zero) sortedPairs

    p1.X * p2.X
