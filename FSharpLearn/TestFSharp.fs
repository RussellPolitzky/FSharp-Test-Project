namespace FSharpLearn

open Microsoft.VisualStudio.TestTools.UnitTesting    
open Assertions
open ExpertFSharp
open System.Diagnostics
open RegistryReader 
open ReadFileAnimalsNumbers


[<TestClass>]
type Test_FSharp() =

    // Intrestingly, the list comprehension here is 
    // around twice as fast as the recursive mapping
    // function, but when we turn on the release
    // compile and the tail recursive all, then 
    // they're almost the same speed.  Given that the
    // mapping function is so much more general, 
    // this is quite an interesting result.
    //
    // Conclusion:  Recursive calls are fast in 
    // F# when release build optimizations are
    // used.

    [<TestMethod>]        
    member this.``should be able to use map function for lists``() =
        let lowerBound = 1
        let upperBound = 10000
        let stopWatch = new Stopwatch()
        stopWatch.Start()
        // Note the form of this very simple list comprehension 
        // here.  Note the generator function.
        let expected = [ for i in lowerBound .. upperBound -> i*i ]
        let t1 = stopWatch.Elapsed

        let recResult = 
            [lowerBound..upperBound] 
            |> ExpertFSharp.map (fun i -> i*i)

        stopWatch.Stop()
        let t2 = stopWatch.Elapsed

        recResult |> Equals expected

        let compTime = t1
        let recTime  = t2 - t1
        printf "List comp time: %A, Recursive function: %A\r\n" compTime.TotalMilliseconds recTime.TotalMilliseconds
        printf "The list comp was %A times faster." ((float recTime.Ticks)/(float compTime.Ticks))
        recTime.Ticks |> GreaterThan compTime.Ticks 
        
        // Recursive is only a little slower than
        // the list comprehension when release optimisation
        // is turned on.

        
    [<TestMethod>]        
    member this.``should be able to determine if a proxy is in use or not``() =
        (getRegistryValue "HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Internet Settings\ProxyEnable").ToString()
        |> IsSameStringAs @""

    
    [<TestMethod>]        
    member this.``should be able to get a number animal file``() =
        getFile
        |> IsSameStringAs ""
    
    
    
    
    
    
    
    
    
        