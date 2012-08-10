namespace FSharpLearn

open Microsoft.VisualStudio.TestTools.UnitTesting    
open Assertions

[<TestClass>]
type Test_FSharp() =
   
    [<TestMethod>]        
    member this.``should be able to convert a sequence to a string``() =
        [1;2;3;4;5] 
        |> Seq.toSingleSring
        |> Equals "12345"
