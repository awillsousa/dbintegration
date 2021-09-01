namespace FsEfTest

open FsEfTest.Models
open System.Linq
open Microsoft.EntityFrameworkCore
open System.Data.SqlClient

type NonQueryResult =
    | Success of int
    | LoginError of SqlException
    | ConstraintError of SqlException
    | ForeignKeyError of SqlException

module dbintegrationRepository =

    (************************************)
    // Provider related functions
    (************************************)

    let getProvider (context: dbintegrationContext) id =
        query {
            for provider in context.Providers do
                where (provider.ProviderId = id)
                select provider 
                exactlyOne
        } |> (fun x -> if box x = null then None else Some x)

    let getAllProviders (context: dbintegrationContext) =
        query {
            for provider in context.Providers do                
                select provider                 
        } 
        |> Seq.toList

    let addProvider (context: dbintegrationContext) (entity: dbintegrationDomain.Provider) =
        context.Providers.Add(entity) |> ignore
        let result = context.SaveChanges true
        match result with
          | 0 -> printfn "error"
          | _ -> printfn "success"
          
          
    let addProvider2 (context: dbintegrationContext) (entity: dbintegrationDomain.Provider) =
        context.Providers.Add(entity) |> ignore
        context.SaveChanges true |> ignore

    let deleteProvider (context: dbintegrationContext) (entity: dbintegrationDomain.Provider) = 
        context.Providers.Remove entity |> ignore
        context.SaveChanges true |> ignore

    let updateProvider (context: dbintegrationContext) (entity: dbintegrationDomain.Provider) = 
        let currentEntry = context.Providers.Find(entity.ProviderId)
        context.Entry(currentEntry).CurrentValues.SetValues(entity)
        context.SaveChanges true |> ignore

    (************************************)
    // Currency related functions
    (************************************)

    let getCurrency (context: dbintegrationContext) id =
        query {
            for currency in context.Currencies do
                where (currency.CurrencyId = id)
                select currency 
                exactlyOne
        } |> (fun x -> if box x = null then None else Some x)

    let getAllCurrencies (context: dbintegrationContext) =
        query {
            for currency in context.Currencies do                
                select currency                 
        } 
        |> Seq.toList
       
    let addCurrency (context: dbintegrationContext) (entity: dbintegrationDomain.Currency) =
        context.Currencies.Add(entity) |> ignore
        context.SaveChanges true |> ignore

    let deleteCurrency (context: dbintegrationContext) (entity: dbintegrationDomain.Currency) = 
        context.Currencies.Remove entity |> ignore
        context.SaveChanges true |> ignore

    let updateCurrency (context: dbintegrationContext) (entity: dbintegrationDomain.Currency) = 
        let currentEntry = context.Currencies.Find(entity.CurrencyId)
        context.Entry(currentEntry).CurrentValues.SetValues(entity)
        context.SaveChanges true |> ignore

    (************************************)
    // CurrencyPair related functions
    (************************************)

    let getCurrencyPair (context: dbintegrationContext) id =
        query {
            for currencyPair in context.CurrencyPairs do
                where (currencyPair.CurrencyPairId = id)
                select currencyPair 
                exactlyOne
        } |> (fun x -> if box x = null then None else Some x)

    let getAllCurrencyPairs (context: dbintegrationContext) =
        query {
            for currencyPair in context.CurrencyPairs do                
                select currencyPair                 
        } 
        |> Seq.toList
        

    let addCurrencyPair (context: dbintegrationContext) (entity: dbintegrationDomain.CurrencyPair) =
        context.CurrencyPairs.Add(entity) |> ignore
        context.SaveChanges true |> ignore

    let deleteCurrencyPair (context: dbintegrationContext) (entity: dbintegrationDomain.CurrencyPair) = 
        context.CurrencyPairs.Remove entity |> ignore
        context.SaveChanges true |> ignore

    let updateCurrencyPair (context: dbintegrationContext) (entity: dbintegrationDomain.CurrencyPair) = 
        let currentEntry = context.CurrencyPairs.Find(entity.CurrencyPairId)
        context.Entry(currentEntry).CurrentValues.SetValues(entity)
        context.SaveChanges true |> ignore

    (************************************)
    // RateRecord related functions
    (************************************)

    let getRateRecord (context: dbintegrationContext) id =
        query {
            for rateRecord in context.RateRecords do
                where (rateRecord.RateRecordId = id)
                select rateRecord 
                exactlyOne
        } |> (fun x -> if box x = null then None else Some x)

    let getAllRateRecords (context: dbintegrationContext) =
        query {
            for rateRecord in context.RateRecords do                
                select rateRecord                 
        } 
        |> Seq.toList
        

    let addRateRecord (context: dbintegrationContext) (entity: dbintegrationDomain.RateRecord) =
        context.RateRecords.Add(entity) |> ignore
        context.SaveChanges true |> ignore

    let deleteRateRecord (context: dbintegrationContext) (entity: dbintegrationDomain.RateRecord) = 
        context.RateRecords.Remove entity |> ignore
        context.SaveChanges true |> ignore

    let updateRateRecord (context: dbintegrationContext) (entity: dbintegrationDomain.RateRecord) = 
        let currentEntry = context.RateRecords.Find(entity.RateRecordId)
        context.Entry(currentEntry).CurrentValues.SetValues(entity)
        context.SaveChanges true |> ignore

    (************************************)
    // TradeRecord related functions
    (************************************)

    let getTradeRecord (context: dbintegrationContext) id =
        query {
            for tradeRecord in context.TradeRecords do
                where (tradeRecord.TradeRecordId = id)
                select tradeRecord 
                exactlyOne
        } |> (fun x -> if box x = null then None else Some x)

    let getAllTradeRecords (context: dbintegrationContext) =
        query {
            for tradeRecord in context.TradeRecords do                
                select tradeRecord                 
        } 
        |> Seq.toList
        

    let addTradeRecord (context: dbintegrationContext) (entity: dbintegrationDomain.TradeRecord) =
        context.TradeRecords.Add(entity) |> ignore
        context.SaveChanges true |> ignore

    let deleteTradeRecord (context: dbintegrationContext) (entity: dbintegrationDomain.TradeRecord) = 
        context.TradeRecords.Remove entity |> ignore
        context.SaveChanges true |> ignore

    let updateTradeRecord (context: dbintegrationContext) (entity: dbintegrationDomain.TradeRecord) = 
        let currentEntry = context.TradeRecords.Find(entity.TradeRecordId)
        context.Entry(currentEntry).CurrentValues.SetValues(entity)
        context.SaveChanges true |> ignore


    
    