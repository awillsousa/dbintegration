module ExamplesRateRecord
    open Library

    (* RateRecord Examples *)

    let displayOneRateRecord id =
        let p = findRateRecord id
        p

    // Get all providers
    let displayAllRateRecord =
        let p = allRateRecords
        p

    // Insert currency
    let insertOneRateRecord currencypairid datetimerate price providerid =
        let p = insertRateRecord(0L,
                                 System.Int64.Parse(currencypairid),
                                 datetimerate,
                                 System.Decimal.Parse(price),
                                 System.Int64.Parse(providerid))
        p


    let rateRecordExamples =
        "\nRateRecord manipulation examples\n==============================\n" |> printfn "%s"
        "\nDisplay all RateRecords" |> printfn "%s"

        displayAllRateRecord |> printfn "%s"

        "\nInsert one RateRecord" |> printfn "%s"
        "currencypairid = 2 datetimerate = 01/08/2021 10:30:21 price = 5.00 providerid = 1" |> printfn "%s"
        insertOneRateRecord "2" "01/08/2021 10:30:21" "5.00" "1" |> printfn "%s"

        "\nDisplay all RateRecord" |> printfn "%s"
        displayAllRateRecord |> printfn "%s"

        "\nInsert one provider with invalid fields" |> printfn "%s"
        "currencypairid = 1000 datetimerate = 0108202110:30:21 price = -5.00 providerid = 2001" |> printfn "%s"
        insertOneRateRecord "1000" "0108202110:30:21" "-5.00" "2001" |> printfn "%s"

        "\nExclude one provider that doesn't exists" |> printfn "%s"
        excludeRateRecord 1000L |> printfn "%s"

