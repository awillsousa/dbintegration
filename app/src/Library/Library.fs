module Library

//open Newtonsoft.Json
open System.Text.Json
open System.Data.SqlClient
open DBILib.Models.dbintegrationDomain
open DBILib.CompositionRoot
open DBILib.dbintegrationValidation
open System.Collections.Generic
open Newtonsoft.Json

(******************************)
(*      Helper functions      *)
(******************************)

// Record defined to be serialized in all function calls
type ResultData = {Result: string; Msg: obj; Data: obj }


// Function to create a record to contain information about
// the request that will be serialized to json
let createResponseRecord (r:string, msg:obj, data:obj) =
    let r = {Result = r; Msg = msg; Data = data}
    r

// Serialize records
let serializeResult r =
    let options = JsonSerializerSettings()
    options.ReferenceLoopHandling <- ReferenceLoopHandling.Ignore
    options.Formatting <- Formatting.Indented
    options.MaxDepth <- 1
    JsonConvert.SerializeObject(r, options)


(******************************)
(* Provider related functions *)
(******************************)

// Find one provider instance by id
let findProvider id =
    try
        let providerData = getProvider id
        let result = match providerData with
                        | None -> createResponseRecord ("ok", ["No data found!"],[])
                        | Some(providerData) -> createResponseRecord ("ok", [""], [providerData])
        serializeResult(result)
    with
        | :? System.InvalidOperationException as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.Data.DataException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.ArgumentException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | ex -> serializeResult(createResponseRecord("exception", ex.Message, None))


// Return all providers in the database
let allProviders =
    try
        let providerData = getAllProviders
        let result = match providerData.Length with
                        | 0 -> createResponseRecord ("ok", "No data found!", [])
                        | _ -> createResponseRecord ("ok", "", providerData)
        serializeResult(result)
    with
        | :? System.InvalidOperationException as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.Data.DataException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.ArgumentException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | ex -> serializeResult(createResponseRecord("exception", ex.Message, None))


// Insert a provider instance
let insertProvider (providerid: int64, name:string, shortname:string, description:string) =
    try
        // Validate the provider record field values if all is OK
        // create a provider record
        let p =  createProvider providerid name shortname description

        // Analyze the return of the createProvider and return error
        // or try insert the record
        let result = match p with
                     | Error errs -> createResponseRecord ("error", String.concat "\n" errs , None)
                     | Ok p -> match addProvider p with
                                            | Some(p) -> createResponseRecord ("ok", "Provider inserted with success!", None)
                                            | None -> createResponseRecord ("error", "Error when inserting data!", None)
        // Serialize the result
        let jsonresult = serializeResult(result)
        jsonresult
    with
        | :? System.InvalidOperationException as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.Data.DataException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.ArgumentException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | ex -> serializeResult(createResponseRecord("exception", ex.Message, None))


// Update a provider instance
let changeProvider providerid name shortname description =
    try
        // Validate the provider record field values if all is OK
        // create a provider record
        let p =  createProvider providerid name shortname description

        // Analyze the return of the createProvider and return error
        // or try insert the record
        let result = match p with
                     | Error errs -> createResponseRecord ("error", String.concat "\n" errs , None)
                     | Ok p ->
                             // Verify if this provider exists in the database
                             let providerInstance = getProvider providerid
                             match providerInstance with
                                | None ->  match addProvider p with
                                            | Some(providerInstance) -> createResponseRecord ("ok", "Provider inserted with success!", None)
                                            | None -> createResponseRecord ("error", "Error when inserting data!", None)
                                | Some(providerInstance) -> createResponseRecord ("error", "Provider already exists!", None)

        // Serialize the result
        let jsonresult = serializeResult(result)
        jsonresult
    with
        | :? System.InvalidOperationException as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.Data.DataException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.ArgumentException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | ex -> serializeResult(createResponseRecord("exception", ex.Message, None))


