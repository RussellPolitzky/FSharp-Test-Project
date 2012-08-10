module ExpertFSharp

/// Page 97 simple recursive function
/// for list.map
let rec map (f: 'T -> 'U)  (l: 'T list) = 
    match l with 
    | h :: t -> f h :: map f t
    | [] -> []