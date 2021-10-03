module ReadCsvs

open System.IO

let Read csvName =
    File.ReadLines(@"DataFiles\" + csvName + ".csv") 
    |> Seq.toArray
    |> Array.map (fun x -> x.Split(","))

let Read2ColumnCsv csvName =
    csvName
    |> Read
    |> Array.map (fun x -> (x.[0].Trim().ToString(), x.[1].Trim().ToString()))

let ReadIrregularVerbCsv verbName =
    $"\IrregularVerbs\{verbName}"
    |> Read    
    |> Array.map (fun x -> x.[0].ToString())
    |> Array.indexed
    |> dict