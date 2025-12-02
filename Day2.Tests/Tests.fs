module Day2Tests

open System.IO
open Xunit
open Day2.Logic

// Tests for parse function
[<Theory>]
[<InlineData("1-5")>]
[<InlineData("10-12")>]
let ``parse should handle various range formats`` (input: string) =
    let result = parse input
    Assert.NotEmpty(result)

// Tests for isEvenLength
[<Theory>]
[<InlineData("1234", true)>]
[<InlineData("123", false)>]
[<InlineData("", true)>]
[<InlineData("ab", true)>]
let ``isEvenLength should correctly identify even-length strings`` (input: string) (expected: bool) =
    Assert.Equal(expected, isEvenLength input)

// Tests for split
[<Fact>]
let ``split should divide string in half`` () =
    let first, second = split "abcd"
    Assert.Equal("ab", first)
    Assert.Equal("cd", second)

[<Fact>]
let ``split should handle short strings`` () =
    let first, second = split "ab"
    Assert.Equal("a", first)
    Assert.Equal("b", second)

// Tests for isRepeating
[<Theory>]
[<InlineData("abab", true)>]
[<InlineData("ababab", true)>]
[<InlineData("aaa", true)>]
[<InlineData("abcabc", true)>]
[<InlineData("abcd", false)>]
[<InlineData("abcdef", false)>]
[<InlineData("aa", true)>]
let ``isRepeating should identify repeating patterns`` (input: string) (expected: bool) =
    Assert.Equal(expected, isRepeating input)

// Tests for isInvalid (Part 1 logic)
[<Theory>]
[<InlineData(1111L, true)>]
[<InlineData(1234L, false)>]
[<InlineData(2222L, true)>]
[<InlineData(123456L, false)>]
[<InlineData(111111L, true)>]
let ``isInvalid should identify even-length numbers with matching halves`` (input: int64) (expected: bool) =
    Assert.Equal(expected, isInvalid input)

// Tests for isInvalid2 (Part 2 logic)
[<Theory>]
[<InlineData(1111L, true)>]
[<InlineData(1234L, false)>]
[<InlineData(2222L, true)>]
[<InlineData(111111L, true)>]
[<InlineData(121212L, true)>]
[<InlineData(123456L, false)>]
let ``isInvalid2 should identify numbers with repeating patterns`` (input: int64) (expected: bool) =
    Assert.Equal(expected, isInvalid2 input)

[<Fact>]
let ``part1 should compute correct sum for sample input`` () =
    let result = File.ReadAllText "test.txt" |> part1
    Assert.Equal(1227775554L, result)

[<Fact>]
let ``part2 should compute correct sum for sample input`` () =
    let result = File.ReadAllText "test.txt" |> part2
    Assert.Equal(4174379265L, result)
