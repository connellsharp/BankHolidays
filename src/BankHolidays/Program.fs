open System
open Dates

let (<|>) f g x = f x || g x

let isNonWorkDay =
    isBankHoliday <|> isWeekend
    
let isWorkDay date =
    isNonWorkDay date |> not
    
let printDateResults dateStr =
    dateStr
    |> DateTime.Parse
    |> isNonWorkDay
    |> printfn "%s - %b" dateStr
    
[<EntryPoint>]
let main argv =
    [
        "2016-12-26";
        "2016-12-27";
        "2016-12-28";
        "2019-04-19";
        "2019-04-22";
        "2019-04-23";
        "2019-05-05";
        "2019-05-06";
        "2019-05-07";
        "2019-08-25";
        "2019-08-26";
        "2019-08-27";
        "2019-12-25";
        "2019-12-26";
        "2019-12-27";
        "2019-12-28";
        "2019-12-29";
        "2019-12-30";
    ]
    |> Seq.iter printDateResults
    0