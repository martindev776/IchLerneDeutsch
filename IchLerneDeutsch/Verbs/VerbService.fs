module VerbService

open System
open VerbInfo
open Utilities
open SubjectInfo
open System.Collections.Generic

let ConjugateRegularVerb verb subject =   

    let lastLetter = verb.Root.[verb.Root.Length - 1]
    let extraE = match lastLetter with
                 | 'd' | 't' -> "e"
                 | _ -> ""

    let ending = match subject with
                 | Ich -> "e"
                 | Du -> extraE + "st"
                 | Er | Sie | Es | Ihr -> extraE + "t"
                 | Wir | SieFormal | SiePlural -> "en"

    verb.Root + ending

let ConjugateIrregularVerb verb subject =    
    let ConjugateIrregularVerb (irregularVerbDict: IDictionary<int, string>) pronoun =
        let index = pronoun |> MapPronounToInt
        irregularVerbDict.[index]
        
    subject     
    |> (verb.German |> ReadCsvs.ReadIrregularVerbCsv |> ConjugateIrregularVerb)
    

let GetConjugationDisplay verb pronoun =
    let conjugate = verb |> match verb.Type with
                            | VerbType.Regular -> ConjugateRegularVerb
                            | VerbType.Irregular -> ConjugateIrregularVerb
    $"{(pronoun |> MapSubject)} -> {(pronoun |> conjugate)}"

let rec DisplayVerbMenu verbType =
    
    let menuTuples =
        match verbType with
            | VerbType.Regular -> "regularVerbsEndingInEn"
            | VerbType.Irregular -> "irregularVerbs"
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
                                                                  |> MapRegularVerbs verbType
                                                                  |> (fun verb -> AllPronouns |> Array.map (GetConjugationDisplay verb))
                                                                  |> PressAnyKeyToContinuePrintMenu; DisplayVerbMenu verbType
    | "Q" | "q" -> () |> ignore
    | _ -> DisplayVerbMenu verbType

let rec VerbMainMenu() =
    [|
        $"What do you want to do?";
        $"1: Display Regular Verbs";
        $"2: Display Irregular Verbs";
        $"Q: Quit"
    |]
    |> PrintMenu

    let inputValue = Console.ReadLine()

    match inputValue with
    | "1" -> VerbType.Regular |> DisplayVerbMenu |> ignore
    | "2" -> VerbType.Irregular |> DisplayVerbMenu |> ignore
    | "Q" | "q" -> () |> ignore
    | _ -> VerbMainMenu()