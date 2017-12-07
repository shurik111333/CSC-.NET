// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

module Main

open System
open Tasks

let test expected actual =
    match actual, expected with
    | actual, expected when actual = expected -> ()
    | actual, expected                        -> printfn "FAIL, actual = %O, expected = %O" actual expected

let testSeq expected actual = Seq.map2 test expected actual |> ignore

let getRandomLists n len = 
    let rand = new Random()
    [for i in 1..n -> [for i in 1..len -> rand.Next()]]

printfn "TESTING STARTED"

let testFib() = 
    let expectedFibonacci = [1; 1; 2; 3; 5; 8; 13; 21; 34]
    testSeq expectedFibonacci <| List.map fib [0..List.length expectedFibonacci - 1]

let testReverse() = 
    let lists = getRandomLists 4 10
    let expectedLists = List.map List.rev lists
    testSeq expectedLists <| List.map reverse lists

let testMergeSort() =
    let lists = getRandomLists 4 10
    let expectedLists = List.map List.sort lists
    testSeq expectedLists <| List.map mergeSort lists

let testTree() = 
    let tree1 = Value 7
    let expected1 = 7
    let tree2 = Sum (Mult (Value 5, Value 6), Divide (Subtract (Value 15, Value 3), Value 4))
    let expected2 = 33
    calc tree1 |> test expected1
    calc tree2 |> test expected2

let testPrimes() = 
    let expectedPrimes = [2; 3; 5; 7; 11; 13; 17; 19; 23; 29]
    let n = Seq.length expectedPrimes
    testSeq expectedPrimes <| (primes() |> Seq.take n)

printfn "TESTING FINISHED"