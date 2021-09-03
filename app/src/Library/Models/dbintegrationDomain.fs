namespace DBILib.Models

open System
open System.Collections.Generic
open System.ComponentModel.DataAnnotations
open System.ComponentModel.DataAnnotations.Schema


module rec dbintegrationDomain =


    [<CLIMutable>]
    type Currency = {
        CurrencyId: Int64
        Alias: string
        Name: string
        Symbol: string
    }

    [<CLIMutable>]
    type CurrencyPair = {
        CurrencyPairId: Int64
        Alias: string
        FirstCurrencyId: Int64
        SecondCurrencyId: Int64
    }

    [<CLIMutable>]
    type Provider = {
        ProviderId: Int64
        Description: string
        Name: string
        ShortName: string
    }


    [<CLIMutable>]
    type RateRecord = {
        RateRecordId: Int64
        CurrencyPairId: Int64
        DateTimeRate: DateTime
        Price: decimal
        ProviderId: Int64
    }


    [<CLIMutable>]
    type TradeRecord = {
        TradeRecordId: Int64
        DateTimeTransaction: DateTime
        Quantity: Int64
        TradeRateId: Int64
        TypeTransaction: string
    }
