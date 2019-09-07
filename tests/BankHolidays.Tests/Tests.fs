module Tests

open System
open Xunit
open Dates

[<Theory>]
[<InlineData("1700-04-09")>]
[<InlineData("1800-04-11")>]
[<InlineData("1937-03-26")>]
[<InlineData("2019-04-19")>]
[<InlineData("2020-04-10")>]
[<InlineData("2021-04-02")>]
[<InlineData("2022-04-15")>]
[<InlineData("2023-04-07")>]
let ``Good Friday is bank holiday`` strDate =
    let b = isBankHoliday (DateTime.Parse strDate)
    Assert.True(b)
