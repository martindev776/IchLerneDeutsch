module SubjectInfo

type Pronouns = 
    | Ich
    | Du
    | Er
    | SieInformal
    | Es
    | Wir
    | Ihr
    | SieFormal
    | SiePlural

let AllPronouns =
    [|Ich;Du;Er;SieInformal;Es;Wir;Ihr;SieFormal;SiePlural|]

let MapSubject subject =
    match subject with
    | Ich -> "ich"
    | Du -> "du"
    | Er -> "er"
    | SieInformal | SiePlural -> "sie"
    | Es -> "es"
    | Wir -> "wir"
    | Ihr -> "ihr"
    | SieFormal -> "Sie"
