module ReadCsvs

open System.IO

let Read csvName =
    File.ReadLines(@"DataFiles\" + csvName + ".csv") 
    |> Seq.toArray
    |> Array.map (fun x -> x.Split(","))

let Read2ColumnCsv csvName =
    csvName
    |> Read
    |> Array.map (fun x -> (x.[0].Trim(), x.[1].Trim()))