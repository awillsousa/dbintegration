namespace WebApp.Controllers

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
//open WebApp
open Library

[<ApiController>]
[<Route("api/[controller]")>]
type ProviderController (logger : ILogger<ProviderController>) =
    inherit ControllerBase()


    [<HttpGet>]
    member _.Get() =
        let providers = allProviders
        let result = [| providers |]
        ActionResult<string[]>(result)

    [<HttpGet("id")>]
    member _.Get(id: int) =
        let provider = findProvider (int64(id))
        let result = [| provider |]
        ActionResult<string[]>(result)