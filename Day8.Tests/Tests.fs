module Tests

open System.IO
open Xunit
open Day8.Logic

[<Theory>]
[<InlineData(1, 2, 3, 4, 5, 6, 5.19615242)>]
[<InlineData(0, 0, 0, 1, 1, 1, 1.73205081)>]
[<InlineData(-1, -2, -3, 4, 5, 6, 12.4498996)>]
[<InlineData(4, 5, 6, -1, -2, -3, 12.4498996)>]
let ``Point3D distanceTo should compute correct distance``
    (x1: int, y1: int, z1: int, x2: int, y2: int, z2: int, expected: float)
    =
    let p1 = { X = x1; Y = y1; Z = z1 }
    let p2 = { X = x2; Y = y2; Z = z2 }
    let result = p1.distanceTo (p2)
    Assert.Equal(expected, result, 8)

[<Fact>]
let ``part1 should compute correct sum for sample input`` () =
    let result = File.ReadAllText "test.txt" |> part1 10
    Assert.Equal(40, result)

[<Fact>]
let ``part2 should compute correct sum for sample input`` () =
    let result = File.ReadAllText "test.txt" |> part2
    Assert.Equal(25272L, result)
