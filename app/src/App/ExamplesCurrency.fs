module ExamplesCurrency
    open Library

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

        printfn "%s" "\nInsert one currency with empty fields"
        let r = insertOneCurrency "" "" "R$"
        printfn "%s" r

        printfn "%s" "\nExclude one currency that doesn't exists"
        let r = excludeCurrency 1000L
        printfn "%s" r
