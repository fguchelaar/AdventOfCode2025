module Tests

open System.IO
open Xunit
open Day5.Logic

[<Theory>]
[<InlineData("1-2", 1, 2)>]
[<InlineData("10-20", 10, 20)>]
[<InlineData("0-0", 0, 0)>]
[<InlineData("5-15", 5, 15)>]
let ``rangeOf should parse range correctly`` (input: string, x: int, y: int) =
    let result = rangeOf input
    Assert.Equal({ X = x; Y = y }, result)

[<Theory>]
[<InlineData(5, "1-10", true)>]
[<InlineData(15, "10-20", true)>]
[<InlineData(25, "10-20", false)>]
[<InlineData(0, "0-0", true)>]
[<InlineData(1, "0-0", false)>]
let ``contains should determine if id is within range`` (id: int, rangeStr: string, expected: bool) =
    let range = rangeOf rangeStr
    let result = contains range id
    Assert.Equal(expected, result)

[<Fact>]
let ``part1 should compute correct sum for sample input`` () =
    let result = File.ReadAllText "test.txt" |> part1
    Assert.Equal(3, result)

[<Fact>]
let ``part2 should compute correct sum for sample input`` () =
    let result = File.ReadAllText "test.txt" |> part2
    Assert.Equal(-1, result)
