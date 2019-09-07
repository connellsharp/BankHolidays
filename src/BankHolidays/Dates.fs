module Dates

open System

let date year month day = 
    new DateTime(year, month, day)
    
let isDayOfWeek daysOfWeek (date:DateTime) =
    daysOfWeek |> Seq.contains date.DayOfWeek

let isWeekend =
    isDayOfWeek [ DayOfWeek.Saturday; DayOfWeek.Sunday ]
    
let easter year = // https://techneilogy.blogspot.com/2010/04/easter.html
    let (/%) l r = (l/r),(l%r)
    let a     = year%19
    let (b,c) = year/%100
    let (d,e) = b/%4
    let f     = (b+8)/25
    let g     = (b-f+1)/3
    let h     = (19*a+b-d-g+15)%30
    let (i,k) = c/%4
    let l     = (32+2*e+2*i-h-k)%7
    let m     = (a+11*h+22*l)/451
    let (n,p) = (h+l+7*m+114)/%31
    date year n (p+1)
    
let addDays dayCount (date:DateTime) =
    date.AddDays (float dayCount)
    
let rec skipUntil increment predicate (date:DateTime) =
    if predicate date then 
        date
    else 
        skipUntil increment predicate (addDays increment date)
    
let skipUntilMonday = skipUntil 1 (isDayOfWeek [ DayOfWeek.Monday ])
    
let isNotWeekend date = not (isWeekend date)
let skipUntilWeekday = skipUntil 1 isNotWeekend
let skip2UntilWeekday = skipUntil 2 isNotWeekend
 
let getBankHolidays year =
    [
        date year 1 1 |> skipUntilWeekday;
        easter year |> addDays -2;
        easter year |> addDays 1;
        date year 5 1 |> skipUntilMonday;
        date year 5 25 |> skipUntilMonday;
        date year 8 25 |> skipUntilMonday;
        date year 12 25 |> skip2UntilWeekday;
        date year 12 26 |> skip2UntilWeekday;
    ]
    
let isBankHoliday (date:DateTime) =
    getBankHolidays date.Year |> Seq.contains date