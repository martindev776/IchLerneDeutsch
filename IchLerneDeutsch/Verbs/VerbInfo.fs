module VerbInfo

type VerbType =
    Regular

type Verb = {
    English: string
    German: string
    Root: string
    Ending: string
    Type: VerbType
}