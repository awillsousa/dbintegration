// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open dotenv.net
open FsEfTest.Config


[<EntryPoint>]
let main argv =
    let from whom =
        sprintf "from %s" whom
    
    //let connectionStringDB = Environment.GetEnvironmentVariable "HOST_DB"        
    //let hostDB = envVars.Item("STRCONNECT_DB")
    let strConnection = FsEfTest.Config.getEnvVar "STRCONNECT_DB"
    printfn "\nDados de Configuracao: \n String de conexao: {%s}" strConnection
    
    let message = from "F#" // Call the function
    printfn "Hello world %s" message
    0 // return an integer exit code