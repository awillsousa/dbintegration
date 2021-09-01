open System
open Library
open Argu
open App.ExamplesCurrency
open App.ExamplesProvider
open App.ExamplesCurrencyPair
open App.ExamplesRateRecord
open App.ExamplesTradeRecord
open App.LoadData

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

        let r = match loaddata with
                    | false -> ""
                    | true  -> loadData
        Console.WriteLine(r)

        match provider || all with
        | false -> "" |> ignore
        | true  -> providerExamples |> ignore

        match currency || all with
        | false -> "" |> ignore
        | true  -> currencyExamples |> ignore

        match currencypair || all with
        | false -> "" |> ignore
        | true  -> currencyPairExamples |> ignore

        match raterecord || all with
        | false -> "" |> ignore
        | true  -> rateRecordExamples |> ignore












    with e ->
        printfn "%s" e.Message


    0 // return an integer exit code