// Exclude one provider
let excludeProvider id =
    try
        let p = getProvider id
        let result = match p with
                        | None -> createResponseRecord("error", "Provider does not exist in database!", None)
                        | Some p -> if delProvider(p) = None then createResponseRecord("error", "No data deleted!", null)
                                    else createResponseRecord("ok", "Provider deleted!", None)
        serializeResult(result)
    with
        | :? System.InvalidOperationException as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.Data.DataException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.ArgumentException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | ex -> serializeResult(createResponseRecord("exception", ex.Message, None))


// <!!!CAUTION!!!> Exclude all Provider rows in the database
let excludeAllProviders =
    try
        let result = match delAllProviders with
                        | None -> createResponseRecord("error", "No data deleted!", null)
                        | _ -> createResponseRecord("ok", "All Providers deleted!", None)
        serializeResult(result)
    with
        | :? System.InvalidOperationException as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.Data.DataException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.ArgumentException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | ex -> serializeResult(createResponseRecord("exception", ex.Message, None))

(******************************)
(* Currency related functions *)
(******************************)

// Find a specific Curency by id
let findCurrency id =
    try
        let currencyData = getCurrency id
        let result = match currencyData with
                        | None -> createResponseRecord("ok", "No data found!", [])
                        | Some(providerData) -> createResponseRecord("ok", "", [currencyData])
        serializeResult(result)
    with
        | :? System.InvalidOperationException as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.Data.DataException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.ArgumentException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | ex -> serializeResult(createResponseRecord("exception", ex.Message, None))


// Return all providers in the database
let allCurrencies =
    try
        let currencyData = getAllCurrencies
        let result = match currencyData.Length with
                        | 0 -> createResponseRecord("ok", "No data found!", [])
                        | _ -> createResponseRecord("ok", "", [currencyData])
        serializeResult(result)
    with
        | :? System.InvalidOperationException as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.Data.DataException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.ArgumentException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | ex -> serializeResult(createResponseRecord("exception", ex.Message, None))


// Insert a provider instance
let insertCurrency (currencyid:int64, alias:string, name:string, symbol:string) =
    try
        // Validate the currency record field values if all is OK
        // create a currency record
        let p =  createCurrency currencyid alias name symbol

        // Analyze the return of the createProvider and return error
        // or try insert the record
        let result = match p with
                     | Error errs -> createResponseRecord ("error", errs , None)
                     | Ok currencyRec -> match addCurrency currencyRec with
                                            | Some(p) -> createResponseRecord ("ok", "Provider inserted with success!", None)
                                            | None -> createResponseRecord ("error", "Error when inserting data!", None)
        serializeResult(result)
    with
        | :? System.InvalidOperationException as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.Data.DataException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.ArgumentException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | ex -> serializeResult(createResponseRecord("exception", ex.Message, None))


// Update a provider instance
let changeCurrency p =
    try
        let r = updateCurrency p
        let result = match r with
                        | None -> {Result = "error"; Msg="Error when updating data!"; Data=None}
                        | _ -> {Result = "ok"; Msg="Currency updated with success!"; Data=None}
        serializeResult(result)
    with
        | :? System.InvalidOperationException as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.Data.DataException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.ArgumentException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | ex -> serializeResult(createResponseRecord("exception", ex.Message, None))


// Exclude one provider
let excludeCurrency id =
    try
        let p = getCurrency id
        let result = match p with
                        | None -> {Result = "error"; Msg = "Currency does not exist in database!"; Data = None}
                        | Some currency -> if delCurrency(currency) = None then {Result = "error"; Msg = "No data deleted!"; Data = null}
                                           else {Result = "ok"; Msg = "Currency deleted!"; Data = None }
        serializeResult(result)
    with
        | :? System.InvalidOperationException as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.Data.DataException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))

