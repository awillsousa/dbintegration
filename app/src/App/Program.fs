open System
open Library
open Argu
open ExamplesCurrency
open ExamplesProvider
open ExamplesCurrencyPair
open ExamplesRateRecord
open ExamplesTradeRecord
open LoadData

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
        else
            printfn "%s" "Invalid option."
            let usage = parser.PrintUsage()
            printfn "%s" usage


        (*
        let r = match loaddata with
                    | false -> ""
                    | true  -> loadData
        printfn "%s" r

        match provider || all with
        | false -> "" |> ignore
        | true  -> ExamplesProvider.providerExamples

        match currency || all with
        | false -> "" |> ignore
        | true  -> ExamplesCurrency.currencyExamples

        match currencypair || all with
        | false -> "" |> ignore
        | true  -> currencyPairExamples

        match raterecord || all with
        | false -> "" |> ignore
        | true  -> rateRecordExamples
        *)

    with e ->
        printfn "%s" e.Message

    0 // return an integer exit code
