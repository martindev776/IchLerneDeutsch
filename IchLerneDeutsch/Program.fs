open System
open NounService
open VerbService
open Utilities

[<EntryPoint>]
let main argv =

    let rec MenuLoop() =
    
        [|
            $"Please make a selection:";
            $"1: Nouns";
            $"2: Verbs";
            $"Q: Quit"
        |]
        |> PrintMenu

        let inputValue = Console.ReadLine()

        match inputValue with
        | "1" -> NounMainMenu(); MenuLoop()
        | "2" -> VerbMainMenu(); MenuLoop()
        | "Q" | "q" -> () |> ignore
        | _ -> MenuLoop()

    MenuLoop()

    0