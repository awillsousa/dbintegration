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


//let json = Newtonsoft.Json.JsonSerializer.Create()
//let! json.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore

let serializeResult r =
    (*
    let options = JsonSerializerOptions(MaxDepth = 1,
                                        IgnoreNullValues = true,
                                        IgnoreReadOnlyProperties = true)
    JsonSerializer.Serialize(r, options, Formatting.Indented)
    *)

    let options = JsonSerializerSettings()
    options.ReferenceLoopHandling <- ReferenceLoopHandling.Ignore
    options.Formatting <- Formatting.Indented
    options.MaxDepth <- 1
    //let json = Newtonsoft.Json.JsonSerializer.Create(options)
    //json.Serialize(r)
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


// Insert a provider instance
let insertProvider (name:string, shortname:string, description:string) =
    try
        // Validate the provider record field values if all is OK
        // create a provider record
        let defaultProviderId = 0L
        let p =  createProvider defaultProviderId name shortname description

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


// Update a provider instance
let changeProvider providerid name shortname description =
    try
        (*
        let r = updateProvider p
        let result = match r with
                        | None -> createResponseRecord("error", "Error when updating data!", None)
                        | _ -> createResponseRecord("ok", "Provider updated with success!", None)
        serializeResult(result)
        *)
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


(******************************)
(* Currency related functions *)
(******************************)

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


// Insert a provider instance
let insertCurrency (alias:string, name:string, symbol:string) =
    try
        // Validate the currency record field values if all is OK
        // create a currency record
        let defaultCurrencyId = 0L
        let p =  createCurrency defaultCurrencyId alias name symbol

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


// Insert a CurrencyPair instance
let insertCurrencyPair (alias:string, firstcurrencyid:int64, secondcurrencyid:int64) =
    try
        let defaultId = 0L
        let p = createCurrencyPair defaultId alias firstcurrencyid secondcurrencyid

        let result = match p with
                     | Error errs -> createResponseRecord ("error", errs , None)
                     | Ok currencyPair -> match addCurrencyPair currencyPair with
                                            | Some(p) -> createResponseRecord ("ok", "Currency Pair inserted with success!", None)
                                            | None -> createResponseRecord ("error", "Error when inserting data!", None)
        serializeResult(result)
    with
        | :? System.InvalidOperationException as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.Data.DataException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))


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

// Return all RateRecords in the database
let allRateRecords =
    try
        let rateRecordData = getAllRateRecords
        let result = match rateRecordData.Length with
                        | 0 -> createResponseRecord("ok", "No data found!", [])
                        | _ -> createResponseRecord("ok", "", [rateRecordData])
        serializeResult(result)
    with
        | :? System.InvalidOperationException as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.Data.DataException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))



// Find all RateRecords by Provider
let findRateRecordbyProvider providerId =
    try
        //let providerSome = getProvider providerId
        //let providerData = providerSome.Value
        let rateRecords = getRateRecordsbyProvider providerId
        let result = match rateRecords.Length with
                        | 0 -> createResponseRecord("ok", "No data found!", [])
                        | _ -> createResponseRecord("ok", "", [rateRecords])
        serializeResult(result)
    with
        | :? System.InvalidOperationException as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.Data.DataException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))

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
let insertRateRecord (currencypairid:int64, datetimerate:string, price:decimal, providerid:int64) =
    try
        (*
        let cp = getCurrencyPair currencypairid
        let provider = getProvider providerid
        let tr = List<TradeRecord>()
        let p =  {  RateRecordId = 0L;
                    CurrencyPairId = currencypairid;
                    DateTimeRate = System.DateTime.Parse(datetimerate);
                    Price = price;
                    ProviderId = providerid;
                    CurrencyPair = cp.Value;
                    Provider = provider.Value;
                    TradeRecords = tr
                  }
        *)
        let idraterecord = 0L // Default to autoincrement
        // Validate and create a record of RateRecord type
        let r = createRateRecord idraterecord currencypairid datetimerate price providerid

        let result = match r with
                     | Error errs -> createResponseRecord ("error", errs , None)
                     | Ok p -> match addRateRecord p with
                               | None -> createResponseRecord ("error", "Error when inserting data!", None)
                               | _ -> createResponseRecord ("ok", "Rate Record inserted with success!", None)
        serializeResult(result)
    with
        | :? System.InvalidOperationException as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.Data.DataException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))

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



// Insert a TradeRecord
let insertTradeRecord (currencypairid:int64, datetimetransaction:string, quantity:int64, traderateid:int64, typetransaction:string) =
    try
        let tr = getRateRecord traderateid
        let p =  {  TradeRecordId = 0L;
                    DateTimeTransaction = System.DateTime.Parse(datetimetransaction);
                    Quantity = quantity;
                    TradeRateId = traderateid;
                    TypeTransaction = typetransaction;
                    //TradeRate = tr.Value
                  }

        let r = addTradeRecord p
        let result = match r with
                        | None -> {Result = "error"; Msg="Error when inserting data!"; Data=None}
                        | _ -> {Result = "ok"; Msg="Trade Record inserted with success!"; Data=None}
        serializeResult(result)
    with
        | :? System.InvalidOperationException as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))
        | :? System.Data.DataException  as ex -> serializeResult(createResponseRecord("exception", ex.Message, None))

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

