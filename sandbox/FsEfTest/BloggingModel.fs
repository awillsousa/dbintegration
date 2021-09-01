module BloggingModel

open System.ComponentModel.DataAnnotations
open Microsoft.EntityFrameworkCore
open EntityFrameworkCore.FSharp.Extensions
open Microsoft.FSharp.Data.TypeProviders
open Config

[<CLIMutable>]
type Blog = {
    [<Key>] Id: int
    Url: string
}

type BloggingContext() =  
    inherit DbContext()
    
    [<DefaultValue>] val mutable blogs : DbSet<Blog>
    member this.Blogs with get() = this.blogs and set v = this.blogs <- v

    override _.OnModelCreating builder =
        builder.RegisterOptionTypes() // enables option values for all entities

    override __.OnConfiguring(options: DbContextOptionsBuilder) : unit =
        let strConnection = Config.getEnvVar "STRCONNECT_DB"
        options.UseNpgsql(strConnection) |> ignore        
        //options.UseSqlite("Data Source=blogging.db") |> ignore        