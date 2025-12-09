module Tests

open System.IO
open Xunit
open Day9.Logic

[<Fact>]
let ``part1 should compute correct sum for sample input`` () =
    let result = File.ReadAllText "test.txt" |> part1
    Assert.Equal(50L, result)

[<Fact>]
let ``part2 should compute correct sum for sample input`` () =
    let result = File.ReadAllText "test.txt" |> part2
    Assert.Equal(24L, result)
