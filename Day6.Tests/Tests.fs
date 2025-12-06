module Tests

open System.IO
open Xunit
open Day6.Logic

[<Theory>]
[<InlineData(2, 3, 5)>]
[<InlineData(10, 15, 25)>]
let ``add should return correct sum`` a b expected =
    let result = add a b
    Assert.Equal(expected, result)

[<Theory>]
[<InlineData(2, 3, 6)>]
[<InlineData(10, 15, 150)>]
let ``multiply should return correct product`` a b expected =
    let result = multiply a b
    Assert.Equal(expected, result)

[<Theory>]
[<InlineData("1,2,3", 6)>]
[<InlineData("4,5,6", 15)>]
let ``perform with add should return correct result`` (input: string) expected =
    let sequence = input.Split(',') |> Seq.map int
    let result = perform add sequence
    Assert.Equal(expected, result)

[<Theory>]
[<InlineData("2,3,4", 24)>]
[<InlineData("1,5,6", 30)>]
let ``perform with multiply should return correct result`` (input: string) expected =
    let sequence = input.Split(',') |> Seq.map int
    let result = perform multiply sequence
    Assert.Equal(expected, result)

[<Theory>]
[<InlineData("+", 2, 3, 5)>]
[<InlineData("*", 2, 3, 6)>]
let ``useFunc should return correct function`` operator a b expected =
    let func = useFunc operator
    let result = func a b
    Assert.Equal(expected, result)

[<Theory>]
[<InlineData("1,2,3,+", 6)>]
[<InlineData("2,3,4,*", 24)>]
let ``eval should compute correct result`` (input: string) expected =
    let result = eval (input.Split(','))
    Assert.Equal(expected, result)

[<Fact>]
let ``part1 should compute correct sum for sample input`` () =
    let result = File.ReadAllText "test.txt" |> part1
    Assert.Equal(4277556, result)

[<Fact>]
let ``part2 should compute correct sum for sample input`` () =
    let result = File.ReadAllText "test.txt" |> part2
    Assert.Equal(-1, result)