// <!!!CAUTION!!!> Exclude all Currency rows in the database
let excludeAllCurrencies =
    try
        let result = match delAllCurrencies with
                        | None -> createResponseRecord("error", "No data deleted!", null)
                        | _ -> createResponseRecord("ok", "All Currencies deleted!", None)
        serializeResult(result)
    with
        | :? System.InvalidOperationException as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.Data.DataException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.ArgumentException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | ex -> serializeResult(createResponseRecord("exception", ex.Message, None))

(**********************************)
(* CurrencyPair related functions *)
(**********************************)

// Find a specific CurrencyPair
let findCurrencyPair id =
    try
        let currencyPairData = getCurrencyPair id
        let result = match currencyPairData with
                        | None -> createResponseRecord("ok", "No data found!", [])
                        | Some(currencyPairData) -> createResponseRecord("ok", "", [currencyPairData])
        serializeResult(result)
    with
        | :? System.InvalidOperationException as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.Data.DataException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.ArgumentException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | ex -> serializeResult(createResponseRecord("exception", ex.Message, None))

// Return all CurrencyPairs in the database
let allCurrencyPairs =
    try
        let currencyPairData = getAllCurrencyPairs
        let result = match currencyPairData.Length with
                        | 0 -> createResponseRecord("ok", "No data found!", [])
                        | _ -> createResponseRecord("ok", "", currencyPairData)
        serializeResult(result)
    with
        | :? System.InvalidOperationException as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.Data.DataException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.ArgumentException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | ex -> serializeResult(createResponseRecord("exception", ex.Message, None))

// Insert a CurrencyPair instance
let insertCurrencyPair (currencypairid: int64, alias:string, firstcurrencyid:int64, secondcurrencyid:int64) =
    try
        let p = createCurrencyPair currencypairid alias firstcurrencyid secondcurrencyid

        let result = match p with
                     | Error errs -> createResponseRecord ("error", errs , None)
                     | Ok currencyPair -> match addCurrencyPair currencyPair with
                                            | Some(p) -> createResponseRecord ("ok", "Currency Pair inserted with success!", None)
                                            | None -> createResponseRecord ("error", "Error when inserting data!", None)
        serializeResult(result)
    with
        | :? System.InvalidOperationException as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.Data.DataException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.ArgumentException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | ex -> serializeResult(createResponseRecord("exception", ex.Message, None))

// Update a CurrencyPair instance
let changeCurrencyPair p =
    try
        let r = updateCurrencyPair p
        let result = match r with
                        | None -> createResponseRecord("error", "Error when updating data!", None)
                        | _ -> createResponseRecord("ok", "Currency Pair updated with success!", None)
        serializeResult(result)
    with
        | :? System.InvalidOperationException as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.Data.DataException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.ArgumentException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | ex -> serializeResult(createResponseRecord("exception", ex.Message, None))

// Exclude one CurrencyPair
let excludeCurrencyPair id =
    try
        let p = getCurrencyPair id
        let result = match p with
                        | None -> createResponseRecord("error", "Currency Pair does not exist in database!", None)
                        | Some p -> if delCurrencyPair(p) = None then createResponseRecord("error", "No data deleted!", null)
                                    else createResponseRecord("ok", "Currency Pair deleted!", None)
        serializeResult(result)
    with
        | :? System.InvalidOperationException as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.Data.DataException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.ArgumentException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | ex -> serializeResult(createResponseRecord("exception", ex.Message, None))

// <!!!CAUTION!!!> Exclude all CurrencyPair rows in the database
let excludeAllCurrencyPairs =
    try
        let result = match delAllCurrencyPairs with
                        | None -> createResponseRecord("error", "No data deleted!", null)
                        | _ -> createResponseRecord("ok", "All CurrencyPairs deleted!", None)
        serializeResult(result)
    with
        | :? System.InvalidOperationException as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.Data.DataException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.ArgumentException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | ex -> serializeResult(createResponseRecord("exception", ex.Message, None))


(********************************)
(* RateRecord related functions *)
(********************************)

