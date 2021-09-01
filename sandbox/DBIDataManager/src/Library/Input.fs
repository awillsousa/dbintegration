namespace DBILib


(* Valiadation functions 
    let private isValidEmail (email:string) =
        try
            new System.Net.Mail.MailAddress(email) |> ignore
            true
        with 
        | _ -> false

    let validate (provider: dbintegrationDomain.Provider) =
        let errors = seq {
            if(String.IsNullOrEmpty(provider.Name)) then yield "Name should not be empty"
            if(String.IsNullOrEmpty(provider.ShortName)) then yield "ShortName should not be empty"
            if(String.IsNullOrEmpty(p
            rovider.Description)) then yield "Description should not be empty"            
        }

        if(Seq.isEmpty errors) then Ok provider else Error errors

    let create providerid name shortname description =
        let tr = []
        let c =  { ProviderId = providerid; 
                  Name = "Universal Series Broker";
                  ShortName = "USB"; 
                  Description = "Broker USM";
                  RateRecords = tr.ToArray()
                }
        validate c
    *)

[<RequireQualifiedAccess>]
module Input = 

    open System
    open FsEfTest.Models
    open dbintegrationDomain

    let private captureInput(label:string) = 
            printf "%s" label
            Console.ReadLine()
    (*
    let private readNumber(label:string) = 
        printf "%s" label
        let mutable n = Console.ReadLine()
        
        let result =
            match Int32.TryParse n with
            | (true, result) -> Some(result) 
            | (false, _) -> printfn "Please input a number!"; readNumber()

        

    let private printErrors errs =
        printfn "ERRORS"
        errs |> Seq.iter (printfn "%s")
       
    let rec private captureProvider() =
        printfn "INPUT PROVIDER DATA"
        createProvider
            (captureInput "Id: ")
            (captureInput "Name: ")
            (captureInput "ShortName: ")
            (captureInput "Description: ")
        

    let private captureProviderChoice saveProvider =
        let provider = captureProvider()
        saveProvider provider
        let another = captureInput "\nContinue (Y/N)?"
        match another.ToUpper() with
        | "Y" -> Choice1Of2 ()
        | _ -> Choice2Of2 ()

    let rec private captureProviders saveProvider =
        match captureProviderChoice saveProvider with
        | Choice1Of2 _ -> 
            captureProviders saveProvider
        | Choice2Of2 _ -> ()

    let printMenu() =
        printfn "===================="
        printfn "MENU"
        printfn "===================="
        printfn "1. Show Providers"
        printfn "2. Insert Providers"
        printfn "0. Quit"

    let routeMenuOption i getProviders saveProvider =
        match i with
        | "1" -> 
            printfn "Providers"
            getProviders |> ignore
        | "2" -> captureProviders saveProvider
        | _ -> printMenu()

    let readKey() =
        let k = Console.ReadKey()
        Console.WriteLine()
        k.KeyChar |> string

    *)