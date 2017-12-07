module Tasks

open System

// Fibonacci
let fib n = 
    let rec fib' a b n = 
        match n with
        | 0 -> b
        | n -> fib' b (a + b) (n - 1)
    fib' 0 1 n

// Reverse list
let reverse l = 
    let rec reverse' l acc = 
        match l with
        | []     -> acc
        | h :: t -> reverse' t (h :: acc)
    reverse' l []

// Merge sort
let rec mergeSort l = 
    let split l k = (List.take k l, List.skip k l)
    let n = List.length l
    let mapPair f (a, b) = (f a, f b)
    let rec merge acc (l, r) = 
        match l, r with
        | [], [] -> acc
        | [], h :: t -> merge (h :: acc) ([], t)
        | h :: t, [] -> merge (h :: acc) (t, [])
        | h1 :: t1, h2 :: t2 when h1 < h2 -> merge (h1 :: acc) (t1, h2 :: t2)
        | h1 :: t1, h2 :: t2              -> merge (h2 :: acc) (h1 :: t1, t2)
    match l with
    | []  -> []
    | [x] -> [x]
    | l   -> split l (n / 2) |> mapPair mergeSort |> merge [] |> List.rev

// Arithmetic tree
type 'a Tree = 
    | Value    of 'a
    | Sum      of 'a Tree * 'a Tree
    | Subtract of 'a Tree * 'a Tree
    | Mult     of 'a Tree * 'a Tree
    | Divide   of 'a Tree * 'a Tree

let rec calc t =
    match t with
    | Value    x        -> x
    | Sum      (t1, t2) -> calc t1 + (calc t2)
    | Subtract (t1, t2) -> calc t1 - (calc t2)
    | Mult     (t1, t2) -> calc t1 * (calc t2)
    | Divide   (t1, t2) -> calc t1 / (calc t2)

// Sequence of primes
let primes() = 
    let isPrime x = 
        let k = x |> float |> Math.Sqrt |> Math.Floor |> int
        Seq.filter ((=) 0 << (%) x) [2..k] |> Seq.isEmpty
    Seq.initInfinite ((+) 2) |> Seq.filter isPrime