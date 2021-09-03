namespace DBILib

open DBILib.Models
open System.Linq
open Microsoft.EntityFrameworkCore
open System.Data.SqlClient

module dbintegrationRepository =

    (************************************)
    // Provider related functions
    (************************************)

    let getProvider (context: dbintegrationContext) id =
        query {
            for provider in context.Providers do
                where (provider.ProviderId = id)
                select provider
                exactlyOneOrDefault
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
        let result = if result >= 1  then Some(entity) else None
        result

    let deleteProvider (context: dbintegrationContext) (entity: dbintegrationDomain.Provider) =
        context.Providers.Remove entity |> ignore
        let result = context.SaveChanges true
        let result = if result >= 1  then Some(entity) else None
        result

    let updateProvider (context: dbintegrationContext) (entity: dbintegrationDomain.Provider) =
        let currentEntry = context.Providers.Find(entity.ProviderId)
        context.Entry(currentEntry).CurrentValues.SetValues(entity)
        let result = context.SaveChanges true
        let result = if result >= 1  then Some(entity) else None
        result

    let deleteAllProviders (context: dbintegrationContext) =
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE PROVIDER CASCADE;") |> ignore
        let result = context.SaveChanges true
        let result = if result >= 1  then Some(result) else None
        result




    (************************************)
    // Currency related functions
    (************************************)

    let getCurrency (context: dbintegrationContext) id =
        query {
            for currency in context.Currencies do
                where (currency.CurrencyId = id)
                select currency
                exactlyOneOrDefault
        } |> (fun x -> if box x = null then None else Some x)

    let getAllCurrencies (context: dbintegrationContext) =
        query {
            for currency in context.Currencies do
                select currency
        }
        |> Seq.toList

    let addCurrency (context: dbintegrationContext) (entity: dbintegrationDomain.Currency) =
        context.Currencies.Add(entity) |> ignore
        let result = context.SaveChanges true
        let result = if result >= 1  then Some(entity) else None
        result

    let deleteCurrency (context: dbintegrationContext) (entity: dbintegrationDomain.Currency) =
        context.Currencies.Remove entity |> ignore
        let result = context.SaveChanges true
        let result = if result >= 1  then Some(entity) else None
        result

    let updateCurrency (context: dbintegrationContext) (entity: dbintegrationDomain.Currency) =
        let currentEntry = context.Currencies.Find(entity.CurrencyId)
        context.Entry(currentEntry).CurrentValues.SetValues(entity)
        let result = context.SaveChanges true
        let result = if result >= 1  then Some(entity) else None
        result

    let deleteAllCurrencies (context: dbintegrationContext) =
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE CURRENCY CASCADE;") |> ignore
        let result = context.SaveChanges true
        let result = if result >= 1  then Some(result) else None
        result


    (************************************)
    // CurrencyPair related functions
    (************************************)

    let getCurrencyPair (context: dbintegrationContext) id =
        query {
            for currencyPair in context.CurrencyPairs do
                where (currencyPair.CurrencyPairId = id)
                select currencyPair
                exactlyOneOrDefault
        } |> (fun x -> if box x = null then None else Some x)

    let getAllCurrencyPairs (context: dbintegrationContext) =
        query {
            for currencyPair in context.CurrencyPairs do
                select currencyPair
        }
        |> Seq.toList


    let addCurrencyPair (context: dbintegrationContext) (entity: dbintegrationDomain.CurrencyPair) =
        context.CurrencyPairs.Add(entity) |> ignore
        let result = context.SaveChanges true
        let result = if result >= 1  then Some(entity) else None
        result

    let deleteCurrencyPair (context: dbintegrationContext) (entity: dbintegrationDomain.CurrencyPair) =
        context.CurrencyPairs.Remove entity |> ignore
        let result = context.SaveChanges true
        let result = if result >= 1  then Some(entity) else None
        result

    let updateCurrencyPair (context: dbintegrationContext) (entity: dbintegrationDomain.CurrencyPair) =
        let currentEntry = context.CurrencyPairs.Find(entity.CurrencyPairId)
        context.Entry(currentEntry).CurrentValues.SetValues(entity)
        let result = context.SaveChanges true
        let result = if result >= 1  then Some(entity) else None
        result

    let deleteAllCurrencyPairs (context: dbintegrationContext) =
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE CURRENCYPAIR CASCADE;") |> ignore
        let result = context.SaveChanges true
        let result = if result >= 1  then Some(result) else None
        result

    (************************************)
    // RateRecord related functions
    (************************************)

    let getRateRecord (context: dbintegrationContext) id =
        query {
            for rateRecord in context.RateRecords do
                where (rateRecord.RateRecordId = id)
                select rateRecord
                exactlyOneOrDefault
        } |> (fun x -> if box x = null then None else Some x)

    let getAllRateRecords (context: dbintegrationContext) =
        query {
            for rateRecord in context.RateRecords do
                select rateRecord
        }
        |> Seq.toList

    let getRateRecordsbyProvider (context: dbintegrationContext) providerId =
        query {
            for rateRecord in context.RateRecords do
                where (rateRecord.ProviderId = providerId)
                select rateRecord
        }
        |> Seq.toList

    let searchRateRecord (context: dbintegrationContext) providerId currencyPairId startDate endDate startPrice endPrice =
        query {
            for rateRecord in context.RateRecords do
                where (rateRecord.ProviderId = providerId &&
                       rateRecord.CurrencyPairId = currencyPairId &&
                       rateRecord.DateTimeRate >= startDate &&
                       rateRecord.DateTimeRate <= endDate &&
                       rateRecord.Price >= startPrice &&
                       rateRecord.Price <= endPrice )
                select rateRecord
        }
        |> Seq.toList

    let addRateRecord (context: dbintegrationContext) (entity: dbintegrationDomain.RateRecord) =
        context.RateRecords.Add(entity) |> ignore
        let result = context.SaveChanges true
        let result = if result >= 1  then Some(entity) else None
        result

    let deleteRateRecord (context: dbintegrationContext) (entity: dbintegrationDomain.RateRecord) =
        context.RateRecords.Remove entity |> ignore
        let result = context.SaveChanges true
        let result = if result >= 1  then Some(entity) else None
        result

    let updateRateRecord (context: dbintegrationContext) (entity: dbintegrationDomain.RateRecord) =
        let currentEntry = context.RateRecords.Find(entity.RateRecordId)
        context.Entry(currentEntry).CurrentValues.SetValues(entity)
        let result = context.SaveChanges true
        let result = if result >= 1  then Some(entity) else None
        result

    let deleteAllRateRecords (context: dbintegrationContext) =
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE RATERECORD CASCADE;") |> ignore
        let result = context.SaveChanges true
        let result = if result >= 1  then Some(result) else None
        result

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
        let result = context.SaveChanges true
        let result = if result >= 1  then Some(entity) else None
        result

    let deleteTradeRecord (context: dbintegrationContext) (entity: dbintegrationDomain.TradeRecord) =
        context.TradeRecords.Remove entity |> ignore
        let result = context.SaveChanges true
        let result = if result >= 1  then Some(entity) else None
        result

    let updateTradeRecord (context: dbintegrationContext) (entity: dbintegrationDomain.TradeRecord) =
        let currentEntry = context.TradeRecords.Find(entity.TradeRecordId)
        context.Entry(currentEntry).CurrentValues.SetValues(entity)
        let result = context.SaveChanges true
        let result = if result >= 1  then Some(entity) else None
        result

    let deleteAllTradeRecords (context: dbintegrationContext) =
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE TRADERECORD CASCADE;") |> ignore
        let result = context.SaveChanges true
        let result = if result >= 1  then Some(result) else None
        result
