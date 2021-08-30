module Library

//open Newtonsoft.Json
open System.Text.Json
open System.Data.SqlClient
open DBILib.Models.dbintegrationDomain
open DBILib.CompositionRoot
open System.Collections.Generic

// Record defined to be serialized in all function calls
type ResultData = {Result: string; Msg: string; Data: obj }

(******************************)
(* Provider related functions *)
(******************************)

// Find one provider instance by id
let findProvider id =     
    let providerData = getProvider id
    let result = match providerData with
                    | None -> {Result = "ok"; Msg = "No data found!"; Data=[]}
                    | Some(providerData) -> {Result = "ok"; Msg=""; Data = [providerData]}        
    JsonSerializer.Serialize(result)    

// Return all providers in the database
let allProviders = 
    let providerData = getAllProviders
    let result = match providerData.Length with
                    | 0 -> {Result = "ok"; Msg = "No data found!"; Data=[]}
                    | _ -> {Result = "ok"; Msg=""; Data = providerData}        
    JsonSerializer.Serialize(result)    

// Insert a provider instance
let insertProvider (providerid:string, name:string, shortname:string, description:string) =     
    let tr = List<RateRecord>([])
    let mutable provideridInt = providerid |> int64
    let p =  { ProviderId = provideridInt; 
               Name = name;
               ShortName = shortname; 
               Description = description;
               RateRecords =  tr
              } 
    let r = addProvider p     
    let result = match r with
                    | None -> {Result = "error"; Msg="Error when inserting data!"; Data=None}
                    | _ -> {Result = "ok"; Msg="Provider inserted with success!"; Data=None}
    JsonSerializer.Serialize(result)

// Update a provider instance
let changeProvider p =
    let r = updateProvider p
    let result = match r with
                    | None -> {Result = "error"; Msg="Error when updating data!"; Data=None}
                    | _ -> {Result = "ok"; Msg="Provider updated with success!"; Data=None}
    JsonSerializer.Serialize(result)

// Exclude one provider
let excludeProvider id = 
    let p = getProvider id
    let result = match p with  
                    | None -> {Result = "error"; Msg = "Provider does not exist in database!"; Data = None}
                    | Some p -> if delProvider(p) = None then {Result = "error"; Msg = "No data deleted!"; Data = null}
                                else {Result = "ok"; Msg = "Provider deleted!"; Data = None } 
    JsonSerializer.Serialize(result)

(******************************)
(* Currency related functions *)
(******************************)

let findCurrency id = 
    let currencyData = getCurrency id
    let result = match currencyData with
                    | None -> {Result = "ok"; Msg = "No data found!"; Data=[]}
                    | Some(providerData) -> {Result = "ok"; Msg=""; Data = [currencyData]}        
    JsonSerializer.Serialize(result)    

// Return all providers in the database
let allCurrencies = 
    let currencyData = getAllCurrencies
    let result = match currencyData.Length with
                    | 0 -> {Result = "ok"; Msg = "No data found!"; Data=[]}
                    | _ -> {Result = "ok"; Msg=""; Data = currencyData}        
    JsonSerializer.Serialize(result)   

// Insert a provider instance
let insertCurrency (alias:string, name:string, symbol:string) =     
    let cp = List<CurrencyPair>([])
    let p =  { CurrencyId = 0L;
               Alias = alias; 
               Name = name;
               Symbol = symbol; 
               CurrencyPairFirstCurrencies =  cp;
               CurrencyPairSecondCurrencies = cp;
              } 
    let r = addCurrency p     
    let result = match r with
                    | None -> {Result = "error"; Msg="Error when inserting data!"; Data=None}
                    | _ -> {Result = "ok"; Msg="Currency inserted with success!"; Data=None}
    JsonSerializer.Serialize(result)

// Update a provider instance
let changeCurrency p =
    let r = updateCurrency p
    let result = match r with
                    | None -> {Result = "error"; Msg="Error when updating data!"; Data=None}
                    | _ -> {Result = "ok"; Msg="Currency updated with success!"; Data=None}
    JsonSerializer.Serialize(result)

// Exclude one provider
let excludeCurrency id = 
    let p = getCurrency id
    let result = match p with  
                    | None -> {Result = "error"; Msg = "Currency does not exist in database!"; Data = None}
                    | Some p -> if delCurrency(p) = None then {Result = "error"; Msg = "No data deleted!"; Data = null}
                                else {Result = "ok"; Msg = "Currency deleted!"; Data = None } 
    JsonSerializer.Serialize(result)

(**********************************)
(* CurrencyPair related functions *)
(**********************************)


let findCurrencyPair id = 
    let currencyPairData = getCurrencyPair id
    let result = match currencyPairData with
                    | None -> {Result = "ok"; Msg = "No data found!"; Data=[]}
                    | Some(currencyPairData) -> {Result = "ok"; Msg=""; Data = [currencyPairData]}        
    JsonSerializer.Serialize(result)    

// Return all providers in the database
let allCurrencyPairs = 
    let currencyPairData = getAllCurrencies
    let result = match currencyPairData.Length with
                    | 0 -> {Result = "ok"; Msg = "No data found!"; Data=[]}
                    | _ -> {Result = "ok"; Msg=""; Data = currencyPairData}        
    JsonSerializer.Serialize(result)   


