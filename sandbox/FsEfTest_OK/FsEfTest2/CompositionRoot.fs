module CompositionRoot

open FsEfTest
open FsEfTest.Models
open Microsoft.EntityFrameworkCore

let configureContext = 
    (fun () ->
        let optionsBuilder = new DbContextOptionsBuilder<dbintegrationContext>();
        //optionsBuilder.UseNpgsql(@"Server=.\SQLExpress;Database=Series;Integrated Security=SSPI;") |> ignore
        new dbintegrationContext(optionsBuilder.Options)
    )

let getContext = configureContext()
let getProvider  = dbintegrationRepository.getProvider getContext
let getAllProviders = dbintegrationRepository.getAllProviders getContext
let addProvider = dbintegrationRepository.addProvider getContext
let getLastProvider = dbintegrationRepository.getLastProvider getContext

