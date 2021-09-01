module Sample

open FSharp.Data.Sql

type Sql = SqlDataProvider<
            Npgsql.EntityFrameworkCore.PostgreSQL,
            "Host=localhost;Port=15432;Database=dbintegration;Username=dbintegration;Password=fD$#d143da">

let context = Sql.GetDataContext()

let res = 
    query {
        for c in context.Dbo.Users do
            select c.FirstName
    } |> Seq.toList
