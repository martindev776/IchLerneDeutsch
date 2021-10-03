module Utilities

open System

let WriteLine (item: string) = 
    Console.WriteLine(item) |> ignore

let PrintItems items =
    items 
    |> Array.map WriteLine
    |> ignore

let PrintMenu items =    
    Console.Clear()
    items
    |> PrintItems
    

let PressAnyKeyToContinuePrintMenu items =
    Console.Clear()
    items 
    |> PrintItems
    Console.WriteLine("Press any key to continue...")
    Console.ReadKey() |> ignore
    Console.Clear()|> ignore

let GetRandomNumberBetween first second =
    let second' = second + 1
    let randomNumb = System.Random().Next() % (second' - first)
    randomNumb + first

