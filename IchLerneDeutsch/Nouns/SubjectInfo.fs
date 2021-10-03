module SubjectInfo

type Pronouns = 
    | Ich
    | Du
    | Er
    | Sie
    | Es
    | Wir
    | Ihr
    | SieFormal
    | SiePlural

let AllPronouns =
    [|Ich;Du;Er;Sie;Es;Wir;Ihr;SieFormal;SiePlural|]

let MapPronounToInt subject =
    match subject with
    | Ich -> 0
    | Du -> 1
    | Er -> 2
    | Sie -> 3
    | Es -> 4
    | Wir -> 5
    | Ihr -> 6
    | SieFormal -> 7
    | SiePlural -> 8

let MapSubject subject =
    match subject with
    | Ich -> "ich"
    | Du -> "du"
    | Er -> "er"
    | Sie | SiePlural -> "sie"
    | Es -> "es"
    | Wir -> "wir"
    | Ihr -> "ihr"
    | SieFormal -> "Sie"
