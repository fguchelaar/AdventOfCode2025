module Tests

open System.IO
open System.Linq
open Xunit
open Day7.Logic

[<Theory>]
[<InlineData(".....", "1,3", "1,3", 0)>]
[<InlineData(".^...", "1,3", "0,2,3", 1)>]
let ``eval should produce correct beams and number of splitters``
    (line: string, initialBeamsStr: string, expectedBeamsStr: string, expectedSplitters: int)
    =
    let initialBeams = initialBeamsStr.Split(',') |> Seq.map int
    let expectedBeams = expectedBeamsStr.Split(',') |> Seq.map int
    let resultBeams, resultSplitters = eval line initialBeams
    Assert.Equal(expectedSplitters, resultSplitters)
    Assert.Equal(expectedBeams.ToArray(), resultBeams)

[<Fact>]
let ``part1 should compute correct sum for sample input`` () =
    let result = File.ReadAllText "test.txt" |> part1
    Assert.Equal(21, result)

[<Fact>]
let ``part2 should compute correct sum for sample input`` () =
    let result = File.ReadAllText "test.txt" |> part2
    Assert.Equal(40, result)
