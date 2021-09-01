namespace DBILib

module CompositionRoot =

    open DBILib.Models
    open Microsoft.EntityFrameworkCore
    open System
    open dotenv.net

    //open DBILib
    let configureContext =
        (fun () ->
            let optionsBuilder = new DbContextOptionsBuilder<dbintegrationContext>();
            optionsBuilder.UseNpgsql(Config.getEnvVar  "STRCONNECT_DB" ) |> ignore
            new dbintegrationContext(optionsBuilder.Options)
        )

    let getContext = configureContext()

    // Simple alias Provider functions
    let getProvider  = dbintegrationRepository.getProvider getContext
    let getAllProviders = dbintegrationRepository.getAllProviders getContext
    let addProvider = dbintegrationRepository.addProvider getContext
    let delProvider = dbintegrationRepository.deleteProvider getContext
    let delAllProviders = dbintegrationRepository.deleteAllProviders getContext
    let updateProvider = dbintegrationRepository.updateProvider getContext

    // Simple alias Currency functions
    let getCurrency  = dbintegrationRepository.getCurrency getContext
    let getAllCurrencies = dbintegrationRepository.getAllCurrencies getContext
    let addCurrency = dbintegrationRepository.addCurrency getContext
    let delCurrency = dbintegrationRepository.deleteCurrency getContext
    let updateCurrency = dbintegrationRepository.updateProvider getContext

    // Simple alias CurrencyPair functions
    let getCurrencyPair  = dbintegrationRepository.getCurrencyPair getContext
    let getAllCurrencyPairs = dbintegrationRepository.getAllCurrencyPairs getContext
    let addCurrencyPair = dbintegrationRepository.addCurrencyPair getContext
    let delCurrencyPair = dbintegrationRepository.deleteCurrencyPair getContext
    let updateCurrencyPair = dbintegrationRepository.updateCurrencyPair getContext

    // Simple alias RateRecord functions
    let getRateRecord  = dbintegrationRepository.getRateRecord getContext
    let getAllRateRecords = dbintegrationRepository.getAllRateRecords getContext
    let searchRateRecord = dbintegrationRepository.searchRateRecord getContext
    let addRateRecord = dbintegrationRepository.addRateRecord getContext
    let delRateRecord = dbintegrationRepository.deleteRateRecord getContext
    let updateRateRecord = dbintegrationRepository.updateRateRecord getContext
    let getRateRecordsbyProvider = dbintegrationRepository.getRateRecordsbyProvider getContext

    // Simple alias RateRecord functions
    let getTradeRecord  = dbintegrationRepository.getTradeRecord getContext
    let getAllTradeRecords = dbintegrationRepository.getAllTradeRecords getContext
    let addTradeRecord = dbintegrationRepository.addTradeRecord getContext
    let delTradeRecord = dbintegrationRepository.deleteTradeRecord getContext
    let updateTradeRecord = dbintegrationRepository.updateTradeRecord getContext