// Insert a provider instance
let insertCurrencyPair (alias:string, firstcurrencyid:int64, secondcurrencyid:int64) =     
    let rr = List<RateRecord>([])
    let fc = getCurrency firstcurrencyid
    let sc = getCurrency secondcurrencyid
    let p =  { CurrencyPairId = 0L;
               Alias = alias; 
               FirstCurrencyId = firstcurrencyid;
               SecondCurrencyId = secondcurrencyid; 
               FirstCurrency =  fc.Value;
               SecondCurrency = sc.Value;
               RateRecords = rr;
              } 

    let r = addCurrencyPair p     
    let result = match r with
                    | None -> {Result = "error"; Msg="Error when inserting data!"; Data=None}
                    | _ -> {Result = "ok"; Msg="Currency Pair inserted with success!"; Data=None}
    JsonSerializer.Serialize(result)

// Update a provider instance
let changeCurrencyPair p =
    let r = updateCurrencyPair p
    let result = match r with
                    | None -> {Result = "error"; Msg="Error when updating data!"; Data=None}
                    | _ -> {Result = "ok"; Msg="Currency Pair updated with success!"; Data=None}
    JsonSerializer.Serialize(result)

// Exclude one provider
let excludeCurrencyPair id = 
    let p = getCurrencyPair id
    let result = match p with  
                    | None -> {Result = "error"; Msg = "Currency Pair does not exist in database!"; Data = None}
                    | Some p -> if delCurrencyPair(p) = None then {Result = "error"; Msg = "No data deleted!"; Data = null}
                                else {Result = "ok"; Msg = "Currency Pair deleted!"; Data = None } 
    JsonSerializer.Serialize(result)

(********************************)
(* RateRecord related functions *)
(********************************)

let findRateRecord id = 
    let rateRecordData = getRateRecord id
    let result = match rateRecordData with
                    | None -> {Result = "ok"; Msg = "No data found!"; Data=[]}
                    | Some(rateRecordData) -> {Result = "ok"; Msg=""; Data = [rateRecordData]}        
    JsonSerializer.Serialize(result)    

let findRateRecordbyProvider providerId =
    let providerSome = getProvider providerId
    let providerData = providerSome.Value
    let mutable rateRecords = providerData.RateRecords
    let result = match rateRecords.Count with
                    | 0 -> {Result = "ok"; Msg = "No data found!"; Data=[]}
                    | _ -> {Result = "ok"; Msg=""; Data = [rateRecords]}        
    JsonSerializer.Serialize(result)


let filterRateRecord providerId currencyPairId startDate endDate startPrice endPrice =
    let rateRecords = searchRateRecord providerId currencyPairId startDate endDate startPrice endPrice    
    let result = match rateRecords.Length with
                    | 0 -> {Result = "ok"; Msg = "No data found!"; Data=[]}
                    | _ -> {Result = "ok"; Msg=""; Data = [rateRecords]}        
    JsonSerializer.Serialize(result)
        
let insertRateRecord (currencypairid:int64, datetimerate:string, price:decimal, providerid:int64) =
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
    let r = addRateRecord p
    let result = match r with
                    | None -> {Result = "error"; Msg="Error when inserting data!"; Data=None}
                    | _ -> {Result = "ok"; Msg="Rate Record inserted with success!"; Data=None}
    JsonSerializer.Serialize(result)

let changeRateRecord p =
    let r = updateRateRecord p
    let result = match r with
                    | None -> {Result = "error"; Msg="Error when updating data!"; Data=None}
                    | _ -> {Result = "ok"; Msg="Rate Record updated with success!"; Data=None}
    JsonSerializer.Serialize(result)

let excludeRateRecord id = 
    let p = getRateRecord id
    let result = match p with  
                    | None -> {Result = "error"; Msg = "Rate Record does not exist in database!"; Data = None}
                    | Some p -> if delRateRecord(p) = None then {Result = "error"; Msg = "No data deleted!"; Data = null}
                                else {Result = "ok"; Msg = "Rate Record deleted!"; Data = None } 
    JsonSerializer.Serialize(result)


(*********************************)
(* TradeRecord related functions *)
(*********************************)

let findTradeRecord id = 
    let tradeRecordData = getTradeRecord id
    let result = match tradeRecordData with
                    | None -> {Result = "ok"; Msg = "No data found!"; Data=[]}
                    | Some(tradeRecordData) -> {Result = "ok"; Msg=""; Data = [tradeRecordData]}        
    JsonSerializer.Serialize(result)    
               
let insertTradeRecord (currencypairid:int64, datetimetransaction:string, quantity:int64, traderateid:int64, typetransaction:string) =
    let tr = getRateRecord traderateid
    let p =  {  TradeRecordId = 0L;
                DateTimeTransaction = System.DateTime.Parse(datetimetransaction);
                Quantity = quantity;
                TradeRateId = traderateid;
                TypeTransaction = typetransaction;
                TradeRate = tr.Value
              } 

    let r = addTradeRecord p
    let result = match r with
                    | None -> {Result = "error"; Msg="Error when inserting data!"; Data=None}
                    | _ -> {Result = "ok"; Msg="Trade Record inserted with success!"; Data=None}
    JsonSerializer.Serialize(result)

let changeTradeRecord p =
    let r = updateTradeRecord p
    let result = match r with
                    | None -> {Result = "error"; Msg="Error when updating data!"; Data=None}
                    | _ -> {Result = "ok"; Msg="Trade Record updated with success!"; Data=None}
    JsonSerializer.Serialize(result)

let excludeTradeRecord id = 
    let p = getTradeRecord id
    let result = match p with  
                    | None -> {Result = "error"; Msg = "Trade Record does not exist in database!"; Data = None}
                    | Some p -> if delTradeRecord(p) = None then {Result = "error"; Msg = "No data deleted!"; Data = null}
                                else {Result = "ok"; Msg = "Trade Record deleted!"; Data = None } 
    JsonSerializer.Serialize(result)

