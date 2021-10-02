module NounService

open System
open NounInfo
open Utilities

let GetDefiniteArticle gender =
    match gender with
    | Masculine -> "der"
    | Feminine -> "die"
    | Neuter -> "das"

let GetPrintNounText word =
    let article = word.Gender |> GetDefiniteArticle
    article + " " + word.German + " / " + "the " + word.English

let PrintNoun word =
    let text = word |> GetPrintNounText
    Console.WriteLine(text)

let masculineFileName = "masculineNouns"
let feminineFileName = "feminineNouns"
let neuterFileName = "neuterNouns"

let GetNouns gender =    
    match gender with
        | Gender.Masculine -> masculineFileName
        | Gender.Feminine -> feminineFileName
        | Gender.Neuter -> neuterFileName
    |> ReadCsvs.Read2ColumnCsv
    |> Array.map (fun (german, english) -> { English = english; German = german; Gender = gender })

let GetAllNouns() =
    let masculineNouns = Gender.Masculine |> GetNouns
    let feminineNouns = Gender.Feminine |> GetNouns
    let neuterNouns = Gender.Neuter |> GetNouns

    [|masculineNouns;feminineNouns;neuterNouns|]
    |> Array.concat

let rec PlayTheDefiniteArticleGame() =

    let rec PlayGame() =

        let randomNumber = System.Random()
        let nouns = GetAllNouns()                     
        let shuffledNouns = nouns |> Array.sortBy (fun _ -> randomNumber.Next(0, nouns.Length - 1))        

        let randomNoun = nouns.Length - 1
                         |> GetRandomNumberBetween 0
                         
                         |> (fun x -> shuffledNouns.[x])   

        [|
            $"Pick the correct article for: {randomNoun.German}";
            $"1: der";
            $"2: die";
            $"3: das";
            $"Q: Quit"
        |]
        |> PrintMenu

        let CheckIfArticleIsCorrect noun chosenArticle =
            let nounsArticle = noun.Gender |> GetDefiniteArticle

            let mappedChosenArticle = match chosenArticle with
                                      | "1" -> "der"
                                      | "2" -> "die"
                                      | "3" -> "das"                                      

            let isSame = mappedChosenArticle = nounsArticle            
            
            [|
                "";
                "-----------------------";
                match isSame with
                | true -> "Correct!"
                | false -> "Wrong!";                
                noun |> GetPrintNounText;
                "-----------------------";
                ""
            |]            
            |> PressAnyKeyToContinuePrintMenu
            |> ignore            
        
        let rec GetInput() =
            let inputValue = Console.ReadLine()    

            match inputValue with
            | "1" | "2" | "3" -> inputValue |> CheckIfArticleIsCorrect randomNoun; () |> PlayGame
            | "q" | "Q" -> () |> ignore
            | _ -> () |> GetInput

        () |> GetInput

    () |> PlayGame


let rec NounMainMenu() =   
     
    [|
        $"What do you want to do?";
        $"1: Display Masculine Nouns";
        $"2: Display Feminine Nouns";
        $"3: Display Neuter Nouns";
        $"4: Play The Definite Article Game";
        $"Q: Quit"
    |] 
    |> PrintMenu

    let inputValue = Console.ReadLine()

    match inputValue with
    | "1" -> Gender.Masculine |> GetNouns |> Array.map GetPrintNounText |> PressAnyKeyToContinuePrintMenu |> ignore
    | "2" -> Gender.Feminine |> GetNouns |> Array.map GetPrintNounText |> PressAnyKeyToContinuePrintMenu |> ignore
    | "3" -> Gender.Neuter |> GetNouns |> Array.map GetPrintNounText |> PressAnyKeyToContinuePrintMenu |> ignore
    | "4" -> () |> PlayTheDefiniteArticleGame |> ignore
    | "Q" | "q" -> () |> ignore
    | _ -> NounMainMenu()