// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open dotenv.net
open FsEfTest.Config
open FsEfTest
open FsEfTest.Models
open System
open System.Linq

open dbintegrationDomain
open CompositionRoot
open System.Collections.Generic

[<EntryPoint>]
let main argv =
   
        
    // Read one Provider
    Console.WriteLine "Provider"
    let p = getProvider 1L
    match p with
    | Some s -> Console.WriteLine("Provider: " + s.Name)
    | _ -> ()  

    // Read all Providers
    let providers = getAllProviders    
    for p in providers do
      Console.WriteLine("Provider: " + p.Name)    
    
    // Insert a new Provider
    /////let tr = Seq.empty.ToArray
    let p = { ProviderId=1L; 
              Name="Compania da Baixada Operária";
              ShortName="CBO"; 
              Description="Companhia exemplo" 
            }
    addProvider p
    
    let lastProvider = getLastProvider 
    Console.WriteLine "Provider Inserido"
    for p in providers do
      Console.WriteLine("Provider: " + p.Name)
      Console.WriteLine("Provider: " + p.ShortName)
      Console.WriteLine("Provider: " + p.Description)


    //let connectionStringDB = Environment.GetEnvironmentVariable "HOST_DB"        
    //let hostDB = envVars.Item("STRCONNECT_DB")
    //let strConnection = FsEfTest.Config.getEnvVar "STRCONNECT_DB"
    //printfn "\nDados de Configuracao: \n String de conexao: {%s}" strConnection
       
    
    0 // return an integer exit code