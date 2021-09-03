module LoadData

    open Library
    open FsCheck
    open System
    open DBILib.Models.dbintegrationDomain

    // A Provider records generator
    type ProviderGenerator =
        static member GenerateRecords (providerid:int, (description, name, shortname)) =
            try
                Some {ProviderId = 0L; //int64(providerid);
                      Description = description;
                      Name = name;
                      ShortName = shortname}
            with
            | :? ArgumentOutOfRangeException -> None

        static member Generate (samples:int) =
            let provideridChoices = Gen.choose (1, 10) |> Gen.sample 0 samples
            let descriptionChoices = Gen.elements ["Provider category A"; "Provider category B"; "Provider category C"; ""] |> Gen.sample 0 samples
            let nameChoices = Gen.elements ["Complete ABS"; "Almost URB"; "Somewhat UBS"; "Maybe BRC"; "Also OEK"] |> Gen.sample 0 samples
            let shortnameChoices = Gen.elements ["ABS"; "URB"; "UBS"; "BRC"; "OEK"] |> Gen.sample 0 samples

            let l1 = List.zip3 descriptionChoices nameChoices shortnameChoices
            List.zip provideridChoices l1
            |> List.choose ProviderGenerator.GenerateRecords

    // A Provider records generator
    type CurrencyGenerator =
        static member GenerateRecords (currencyid: int, (alias, name, symbol)) =
            try
                Some { CurrencyId = 0L; //int64(currencyid);
                       Alias = alias;
                       Name = name;
                       Symbol = symbol}
            with
            | :? ArgumentOutOfRangeException -> None

        static member Generate (samples:int) =
            let currencyidChoices = Gen.choose (1,10) |> Gen.sample 0 samples
            let aliasChoices = Gen.elements ["EURO"; "USD"; "BRL"; "CNY"; "JPY"] |> Gen.sample 0 samples
            let nameChoices = Gen.elements ["Euro"; "Dolar dos Estados Unidos"; "Real"; "Yuan Chines"; "Iene Japonês"] |> Gen.sample 0 samples
            let symbolChoices = Gen.elements ["€"; "$"; "R$"; "元"; "¥"] |> Gen.sample 0 samples

            let l1 = List.zip3 aliasChoices nameChoices symbolChoices
            List.zip currencyidChoices l1
            |> List.choose CurrencyGenerator.GenerateRecords


    // A CurrencyPair records generator
    type CurrencyPairGenerator =
        static member GenerateRecords (currencypairid: int, (alias, firstcurrencyid:int, secondcurrencyid:int)) =
            try
                Some { CurrencyPairId = 0L; //int64(currencypairid);
                       Alias = alias;
                       FirstCurrencyId = int64(firstcurrencyid);
                       SecondCurrencyId = int64(secondcurrencyid) }
            with
            | :? ArgumentOutOfRangeException -> None

        static member Generate (samples:int) =
            let currencypairidChoices = Gen.choose (1,20) |> Gen.sample 0 samples
            let aliasChoices = Gen.elements ["EURO/USD"; "USD/EURO"; "BRL/USD"; "BRL/CNY"; "USD/JPY"] |> Gen.sample 0 samples
            let firtsCurrencyChoices = Gen.elements [1; 2; 3; 3; 3] |> Gen.sample 0 samples
            let secondCurrencyChoices = Gen.elements [2; 1; 2; 4; 5] |> Gen.sample 0 samples

            let l1 = List.zip3 aliasChoices firtsCurrencyChoices secondCurrencyChoices
            List.zip currencypairidChoices l1
            |> List.choose CurrencyPairGenerator.GenerateRecords

    // A RateRecord records generator
    type RateRecordGenerator =
        static member GenerateRecords (raterecordid: int, (((day,month,year), (hour,minute,second)), (currencypairid, price, providerid))) =
            try
                Some { RateRecordId = 0L; //int64(raterecordid);
                       CurrencyPairId = int64(currencypairid);
                       DateTimeRate = DateTime(year,month,day,hour,minute,second);
                       Price = decimal(price/10);
                       ProviderId = int64(providerid)
                      }
            with
            | :? ArgumentOutOfRangeException -> None

        static member Generate (samples:int) =
            let raterecordidChoices = Gen.choose (1,10) |> Gen.sample 0 samples
            let currencypairidChoices = Gen.choose (1,10) |> Gen.sample 0 samples
            let provideridChoices = Gen.choose (1, 10) |> Gen.sample 0 samples

            let years = Gen.choose (2019, 2021) |> Gen.sample 0 samples
            let months = Gen.choose (1, 12) |> Gen.sample 0 samples
            let days = Gen.choose(1, 31) |> Gen.sample 0 samples
            let hours = Gen.choose (1, 12) |> Gen.sample 0 samples
            let minutes = Gen.choose (1, 59) |> Gen.sample 0 samples
            let seconds = Gen.choose (1, 59) |> Gen.sample 0 samples
            let times = List.zip3 hours minutes seconds
            let dates = List.zip3 days months years
            let datetimerateChoices = List.zip dates times

            let priceChoices = Gen.choose (10, 100) |> Gen.sample 0 samples

            let l1 = List.zip3 currencypairidChoices priceChoices provideridChoices
            let l2 = List.zip datetimerateChoices l1
            List.zip raterecordidChoices l2
            |> List.choose RateRecordGenerator.GenerateRecords