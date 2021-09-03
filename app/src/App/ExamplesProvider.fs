module ExamplesProvider
    open Library

    (* Provider examples  *)
    let displayOneProvider id =
        let p = findProvider id
        p

    // Get all providers
    let displayAllProviders =
        let p = allProviders
        p

    // Insert provider
    let insertOneProvider name shortname description =
        let p = insertProvider(0L, name, shortname, description)
        p

    let providerExamples =
        // Deleta todas os Providers
        //printfn "%s" removeAllProviders

        "\nProvider manipulation examples" |> printfn "%s"
        "==============================\n" |> printfn "%s"

        "\nDisplay all Providers" |> printfn "%s"
        displayAllProviders |> printfn "%s"

        "\nInsert one provider" |> printfn "%s"
        insertOneProvider "New Universal Series Broker" "N-USB" "New Broker USM" |> printfn "%s"

        "\nInsert one provider with empty fields" |> printfn "%s"
        insertOneProvider "" "" "Broker Description" |> printfn "%s"

        "\nExclude one provider that doesn't exists" |> printfn "%s"
        excludeProvider 1L |> printfn "%s"

