module ExamplesRateRecord
    open Library

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
