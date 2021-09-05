module ReadCsvs

open System.IO

let Read csvName =
    File.ReadLines(@"DataFiles\" + csvName + ".csv") 
    |> Seq.toArray
    |> Array.map (fun x -> x.Split(","))
    |> Array.map (fun x -> (x.[0], x.[1]))