open System
open Library
open Argu
open ExamplesCurrency
open ExamplesProvider
open ExamplesCurrencyPair
open ExamplesRateRecord
open ExamplesTradeRecord
open LoadData
open DBILib.CompositionRoot

type CliArguments =
    | Load_Data
    | Provider
    | Currency
    | CurrencyPair
    | RateRecord
    | TradeRecord
    | All

    interface IArgParserTemplate with
        member s.Usage =
            match s with
            | Load_Data _ -> "Load demonstration data into database."
            | Provider _ -> "Insert, Retrieve and Delete Provider data."
            | Currency _ -> "Insert, Retrieve and Delete Currency data."
            | CurrencyPair _ -> "Insert, Retrieve and Delete CurrencyPair data."
            | RateRecord _ -> "Insert, Retrieve and Delete RateRecord data."
            | TradeRecord _ -> "Insert, Retrieve and Delete TradeRecord data."
            | All _ -> "Insert, Retrieve and Delete for all tables."

[<EntryPoint>]
let main argv =
    let parser = ArgumentParser.Create<CliArguments>(programName = "dotnet run")
    try
        parser.ParseCommandLine(inputs = argv, raiseOnUsage = true) |> ignore
        let results = parser.Parse()

        let loaddata = results.Contains Load_Data
        let provider = results.Contains Provider
        let currency = results.Contains Currency
        let currencypair = results.Contains CurrencyPair
        let raterecord = results.Contains RateRecord
        let traderecord = results.Contains TradeRecord
        let all = results.Contains All

        if provider then
            providerExamples
        elif currency then
            currencyExamples
        elif currencypair then
            currencyPairExamples
        elif raterecord then
            rateRecordExamples
        elif traderecord then
            tradeRecordExamples
        elif all then
            providerExamples
            currencyExamples
            currencyPairExamples
            rateRecordExamples
            tradeRecordExamples
        elif loaddata then
            // Exclude all data in the database
            // TODO: this code thrown an exception
            //       check if using TRUNCATE is the
            // the better option
            (*
            excludeAllTradeRecords |> printfn "%s"
            excludeAllRateRecords |> printfn "%s"
            excludeAllCurrencyPairs |> printfn "%s"
            excludeAllCurrencies |> printfn "%s"
            excludeAllProviders |> printfn "%s"
            *)

            // Insert dummy data for Provider
            let providersDummy = ProviderGenerator.Generate(10)
            for l in providersDummy do
               try
                   addProvider l |> ignore
               with
                    ex -> ex.Message |> ignore
            "\nProviders inserted into database\n" |> printfn "%s"
            displayAllProviders |> printfn "%s"

            // Insert dummy data for Currency
            let currencyDummy = CurrencyGenerator.Generate(10)
            for l in currencyDummy do
                try
                    addCurrency l |> ignore
                with
                    ex -> ex.Message |> ignore

            //"\nCurrencies inserted into database\n" |> printfn "%s"
            //displayAllCurrencies |> printfn "%s"

            // Insert dummy data for CurrencyPair
            let currencyPairDummy = CurrencyPairGenerator.Generate(100)
            for l in currencyPairDummy do
                try
                    addCurrencyPair l |> ignore
                with
                    ex -> ex.Message |> ignore

            //"\nCurrencyPairs inserted into database\n" |> printfn "%s"
            //displayAllCurrencyPairs |> printfn "%s"

            // Insert dummy data for RateRecord
            let rateRecordDummy = RateRecordGenerator.Generate(300)
            for l in rateRecordDummy do
                try
                    addRateRecord l |> ignore
                with
                    ex -> ex.Message |> ignore //printf "%s"

            //"\nRateRecords inserted into database\n" |> printfn "%s"
            //displayAllRateRecord |> printfn "%s"

        else
            printfn "%s" "Invalid option."
            let usage = parser.PrintUsage()
            printfn "%s" usage


    with e ->
        printfn "%s" e.Message

    0 // return an integer exit code
