// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open System.IO

let readInput (file:String) = seq {
    use sr = new StreamReader (file)
    while not sr.EndOfStream do
        yield sr.ReadLine ()
}

let splitByBlank (input:String[]) : String[][]  =
    let accumulate (acc:String list list) (value:String) : String list list  =
        match (acc, value) with
        | ([]::t,v) -> [v]::t 
        | (a, "") -> []::a
        | (h::t,v) -> (v::h)::t       
    input |> Seq.fold accumulate [[]] |> Seq.map Seq.toArray |> Seq.toArray  
 
let any (input:String[]) : int =
    input |> Seq.map Set.ofSeq |> Set.unionMany |> Seq.length
    
let all (input:String[]) : int =
    input |> Seq.map Set.ofSeq |> Set.intersectMany |> Seq.length 

[<EntryPoint>]
let main argv =
    let input = readInput "/Users/xeno/projects/aoc2020/day6_fs/input.txt" |> Seq.toArray
    let groups = splitByBlank input
    let answer1 = groups |> Seq.map any |> Seq.sum
    let answer2 = groups |> Seq.map all |> Seq.sum         
    printfn "Answer 1: %d" answer1
    printfn "Answer 2: %d" answer2
    0 // return an integer exit code