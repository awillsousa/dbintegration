// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open dotenv.net
open FsEfTest.Config
open FsEfTest
open FsEfTest.Models
open System
open System.Linq

open dbintegrationDomain

[<EntryPoint>]
let main argv =
   
    let p = { ProviderId=1; Name="Compania da Baixada Operária"; ShortName="CBO"; Description="Companhia exemplo" }
    
   
    //let connectionStringDB = Environment.GetEnvironmentVariable "HOST_DB"        
    //let hostDB = envVars.Item("STRCONNECT_DB")
    let strConnection = FsEfTest.Config.getEnvVar "STRCONNECT_DB"
    printfn "\nDados de Configuracao: \n String de conexao: {%s}" strConnection
    
    let message = from "F#" // Call the function
    printfn "Hello world %s" message
    0 // return an integer exit code