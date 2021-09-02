module ExamplesTradeRecord
    open Library

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

