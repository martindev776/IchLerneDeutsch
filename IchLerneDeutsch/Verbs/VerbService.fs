module VerbService

open System
open VerbInfo
open Utilities
open SubjectInfo

let ConjugateVerb word subject =   

    let lastLetter = word.Root.[word.Root.Length - 1]
    let extraE = match lastLetter with
                 | 'd' | 't' -> "e"
                 | _ -> ""

    //Regular verbs only right now
    let ending = match subject with
                 | Ich -> "e"
                 | Du -> extraE + "st"
                 | Er | SieInformal | Es | Ihr -> extraE + "t"
                 | Wir | SieFormal | SiePlural -> "en"

    word.Root + ending 

let GetConjugationDisplay verb pronoun =
    let conjugate = verb |> ConjugateVerb
    $"{(pronoun |> MapSubject)} -> {(pronoun |> conjugate)}"

let MapRegularVerbs (german, english) =
    { English = english
      German = german
      Root = german.Substring(0, german.Length - 2)
      Ending = "en"
      Type = VerbType.Regular }

let rec DisplayVerbMenu verbType =
    
    let menuTuples =
        match verbType with
            | VerbType.Regular -> "regularVerbsEndingInEn"
        |> ReadCsvs.Read2ColumnCsv
        |> Array.indexed
        |> Array.map (fun (index, (german, english)) -> (index + 1, german, english))
    
    menuTuples 
    |> Array.map (fun (index, german, english) -> ($"{index}: {german} / {english}"))
    |> (fun x -> [|[|$"Pick a verb to conjugate"|];
                   x;
                   [|$"Q: Quit"|]|])
    |> Array.concat
    |> PrintMenu

    let menuSelections = 
        menuTuples 
        |> Array.map (fun (index, _, _) -> index |> string)

    let inputValue = Console.ReadLine()

    let menuDict = menuTuples |> Array.map (fun (index, german, english) -> (index, (german, english))) |> dict

    match inputValue with
    | inputValue when Array.contains inputValue menuSelections -> menuDict.[inputValue |> int]                                                                   
                                                                  |> MapRegularVerbs
                                                                  |> (fun verb -> AllPronouns |> Array.map (GetConjugationDisplay verb))
                                                                  |> PressAnyKeyToContinuePrintMenu; DisplayVerbMenu verbType
    | "Q" | "q" -> () |> ignore
    | _ -> DisplayVerbMenu verbType

let rec VerbMainMenu() =
    [|
        $"What do you want to do?";
        $"1: Display Regular Verbs";        
        $"Q: Quit"
    |]
    |> PrintMenu

    let inputValue = Console.ReadLine()

    match inputValue with
    | "1" -> VerbType.Regular |> DisplayVerbMenu |> ignore
    | "Q" | "q" -> () |> ignore
    | _ -> VerbMainMenu()