namespace FsEfTest

open FsEfTest.Models
open System.Linq
open Microsoft.EntityFrameworkCore

module dbintegrationRepository =
    let getProvider (context: dbintegrationContext) id =
        query {
            for provider in context.Providers do
                where (provider.ProviderId = id)
                select provider 
                exactlyOne
        } |> (fun x -> if box x = null then None else Some x)

    let addProvider (context: dbintegrationContext) (entity: dbintegrationDomain.Provider) =
        context.Providers.Add(entity) |> ignore
        context.SaveChanges true |> ignore