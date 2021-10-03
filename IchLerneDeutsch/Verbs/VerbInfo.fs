module VerbInfo

type VerbType =
    | Regular
    | Irregular

type Verb = {
    English: string
    German: string
    Root: string
    Ending: string
    Type: VerbType
}

let MapRegularVerbs verbType (german, english) =
    { English = english
      German = german
      Root = german.Substring(0, german.Length - 2)
      Ending = "en"
      Type = verbType }