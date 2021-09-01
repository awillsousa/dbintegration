// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open FsEfTest.Models
open System.Linq
open dbintegrationDomain
open CompositionRoot
open Newtonsoft.Json


let listAllProviders = 
  let providers = getAllProviders 
  for p in providers do
      Console.WriteLine("Provider: " + p.Name)    
  |> ignore
  
let insertProvider p =
    addProvider  p


let validateProvider (provider: dbintegrationDomain.Provider) =
  let errors = seq {
      if(String.IsNullOrEmpty(provider.Name)) then yield "Name should not be empty"
      if(String.IsNullOrEmpty(provider.ShortName)) then yield "ShortName should not be empty"
      if(String.IsNullOrEmpty(provider.Description)) then yield "Description should not be empty"            
  }

  if(Seq.isEmpty errors) then Ok provider else Error errors

(*
let createProvider providerid name shortname description = 
  let tr = []
  let c =  { ProviderId = providerid; 
            Name = name;
            ShortName = shortname; 
            Description = description;
            RateRecords = tr.ToArray()
          }
  let r = validateProvider c
  match r with
  | Ok -> addProvider c
  | _ -> 
  addProvider r
*)

[<EntryPoint>]
let main argv =
  (* 
  Input.printMenu()
  let mutable selection = Input.readKey()
  while(selection <> "0") do
      Input.routeMenuOption selection getAllProviders insertProvider 
      Input.printMenu()
      selection <- Input.readKey()
  0 // return an integer exit code    
  *)
    
    // Read one Provider
    Console.WriteLine "Provider"
    let p = getProvider 1L

    Console.WriteLine "\nProvider serialized JSON\n "
    let jsonProvider = JsonConvert.SerializeObject(p)
    Console.WriteLine(jsonProvider)

    (*
    match p with
    | Some s -> Console.WriteLine("Provider: " + s.Name)
    | _ -> ()  
    *)

    // Read all Providers
    let providers = getAllProviders        
    Console.WriteLine "\nList of All Providers serialized JSON\n "
    let jsonProviders = JsonConvert.SerializeObject(providers)
    Console.WriteLine(jsonProviders)
    Console.WriteLine("\n\n")

    (*for p in providers do
      Console.WriteLine("Provider: " + p.Name)    
    *)

    (*
    // Insert a new Provider    
    let tr = []
    let p =  { ProviderId=3L; 
              Name="Universal Series Broker";
              ShortName="USB"; 
              Description="Broker USM";
              RateRecords = tr.ToArray()
            }
    
    addProvider p
    *)

    (*
    Console.WriteLine "Provider Inserido"    
    Console.WriteLine("Provider: " + lastProvider.Name)
    Console.WriteLine("Provider: " + lastProvider.ShortName)
    Console.WriteLine("Provider: " + lastProvider.Description)
    *)

    //let connectionStringDB = Environment.GetEnvironmentVariable "HOST_DB"        
    //let hostDB = envVars.Item("STRCONNECT_DB")
    //let strConnection = FsEfTest.Config.getEnvVar "STRCONNECT_DB"
    //printfn "\nDados de Configuracao: \n String de conexao: {%s}" strConnection
    0 // return an integer exit code        
    
    