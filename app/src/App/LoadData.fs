module LoadData

    open Library
    open FsCheck
    open System
    open DBILib.Models.dbintegrationDomain

    type QueryRequest = {Symbol: string; StartDate: DateTime; EndDate: DateTime}

    type Tweet =
        static member GenerateRecords ((year, month, day), symbol) =
            try
                let startDate = DateTime (year, month, day)
                let endDate = startDate.AddDays 1.0
                Some {Symbol = symbol; StartDate = startDate; EndDate = endDate}
            with
            | :? ArgumentOutOfRangeException -> None

        static member Generate () =
            let years = Gen.choose (2019, 2021) |> Gen.sample 0 10
            let months = Gen.choose (1, 12) |> Gen.sample 0 10
            let days = Gen.choose(1, 31) |> Gen.sample 0 10
            let symbols = Gen.elements ["ORCL"; "IBM"; "AAPL"; "GOOGL"] |> Gen.sample 0 10
            let dates = List.zip3 years months days
            List.zip dates symbols
            |> List.choose Tweet.GenerateRecords

    // A Provider records generator
    type ProviderGenerator =
        static member GenerateRecords (providerid:int, (description, name, shortname)) =
            try
                Some {ProviderId = int64(providerid); Description = description; Name = name; ShortName = shortname}
            with
            | :? ArgumentOutOfRangeException -> None

        static member Generate () =
            let provideridChoices = Gen.choose (1, 10) |> Gen.sample 0 10
            let descriptionChoices = Gen.elements ["Provider category A"; "Provider category B"; "Provider category C"; ""] |> Gen.sample 0 10
            let nameChoices = Gen.elements ["Complete ABS"; "Almost URB"; "Somewhat UBS"; "Maybe BRC"; "Also OEK"] |> Gen.sample 0 10
            let shortnameChoices = Gen.elements ["ABS"; "URB"; "UBS"; "BRC"; "OEK"] |> Gen.sample 0 10


            let l1 = List.zip3 descriptionChoices nameChoices shortnameChoices
            List.zip provideridChoices l1
            |> List.choose ProviderGenerator.GenerateRecords

    // A Provider records generator
    type CurrencyGenerator =
        static member GenerateRecords (currencyid: int, (alias, name, symbol)) =
            try
                Some {CurrencyId = int64(currencyid); Alias = alias; Name = name; Symbol = symbol}
            with
            | :? ArgumentOutOfRangeException -> None

        static member Generate () =
            let currencyidChoices = Gen.choose (1, 5) |> Gen.sample 0 5
            let aliasChoices = Gen.elements ["EURO"; "USD"; "BRL"; "CNY"; "JPY"] |> Gen.sample 0 5
            let nameChoices = Gen.elements ["Euro"; "Dolar dos Estados Unidos"; "Real"; "Yuan Chines"; "Iene Japonês"] |> Gen.sample 0 5
            let symbolChoices = Gen.elements ["€"; "$"; "R$"; "元"; "¥"] |> Gen.sample 0 5

            let l1 = List.zip3 aliasChoices nameChoices symbolChoices
            List.zip currencyidChoices l1
            |> List.choose CurrencyGenerator.GenerateRecords