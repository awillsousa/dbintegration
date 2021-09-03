namespace DBILib

open System
open System.Collections.Generic
open DBILib.CompositionRoot
open DBILib.Models.dbintegrationDomain
open System.Globalization

module dbintegrationValidation =

    (**********************)
    (*  Helper functions  *)
    (**********************)

    // Check if a string is a valid datetime
    let validDateTime (d:string) =
        let valid =
            match System.DateTime.TryParse(d) with
            | true,_ -> true
            | false,_ -> false
        valid

    // Check if a string is a valid float
    let validFloat (d:string) =
        let valid =
            match System.Double.TryParse d with
            | true,_ -> true
            | false,_ -> false
        valid

    // Check if a string is a valid int
    let validInt (d:string) =
        let valid =
                match System.Int64.TryParse d with
                | true,_ -> true
                | false,_ -> false
        valid


    (***************************)
    (*  Provider Validations   *)
    (***************************)

    // Validate provider record field values
    let validateProvider idprovider name shortname description =
        let errors = seq {
            if String.IsNullOrEmpty name || String.IsNullOrWhiteSpace name then yield "Name should not be empty."
            if String.IsNullOrEmpty shortname || String.IsNullOrWhiteSpace shortname then yield "ShortName should not be empty."
        }

        if (Seq.isEmpty errors) then
            Ok { ProviderId = idprovider;
                 Name = name;
                 ShortName = shortname;
                 Description = description;
                 //RateRecords =  List<RateRecord>([])
               }
        else
            Error errors

    // Create provider record
    let createProvider idprovider datetimerate price providerid =
        validateProvider idprovider datetimerate price providerid

    (*****************************)
    (*   Currency Validations    *)
    (*****************************)

    // Validate currency record field values
    let validateCurrency idcurrency alias name symbol =
        let errors = seq {
            if String.IsNullOrEmpty alias || String.IsNullOrWhiteSpace alias then yield "Alias should not be empty."
            if String.IsNullOrEmpty name || String.IsNullOrWhiteSpace name then yield "Name should not be empty."
            if String.IsNullOrEmpty symbol || String.IsNullOrWhiteSpace symbol then yield "Name should not be empty."
        }

        if (Seq.isEmpty errors) then
            Ok { CurrencyId = idcurrency;
                 Name = name;
                 Alias = alias;
                 Symbol = symbol;
                 //CurrencyPairFirstCurrencies =  List<CurrencyPair>([]);
                 //CurrencyPairSecondCurrencies = List<CurrencyPair>([])
               }
        else
            Error errors

    // Create provider record
    let createCurrency idcurrency alias name symbol =
        validateCurrency idcurrency alias name symbol

    (*********************************)
    (*   CurrencyPair Validations    *)
    (*********************************)

    // Validate currency record field values
    let validateCurrencyPair idcurrencypair alias firstcurrencyid secondcurrencyid =
        let relatedFirstCurr = getCurrency firstcurrencyid
        let relatedSecondCurr = getCurrency secondcurrencyid

        let errors = seq {
            if String.IsNullOrEmpty alias || String.IsNullOrWhiteSpace alias then yield "Alias should not be empty."
            if relatedFirstCurr = None then yield "CurrencyId does not exists in database!"
            if relatedSecondCurr = None then yield "CurrencyId does not exists in database!"
        }

        if (Seq.isEmpty errors) then
            Ok { CurrencyPairId = idcurrencypair;
                 Alias = alias;
                 FirstCurrencyId = firstcurrencyid;
                 SecondCurrencyId = secondcurrencyid;
                 //FirstCurrency = relatedFirstCurr.Value;
                 //SecondCurrency = relatedSecondCurr.Value;
                 //RateRecords = List<RateRecord>()
                }

        else
            Error errors

    // Create provider record
    let createCurrencyPair idcurrencypair alias firstcurrencyid secondcurrencyid =
        validateCurrencyPair idcurrencypair alias firstcurrencyid secondcurrencyid

    (*****************************)
    (*  RateRecord Validations   *)
    (*****************************)
    //let validateRateRecord (idraterecord:int64, currencypairid:int64, datetimerate:string, price:decimal, providerid:int64) =
    let validateRateRecord idraterecord currencypairid datetimerate price providerid =
        let relatedCurrencyPair = getCurrencyPair currencypairid
        let relatedProvider = getProvider providerid

        let errors = seq {
            if not (validDateTime datetimerate) then yield "DateTimeRate with incorrect format."
            if relatedCurrencyPair = None then yield "CurrencyPairId does not exists in database!"
            if relatedProvider = None then yield "ProviderId does not exists in database!"
        }

        if (Seq.isEmpty errors) then
            let format = "dd/MM/yyyy HH:mm:ss"
            let provider = CultureInfo.InvariantCulture

            Ok { RateRecordId = idraterecord;
                 CurrencyPairId = currencypairid;
                 DateTimeRate = System.DateTime.ParseExact(datetimerate, format, provider);
                 Price = price;
                 ProviderId = providerid;
                 //TradeRecords = List<TradeRecord>()
                 //Provider = relatedProvider.Value
                 //CurrencyPair = relatedCurrencyPair.Value
                 }
        else
            Error errors

    // Create RateRecord record
    let createRateRecord idraterecord currencypairid datetimerate price providerid =
        validateRateRecord idraterecord currencypairid datetimerate price providerid

    (*****************************)
    (*  TradeRecord Validations  *)
    (*****************************)

    let validateTradeRecord idtraderecord datetimetransaction quantity raterecordid typetransaction =
        let relatedRateRecord = getRateRecord raterecordid

        let errors = seq {
            if not (validDateTime datetimetransaction) then yield "DateTimeTransaction with incorrect format."
            if relatedRateRecord = None then yield "RateRecordId does not exists in database!"
        }

        if (Seq.isEmpty errors) then
            let format = "dd/MM/yyyy HH:mm:ss"
            let provider = CultureInfo.InvariantCulture

            Ok { TradeRecordId = idtraderecord;
                 DateTimeTransaction = System.DateTime.ParseExact(datetimetransaction, format, provider);
                 Quantity = quantity;
                 TradeRateId = raterecordid;
                 TypeTransaction = typetransaction;
               }
        else
            Error errors

    // Create RateRecord record
    let createTradeRecord idtraderecord datetimetransaction quantity raterecordid typetransaction =
        validateTradeRecord idtraderecord datetimetransaction quantity raterecordid typetransaction


