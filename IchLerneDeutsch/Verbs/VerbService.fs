module VerbService

open System
open VerbInfo
open Utilities

type Pronouns =
    | Ich
    | Du
    | ErSieEs
    | Wir
    | Ihr
    | SieSie

let ConjugateVerb word subject =
    
    //Regular verbs only right now
    let ending = match subject with
                 | Ich -> "e"
                 | Du -> "st"
                 | ErSieEs | Ihr -> "t"
                 | Wir | SieSie -> "en"

    word.Root + ending 
    
let GetConjugatedVerbText word =
    let conjugate = word |> ConjugateVerb

    [|
        word.Index.ToString() + " : " + word.German + " / " + word.English;
        "ich -> " + (Ich |> conjugate);
        "du -> " + (Du |> conjugate);
        "er, sie, es -> " + (ErSieEs |> conjugate)
        "wir -> " + (Wir |> conjugate)
        "ihr -> " + (Ihr |> conjugate)
        "sie, Sie -> " + (SieSie |> conjugate)
    |]

let MapRegularVerbs (index, (german, english)) =
    { Index = index
      English = english
      German = german
      Root = german.Substring(0, german.Length - 2)
      Ending = "en"
      Type = VerbType.Regular }

let DisplayVerbs verbType =
    
    match verbType with
          | VerbType.Regular -> "regularVerbsEndingInEn"
    |> ReadCsvs.Read2ColumnCsv
    |> Array.indexed
    |> Array.map (match verbType with
                 | VerbType.Regular -> MapRegularVerbs)
    |> Array.map GetConjugatedVerbText
    |> Array.concat
    |> PressAnyKeyToContinuePrintMenu
    |> ignore

    ignore

let rec VerbMainMenu() =
    [|
        $"What do you want to do?";
        $"1: Display Regular Verbs";
        $"Q: Quit"
    |]
    |> PrintMenu

    let inputValue = Console.ReadLine()

    match inputValue with
    | "1" -> VerbType.Regular |> DisplayVerbs |> ignore
    | "Q" | "q" -> () |> ignore
    | _ -> VerbMainMenu()