module WordInfo

open System

type VerbType =
    Regular

type Verb = {
    Index: int
    English: string
    German: string
    Root: string
    Ending: string
    Type: VerbType
}
    

type Gender =
    | Masculine
    | Feminine
    | Neuter

type Noun = {
    English: string
    German: string
    Gender: Gender
}