// Find a specific RateRecord
let findRateRecord id =
    try
        let rateRecordData = getRateRecord id
        let result = match rateRecordData with
                        | None -> createResponseRecord("ok", "No data found!", [])
                        | Some(rateRecordData) -> createResponseRecord("ok", "",[rateRecordData])
        serializeResult(result)
    with
        | :? System.InvalidOperationException as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.Data.DataException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.ArgumentException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | ex -> serializeResult(createResponseRecord("exception", ex.Message, None))


// Return all RateRecords in the database
let allRateRecords =
    try
        let rateRecordData = getAllRateRecords
        let result = match rateRecordData.Length with
                        | 0 -> createResponseRecord("ok", "No data found!", [])
                        | _ -> createResponseRecord("ok", "", rateRecordData)
        serializeResult(result)
    with
        | :? System.InvalidOperationException as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.Data.DataException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.ArgumentException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | ex -> serializeResult(createResponseRecord("exception", ex.Message, None))


// Find all RateRecords by Provider
let findRateRecordbyProvider providerId =
    try
        let rateRecords = getRateRecordsbyProvider providerId
        let result = match rateRecords.Length with
                        | 0 -> createResponseRecord("ok", "No data found!", [])
                        | _ -> createResponseRecord("ok", "", [rateRecords])
        serializeResult(result)
    with
        | :? System.InvalidOperationException as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.Data.DataException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.ArgumentException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | ex -> serializeResult(createResponseRecord("exception", ex.Message, None))


// Filter RateRecords
let filterRateRecord providerId currencyPairId startDate endDate startPrice endPrice =
    try
        let rateRecords = searchRateRecord providerId currencyPairId startDate endDate startPrice endPrice
        let result = match rateRecords.Length with
                        | 0 -> {Result = "ok"; Msg = "No data found!"; Data=[]}
                        | _ -> {Result = "ok"; Msg=""; Data = [rateRecords]}
        serializeResult(result)
    with
        | :? System.InvalidOperationException as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.Data.DataException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))


// Insert a RateRecord
let insertRateRecord (raterecordid:int64, currencypairid:int64, datetimerate:string, price:decimal, providerid:int64) =
    try
        // Validate and create a record of RateRecord type
        let r = createRateRecord raterecordid currencypairid datetimerate price providerid

        let result = match r with
                     | Error errs -> createResponseRecord ("error", errs , None)
                     | Ok p -> match addRateRecord p with
                               | None -> createResponseRecord ("error", "Error when inserting data!", None)
                               | _ -> createResponseRecord ("ok", "Rate Record inserted with success!", None)
        serializeResult(result)
    with
        | :? System.InvalidOperationException as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.Data.DataException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.ArgumentException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | ex -> serializeResult(createResponseRecord("exception", ex.Message, None))


// Change a RateRecord
let changeRateRecord p =
    try
        let r = updateRateRecord p
        let result = match r with
                        | None -> {Result = "error"; Msg="Error when updating data!"; Data=None}
                        | _ -> {Result = "ok"; Msg="Rate Record updated with success!"; Data=None}
        serializeResult(result)
    with
        | :? System.InvalidOperationException as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.Data.DataException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.ArgumentException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | ex -> serializeResult(createResponseRecord("exception", ex.Message, None))


// Exclude RateRecord
let excludeRateRecord id =
    try
        let p = getRateRecord id
        let result = match p with
                        | None -> {Result = "error"; Msg = "Rate Record does not exist in database!"; Data = None}
                        | Some p -> if delRateRecord(p) = None then {Result = "error"; Msg = "No data deleted!"; Data = null}
                                    else {Result = "ok"; Msg = "Rate Record deleted!"; Data = None }
        serializeResult(result)
    with
        | :? System.InvalidOperationException as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.Data.DataException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.ArgumentException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | ex -> serializeResult(createResponseRecord("exception", ex.Message, None))

