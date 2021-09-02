namespace App

open System
open Library


module ExamplesProvider =
    (* Provider examples  *)
    let displayOneProvider id =
        let p = findProvider id
        p

    // Get all providers
    let displayAllProviders =
        let p = allProviders
        p

    // Insert provider
    let insertOneProvider name shortname description =
        let p = insertProvider(name, shortname, description)
        p

    let providerExamples =
        // Deleta todas os Providers
        //printfn "%s" removeAllProviders

        printfn "%s" "\nProvider manipulation examples"
        printfn "%s" "==============================\n"

        printfn "%s" "\nDisplay all Providers"
        printfn "%s" displayAllProviders

        printfn "%s" "\nInsert one provider"
        let r = insertOneProvider "New Universal Series Broker" "N-USB" "New Broker USM"
        printfn "%s" r

        printfn "%s" "\nInsert one provider with empty fields"
        let r = insertOneProvider "" "" "Broker Description"
        printfn "%s" r

        printfn "%s" "\nExclude one provider that doesn't exists"
        let r = excludeProvider 1L
        printfn "%s" r

module ExamplesCurrency =
    (* Currency Examples *)

    let displayOneCurrency id =
        let p = findCurrency id
        p

    // Get all providers
    let displayAllCurrencies =
        let p = allCurrencies
        p

    // Insert currency
    let insertOneCurrency alias name symbol =
        let p = insertCurrency(alias, name, symbol)
        p

    let currencyExamples =
        printfn "%s" "\nCurrency manipulation examples"
        printfn "%s" "==============================\n"

        printfn "%s" "\nDisplay all Currencies"
        let r = displayAllCurrencies
        printfn "%s" r

        printfn "%s" "\nInsert one currency"
        let r = insertOneCurrency "REAL" "Real" "R$"
        printfn "%s" r
        let r = displayAllCurrencies
        printfn "%s" r

        printfn "%s" "\nInsert one currency"
        let r = insertOneCurrency "DOLAR" "USD" "US$"
        printfn "%s" r
        let r = displayAllCurrencies
        printfn "%s" r

        Console.WriteLine ("\nInsert one currency with empty fields")
        let r = insertOneCurrency "" "" "R$"
        printfn "%s" r

        printfn "%s" "\nExclude one currency that doesn't exists"
        let r = excludeCurrency 1000L
        printfn "%s" r

module ExamplesCurrencyPair =
    (* CurrencyPair Examples *)

    let displayOneCurrencyPair id =
        let p = findCurrencyPair id
        p

    // Get all providers
    let displayAllCurrencyPairs =
        let p = allCurrencyPairs
        p

    // Insert currency
    let insertOneCurrencyPair alias currency1 currency2 =
        let p = insertCurrencyPair(alias, System.Int64.Parse(currency1), System.Int64.Parse(currency2))
        p

    let currencyPairExamples =
        printfn "%s" "\nCurrencyPair manipulation examples"
        printfn "%s" "==============================\n"

        printfn "%s" "\nDisplay all CurrencyPairs"
        printfn "%s" displayAllCurrencyPairs

        printfn "%s" "\nInsert one Currency Pair"
        let r = insertOneCurrencyPair "REAL/USD" "1" "2"
        printfn "%s" r
        printfn "%s" "\nDisplay all CurrencyPairs"
        let r = displayAllCurrencyPairs
        printfn "%s" r

        printfn "%s" "\nInsert one Currency Pair"
        let r = insertOneCurrencyPair "USD/REAL" "2" "1"
        printfn "%s" r
        printfn "%s" "\nDisplay all CurrencyPairs"
        let r = displayAllCurrencyPairs
        printfn "%s" r

        printfn "%s" "\nInsert one CurrencyPair with invalid fields"
        let r = insertOneCurrencyPair "" "100" "102"
        printfn "%s" r

        printfn "%s" "\nExclude one CurrencyPair that doesn't exists"
        let r = excludeCurrencyPair 1000L
        printfn "%s" r

module ExamplesRateRecord =
    (* RateRecord Examples *)

    let displayOneRateRecord id =
        printfn "%s" "\nGet just one CurrencyPair"
        let p = findRateRecord id
        p

    // Get all providers
    let displayAllRateRecord =
        let p = allRateRecords
        p

    // Insert currency
    let insertOneRateRecord currencypairid datetimerate price providerid =
        let p = insertRateRecord(System.Int64.Parse(currencypairid),
                                   datetimerate,
                                   System.Decimal.Parse(price),
                                   System.Int64.Parse(providerid))
        p

    let rateRecordExamples =
        printfn "%s" "\nRateRecord manipulation examples"
        printfn "%s" "==============================\n"

        printfn "%s" "\nDisplay all RateRecords"
        let r = displayAllRateRecord
        printfn "%s" r

        printfn "%s" "\nInsert one RateRecord"
        let r = insertOneRateRecord "2" "01/08/2021 10:30:21" "5.00" "30"
        printfn "%s" r
        printfn "%s" "\nDisplay all RateRecord"
        let r = displayAllRateRecord
        printfn "%s" r

        (*
        printfn "%s" "\nInsert one RateRecord"
        insertOneCurrencyPair "USD/REAL" "2" "1"
        printfn "%s" "\nDisplay all RateRecord"
        printfn "%s" displayAllCurrencyPairs


        printfn "%s" "\nInsert one CurrencyPair with invalid fields"
        insertOneCurrencyPair "" "100" "102"

        printfn "%s" "\nExclude one CurrencyPair that doesn't exists"
        let r = excludeCurrencyPair 1000L
        printfn "%s" r
        *)

module ExamplesTradeRecord =
    (* TradeRecord Examples *)

    let displayOneTradeRecord id =
        printfn "%s" "\nGet just one TradeRecord"
        let p = findTradeRecord id
        p

    // Get all providers
    let displayAllTradeRecords =
        let p = allTradeRecords
        p

    // Insert currency
    let insertOneTradeRecord currencypairid datetimerate price providerid =
        (*let r = insertTradeRecord(System.Int64.Parse(currencypairid),
                                   datetimerate,
                                   System.Decimal.Parse(price),
                                   System.Int64.Parse(providerid))*)
        let p = ""
        p

    let tradeRecordExamples =
        printfn "%s" "\nTradeRecord manipulation examples"
        printfn "%s" "==============================\n"

        printfn "%s" "\nDisplay all TradeRecords"
        let r = displayAllTradeRecords
        printfn "%s" r

        printfn "%s" "\nInsert one TradeRecord"
        let r = insertOneTradeRecord "2" "01/08/2021 10:30:21" "5.00" "30"
        printfn "%s" r
        printfn "%s" "\nDisplay all TradeRecord"
        let r = displayAllTradeRecords
        printfn "%s" r


module LoadData =

    let loadData =
        "load data"
