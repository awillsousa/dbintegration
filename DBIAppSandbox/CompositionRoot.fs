module CompositionRoot

open FsEfTest
open FsEfTest.Models
open Microsoft.EntityFrameworkCore

let configureContext = 
    (fun () ->
        let optionsBuilder = new DbContextOptionsBuilder<dbintegrationContext>();
        //optionsBuilder.UseNpgsql(@"Server=.\SQLExpress;Database=Series;Integrated Security=SSPI;") |> ignore
        new dbintegrationContext(optionsBuilder.Options)
    )

let getContext = configureContext()

// Simple alias Provider functions
let getProvider  = dbintegrationRepository.getProvider getContext
let getAllProviders = dbintegrationRepository.getAllProviders getContext
let addProvider = dbintegrationRepository.addProvider getContext
let delProvider = dbintegrationRepository.deleteProvider getContext
let updateProvider = dbintegrationRepository.updateProvider getContext

// Simple alias Currency functions
let getCurrency  = dbintegrationRepository.getCurrency getContext
let getAllCurrencies = dbintegrationRepository.getAllCurrencies getContext
let addCurrency = dbintegrationRepository.addCurrency getContext
let delCurrency = dbintegrationRepository.deleteProvider getContext
let updateCurrency = dbintegrationRepository.updateProvider getContext

// Simple alias CurrencyPair functions
let getCurrencyPair  = dbintegrationRepository.getCurrency getContext
let getAllCurrencyPairs = dbintegrationRepository.getAllCurrencyPairs getContext
let addCurrencyPair = dbintegrationRepository.addCurrencyPair getContext
let delCurrencyPair = dbintegrationRepository.deleteCurrencyPair getContext
let updateCurrencyPair = dbintegrationRepository.updateCurrencyPair getContext

// Simple alias RateRecord functions
let getRateRecord  = dbintegrationRepository.getRateRecord getContext
let getAllRateRecords = dbintegrationRepository.getAllRateRecords getContext
let addRateRecord = dbintegrationRepository.addRateRecord getContext
let delRateRecord = dbintegrationRepository.deleteRateRecord getContext
let updateRateRecord = dbintegrationRepository.updateRateRecord getContext

// Simple alias RateRecord functions
let getTradeRecord  = dbintegrationRepository.getTradeRecord getContext
let getAllTradeRecords = dbintegrationRepository.getAllTradeRecords getContext
let addTradeRecord = dbintegrationRepository.addTradeRecord getContext
let delTradeRecord = dbintegrationRepository.deleteTradeRecord getContext
let updateTradeRecord = dbintegrationRepository.updateTradeRecord getContext
