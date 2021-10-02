module VerbInfo

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