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
        let p = insertCurrency(0L, alias, name, symbol)
        p

    let currencyExamples =
        "\nCurrency manipulation examples" |> printfn "%s"
        "==============================\n" |> printfn "%s"

        "\nDisplay all Currencies" |> printfn "%s"
        displayAllCurrencies |> printfn "%s"

        "\nInsert one currency" |> printfn "%s"
        insertOneCurrency "REAL" "Real" "R$" |> printfn "%s"

        displayAllCurrencies |> printfn "%s"

        "\nInsert one currency" |> printfn "%s"
        insertOneCurrency "DOLAR" "USD" "US$" |> printfn "%s"

        displayAllCurrencies |> printfn "%s"

        "\nInsert one currency with empty fields" |> printfn "%s"
        insertOneCurrency "" "" "R$" |> printfn "%s"

        "\nExclude one currency that doesn't exists" |> printfn "%s"
        excludeCurrency 1000L |> printfn "%s"

