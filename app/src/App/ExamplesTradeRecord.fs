module ExamplesTradeRecord
    open Library

    (* TradeRecord Examples *)

    let displayOneTradeRecord id =
        let p = findTradeRecord id
        p

    // Get all providers
    let displayAllTradeRecords =
        let p = allTradeRecords
        p

    // Insert currency
    let insertOneTradeRecord datetimetransaction quantity traderateid typetransaction =
        let p = insertTradeRecord(0L,
                                  datetimetransaction,
                                  System.Int64.Parse(quantity),
                                  System.Int64.Parse(traderateid),
                                  typetransaction)
        p

    let tradeRecordExamples =
        "\nTradeRecord manipulation examples" |> printfn "%s"
        "==============================\n" |> printfn "%s"

        "\nDisplay all TradeRecords" |> printfn "%s"
        displayAllTradeRecords |> printfn "%s"

        "\nInsert one TradeRecord" |> printfn "%s"
        insertOneTradeRecord "01/08/2021 10:30:21" "1000" "1" "SELL"|> printfn "%s"

        "\nDisplay all TradeRecord" |> printfn "%s"
        displayAllTradeRecords |> printfn "%s"

