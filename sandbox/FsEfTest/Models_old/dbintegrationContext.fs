namespace FsEfTest.Models

open System
open System.Collections.Generic
open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Metadata
open EntityFrameworkCore.FSharp.Extensions
open FsEfTest.Config
open dbintegrationDomain

type dbintegrationContext =
    inherit DbContext

    new() = { inherit DbContext() }
    new(options : DbContextOptions<dbintegrationContext>) =
        { inherit DbContext(options) }

    [<DefaultValue>] val mutable private _Contacts : DbSet<Contact>
    member this.Contacts with get() = this._Contacts and set v = this._Contacts <- v

    [<DefaultValue>] val mutable private _Users : DbSet<User>
    member this.Users with get() = this._Users and set v = this._Users <- v


    override this.OnConfiguring(optionsBuilder: DbContextOptionsBuilder) =
        if not optionsBuilder.IsConfigured then
            let strConnection = FsEfTest.Config.getEnvVar "STRCONNECT_DB"            
            optionsBuilder.UseNpgsql(strConnection) |> ignore
            //optionsBuilder.UseNpgsql("Host=localhost;Port=15432;Database=dbintegration;Username=dbintegration;Password=fD$#d143da") |> ignore
            ()

    override this.OnModelCreating(modelBuilder: ModelBuilder) =
        base.OnModelCreating(modelBuilder)

        modelBuilder.HasAnnotation("Relational:Collation", "en_US.utf8")
            |> ignore


        modelBuilder.Entity<Contact>(fun entity ->

            entity.ToTable("contacts")
                |> ignore

            entity.Property(fun e -> e.Id)
                .HasColumnName("id")
                |> ignore

            entity.Property(fun e -> e.Email)
                .HasColumnName("email")
                |> ignore

            entity.Property(fun e -> e.Firstname)
                .HasColumnName("firstname")
                |> ignore

            entity.Property(fun e -> e.Lastname)
                .HasColumnName("lastname")
                |> ignore
        ) |> ignore

        modelBuilder.Entity<User>(fun entity ->

            entity.ToTable("users")
                |> ignore

            entity.Property(fun e -> e.UserId)
                .HasColumnName("user_id")
                |> ignore

            entity.Property(fun e -> e.Active)
                .IsRequired()
                .HasColumnName("active")
                .HasDefaultValueSql("true")
                |> ignore

            entity.Property(fun e -> e.Email)
                .HasColumnName("email")
                |> ignore

            entity.Property(fun e -> e.FirstName)
                .HasColumnName("first_name")
                |> ignore

            entity.Property(fun e -> e.LastName)
                .HasColumnName("last_name")
                |> ignore

            entity.Property(fun e -> e.Username)
                .HasColumnName("username")
                |> ignore
        ) |> ignore

        modelBuilder.RegisterOptionTypes()