// <!!!CAUTION!!!> Exclude all RateRecord rows in the database
let excludeAllRateRecords =
    try
        let result = match delAllRateRecords with
                        | None -> createResponseRecord("error", "No data deleted!", null)
                        | _ -> createResponseRecord("ok", "All RateRecords deleted!", None)
        serializeResult(result)
    with
        | :? System.InvalidOperationException as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.Data.DataException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.ArgumentException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | ex -> serializeResult(createResponseRecord("exception", ex.Message, None))


(*********************************)
(* TradeRecord related functions *)
(*********************************)

// Find a specific TradeRecord
let findTradeRecord id =
    try
        let tradeRecordData = getTradeRecord id
        let result = match tradeRecordData with
                        | None -> {Result = "ok"; Msg = "No data found!"; Data=[]}
                        | Some(tradeRecordData) -> {Result = "ok"; Msg=""; Data = [tradeRecordData]}
        serializeResult(result)
    with
        | :? System.InvalidOperationException as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.Data.DataException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.ArgumentException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | ex -> serializeResult(createResponseRecord("exception", ex.Message, None))


// Return all RateRecords in the database
let allTradeRecords =
    try
        let tradeRecordData = getAllTradeRecords
        let result = match tradeRecordData.Length with
                        | 0 -> createResponseRecord("ok", "No data found!", [])
                        | _ -> createResponseRecord("ok", "", [tradeRecordData])
        serializeResult(result)
    with
        | :? System.InvalidOperationException as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.Data.DataException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.ArgumentException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | ex -> serializeResult(createResponseRecord("exception", ex.Message, None))


// Insert a TradeRecord
let insertTradeRecord (traderecordid:int64, datetimetransaction:string, quantity:int64, raterecordid:int64, typetransaction:string) =
    try
        let r = createTradeRecord traderecordid datetimetransaction quantity raterecordid typetransaction

        let result = match r with
                     | Error errs -> createResponseRecord ("error", errs , None)
                     | Ok p -> match addTradeRecord p with
                               | None -> createResponseRecord ("error", "Error when inserting data!", None)
                               | _ -> createResponseRecord ("ok", "Trade Record inserted with success!", None)
        serializeResult(result)
    with
        | :? System.InvalidOperationException as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.Data.DataException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.ArgumentException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | ex -> serializeResult(createResponseRecord("exception", ex.Message, None))


// Update a TradeRecord
let changeTradeRecord p =
    try
        let r = updateTradeRecord p
        let result = match r with
                        | None -> {Result = "error"; Msg="Error when updating data!"; Data=None}
                        | _ -> {Result = "ok"; Msg="Trade Record updated with success!"; Data=None}
        serializeResult(result)
    with
        | :? System.InvalidOperationException as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.Data.DataException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.ArgumentException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | ex -> serializeResult(createResponseRecord("exception", ex.Message, None))


// Exclude a TradeRecord
let excludeTradeRecord id =
    try
        let p = getTradeRecord id
        let result = match p with
                        | None -> {Result = "error"; Msg = "Trade Record does not exist in database!"; Data = None}
                        | Some p -> if delTradeRecord(p) = None then {Result = "error"; Msg = "No data deleted!"; Data = null}
                                    else {Result = "ok"; Msg = "Trade Record deleted!"; Data = None }
        serializeResult(result)
    with
        | :? System.InvalidOperationException as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.Data.DataException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.ArgumentException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | ex -> serializeResult(createResponseRecord("exception", ex.Message, None))


// <!!!CAUTION!!!> Exclude all TradeRecord rows in the database
let excludeAllTradeRecords =
    try
        //let result = match delAllTradeRecords with
        let result = match delAllTradeRecords with
                        | None -> createResponseRecord("error", "No data deleted!", null)
                        | _ -> createResponseRecord("ok", "All TradeRecords deleted!", None)
        serializeResult(result)
    with
        | :? System.InvalidOperationException as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.Data.DataException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.ArgumentException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | ex -> serializeResult(createResponseRecord("exception", ex.Message, None))