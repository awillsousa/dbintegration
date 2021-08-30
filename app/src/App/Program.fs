open System
open Library

[<EntryPoint>]
let main argv =
    
    // Get some provider
    Console.WriteLine "Get just one Provider"
    let p = findProvider 7L
    Console.WriteLine(p)

    // Get all providers
    Console.WriteLine "Get all Providers"
    let p = allProviders
    Console.WriteLine(p)

    // Insert some provider   
    try 
        let r = insertProvider ("0", "Universal Series Broker", "USB", "Broker USM")
        Console.WriteLine(r) 
    with 
    | :? System.InvalidOperationException -> printfn "Invalid Operation Error" |> ignore

    // Insert some provider   
    try 
        let r = excludeProvider 3L
        Console.WriteLine(r) 
    with 
    | :? System.InvalidOperationException -> printfn "Invalid Operation Error" |> ignore

    0 // return an integer exit code
