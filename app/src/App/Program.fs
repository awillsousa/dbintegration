open System
open Library


[<EntryPoint>]
let main argv =

    (* Provider examples  *)

    let displayOneProvider id =
        Console.WriteLine ("\nGet just one Provider with id = 7")
        let p = findProvider id
        Console.WriteLine(p)

    // Get all providers
    let displayAllProviders =
        let p = allProviders
        p

    // Insert provider
    let insertOneProvider name shortname description =
        let r = insertProvider(name, shortname, description)
        Console.WriteLine(r)

    let providerExamples =
        Console.WriteLine("\nProvider manipulation examples")
        Console.WriteLine("==============================\n")

        Console.WriteLine("\nDisplay all Providers")
        Console.WriteLine(displayAllProviders)

        Console.WriteLine ("\nInsert one provider")
        insertOneProvider "New Universal Series Broker" "N-USB" "New Broker USM"

        Console.WriteLine ("\nInsert one provider with empty fields")
        insertOneProvider "" "" "Broker Description"

        Console.WriteLine ("\nExclude one provider that doesn't exists")
        let r = excludeProvider 1L
        Console.WriteLine (r)

    (* Currency Examples *)

    let displayOneCurrency id =
        Console.WriteLine ("\nGet just one Currency")
        let p = findCurrency id
        Console.WriteLine(p)

    // Get all providers
    let displayAllCurrencies =
        let p = allCurrencies
        Console.WriteLine(p)

    // Insert currency
    let insertOneCurrency alias name symbol =
        let r = insertCurrency(alias, name, symbol)
        Console.WriteLine(r)

    let currencyExamples =
        Console.WriteLine("\nCurrency manipulation examples")
        Console.WriteLine("==============================\n")

        Console.WriteLine("\nDisplay all Currencies")
        displayAllCurrencies |> ignore

        Console.WriteLine ("\nInsert one currency")
        insertOneCurrency "REAL" "Real" "R$"
        Console.WriteLine(displayAllCurrencies)

        Console.WriteLine ("\nInsert one currency")
        insertOneCurrency "DOLAR" "USD" "US$"
        Console.WriteLine(displayAllCurrencies)

        Console.WriteLine ("\nInsert one currency with empty fields")
        insertOneCurrency "" "" "R$"

        Console.WriteLine ("\nExclude one currency that doesn't exists")
        let r = excludeCurrency 1000L
        Console.WriteLine (r)

    (* CurrencyPair Examples *)

    let displayOneCurrencyPair id =
        Console.WriteLine ("\nGet just one CurrencyPair")
        let p = findCurrencyPair id
        Console.WriteLine(p)

    // Get all providers
    let displayAllCurrencyPairs =
        let p = allCurrencyPairs
        p

    // Insert currency
    let insertOneCurrencyPair alias currency1 currency2 =
        let r = insertCurrencyPair(alias, System.Int64.Parse(currency1), System.Int64.Parse(currency2))
        Console.WriteLine(r)

    let currencyPairExamples =
        Console.WriteLine("\nCurrencyPair manipulation examples")
        Console.WriteLine("==============================\n")

        Console.WriteLine("\nDisplay all CurrencyPairs")
        Console.WriteLine(displayAllCurrencyPairs)

        Console.WriteLine ("\nInsert one Currency Pair")
        insertOneCurrencyPair "REAL/USD" "1" "2"
        Console.WriteLine("\nDisplay all CurrencyPairs")
        Console.WriteLine(displayAllCurrencyPairs)

        Console.WriteLine ("\nInsert one Currency Pair")
        insertOneCurrencyPair "USD/REAL" "2" "1"
        Console.WriteLine("\nDisplay all CurrencyPairs")
        Console.WriteLine(displayAllCurrencyPairs)


        Console.WriteLine ("\nInsert one CurrencyPair with invalid fields")
        insertOneCurrencyPair "" "100" "102"

        Console.WriteLine ("\nExclude one CurrencyPair that doesn't exists")
        let r = excludeCurrencyPair 1000L
        Console.WriteLine (r)

    (* RateRecord Examples *)

    let displayOneRateRecord id =
        Console.WriteLine ("\nGet just one CurrencyPair")
        let p = findRateRecord id
        Console.WriteLine(p)

    // Get all providers
    let displayAllRateRecord =
        let p = allRateRecords
        p

    // Insert currency
    let insertOneRateRecord currencypairid datetimerate price providerid =
        let r = insertRateRecord(System.Int64.Parse(currencypairid),
                                   datetimerate,
                                   System.Decimal.Parse(price),
                                   System.Int64.Parse(providerid))
        Console.WriteLine(r)

    let currencyPairExamples =
        Console.WriteLine("\RateRecord manipulation examples")
        Console.WriteLine("==============================\n")

        Console.WriteLine("\nDisplay all RateRecords")
        Console.WriteLine(displayAllRateRecord)

        Console.WriteLine ("\nInsert one RateRecord")
        insertOneRateRecord "2" "01/08/2021 10:30:21" "5.00" "30"
        Console.WriteLine("\nDisplay all RateRecord")
        Console.WriteLine(displayAllRateRecord)

        (*
        Console.WriteLine ("\nInsert one RateRecord")
        insertOneCurrencyPair "USD/REAL" "2" "1"
        Console.WriteLine("\nDisplay all RateRecord")
        Console.WriteLine(displayAllCurrencyPairs)


        Console.WriteLine ("\nInsert one CurrencyPair with invalid fields")
        insertOneCurrencyPair "" "100" "102"

        Console.WriteLine ("\nExclude one CurrencyPair that doesn't exists")
        let r = excludeCurrencyPair 1000L
        Console.WriteLine (r)
        *)
    0 // return an integer exit code
