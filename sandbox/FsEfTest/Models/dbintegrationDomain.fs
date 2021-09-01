namespace FsEfTest.Models

open System
open System.Collections.Generic

module rec dbintegrationDomain =

    (*[<CLIMutable>]
    type Contact = {
        Id: Int64
        Email: string
        Firstname: string
        Lastname: string
    }*)
    [<CLIMutable>]
    type Currency = {
        CurrencyId: Int64
        Alias: string
        Name: string
        Symbol: string
        CurrencyPairFirstCurrencies: ICollection<CurrencyPair>
        CurrencyPairSecondCurrencies: ICollection<CurrencyPair>
    }

    [<CLIMutable>]
    type CurrencyPair = {
        CurrencyPairId: Int64
        Alias: string
        FirstCurrencyId: Int64
        SecondCurrencyId: Int64
        FirstCurrency: Currency
        SecondCurrency: Currency
        RateRecords: ICollection<RateRecord>
    }

    [<CLIMutable>]
    type Provider = {
        ProviderId: Int64
        Description: string
        Name: string
        ShortName: string
        RateRecords: ICollection<RateRecord>
    }

    [<CLIMutable>]
    type RateRecord = {
        RateRecordId: Int64
        CurrencyPairId: Int64
        DateTimeRate: DateTime
        Price: decimal
        ProviderId: Int64
        CurrencyPair: CurrencyPair
        Provider: Provider
        TradeRecords: ICollection<TradeRecord>
    }

    [<CLIMutable>]
    type TradeRecord = {
        TradeRecordId: Int64
        DateTimeTransaction: DateTime
        Quantity: Int64
        TradeRateId: Int64
        TypeTransaction: string
        TradeRate: RateRecord
    }
    (*
    [<CLIMutable>]
    type User = {
        UserId: int
        Active: bool option
        Email: string
        FirstName: string
        LastName: string
        Username: string
    }
    *)
