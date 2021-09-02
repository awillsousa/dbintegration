module ExamplesCurrencyPair
    open Library

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
