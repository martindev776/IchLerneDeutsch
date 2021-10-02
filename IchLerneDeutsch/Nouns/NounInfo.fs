module NounInfo

type Gender =
    | Masculine
    | Feminine
    | Neuter

type Noun = {
    English: string
    German: string
    Gender: Gender
}