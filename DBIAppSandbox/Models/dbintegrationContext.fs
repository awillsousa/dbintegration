namespace FsEfTest.Models

open System
open System.Collections.Generic
open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Metadata
open EntityFrameworkCore.FSharp.Extensions

open dbintegrationDomain

type dbintegrationContext =
    inherit DbContext

    new() = { inherit DbContext() }
    new(options : DbContextOptions<dbintegrationContext>) =
        { inherit DbContext(options) }
    
    [<DefaultValue>] val mutable private _Currencies : DbSet<Currency>
    member this.Currencies with get() = this._Currencies and set v = this._Currencies <- v

    [<DefaultValue>] val mutable private _CurrencyPairs : DbSet<CurrencyPair>
    member this.CurrencyPairs with get() = this._CurrencyPairs and set v = this._CurrencyPairs <- v

    [<DefaultValue>] val mutable private _Providers : DbSet<Provider>
    member this.Providers with get() = this._Providers and set v = this._Providers <- v

    [<DefaultValue>] val mutable private _RateRecords : DbSet<RateRecord>
    member this.RateRecords with get() = this._RateRecords and set v = this._RateRecords <- v

    [<DefaultValue>] val mutable private _TradeRecords : DbSet<TradeRecord>
    member this.TradeRecords with get() = this._TradeRecords and set v = this._TradeRecords <- v
    

    override this.OnConfiguring(optionsBuilder: DbContextOptionsBuilder) =
        if not optionsBuilder.IsConfigured then
            //optionsBuilder.UseNpgsql("Host=localhost;Port=15432;Database=fseftest;Username=dbintegration;Password=fD$#d143da") |> ignore
            optionsBuilder.UseNpgsql(FsEfTest.Config.getEnvVar "STRCONNECT_DB") |> ignore
            ()

    override this.OnModelCreating(modelBuilder: ModelBuilder) =
        base.OnModelCreating(modelBuilder)

        modelBuilder.HasAnnotation("Relational:Collation", "en_US.utf8")
            |> ignore

        
        modelBuilder.Entity<Currency>(fun entity ->

            entity.ToTable("Currency")
                |> ignore

            entity.Property(fun e -> e.CurrencyId)
                .HasColumnName("CurrencyID")
                |> ignore

            entity.Property(fun e -> e.Alias)
                |> ignore

            entity.Property(fun e -> e.Name)
                |> ignore

            entity.Property(fun e -> e.Symbol)
                |> ignore
        ) |> ignore

        modelBuilder.Entity<CurrencyPair>(fun entity ->

            entity.ToTable("CurrencyPair")
                |> ignore

            entity.Property(fun e -> e.CurrencyPairId)
                .HasColumnName("CurrencyPairID")
                |> ignore

            entity.Property(fun e -> e.Alias)
                |> ignore

            entity.Property(fun e -> e.FirstCurrencyId)
                .HasColumnName("FirstCurrencyID")
                |> ignore

            entity.Property(fun e -> e.SecondCurrencyId)
                .HasColumnName("SecondCurrencyID")
                |> ignore

            entity.HasOne(fun d -> d.FirstCurrency)
                .WithMany("CurrencyPairFirstCurrencies")
                .HasForeignKey([| "FirstCurrencyId" |])
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CurrenyID_1")
                |> ignore

            entity.HasOne(fun d -> d.SecondCurrency)
                .WithMany("CurrencyPairSecondCurrencies")
                .HasForeignKey([| "SecondCurrencyId" |])
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CurrenyID_2")
                |> ignore
        ) |> ignore

        modelBuilder.Entity<Provider>(fun entity ->

            entity.ToTable("Provider")
                |> ignore

            entity.Property(fun e -> e.ProviderId)
                .HasColumnName("ProviderID")
                |> ignore

            entity.Property(fun e -> e.Description)
                |> ignore

            entity.Property(fun e -> e.Name)
                |> ignore

            entity.Property(fun e -> e.ShortName)
                |> ignore
        ) |> ignore

        modelBuilder.Entity<RateRecord>(fun entity ->

            entity.ToTable("RateRecord")
                |> ignore

            entity.Property(fun e -> e.RateRecordId)
                .HasColumnName("RateRecordID")
                |> ignore

            entity.Property(fun e -> e.CurrencyPairId)
                .HasColumnName("CurrencyPairID")
                |> ignore

            entity.Property(fun e -> e.DateTimeRate)
                .HasColumnType("date")
                |> ignore



            entity.Property(fun e -> e.Price)
                |> ignore

            entity.Property(fun e -> e.ProviderId)
                .HasColumnName("ProviderID")
                |> ignore

            entity.HasOne(fun d -> d.CurrencyPair)
                .WithMany("RateRecords")
                .HasForeignKey([| "CurrencyPairId" |])
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CurrencyPair_ID")
                |> ignore

            entity.HasOne(fun d -> d.Provider)
                .WithMany("RateRecords")
                .HasForeignKey([| "ProviderId" |])
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProviderID")
                |> ignore
        ) |> ignore

        modelBuilder.Entity<TradeRecord>(fun entity ->

            entity.ToTable("TradeRecord")
                |> ignore

            entity.Property(fun e -> e.TradeRecordId)
                .HasColumnName("TradeRecordID")
                |> ignore

            entity.Property(fun e -> e.DateTimeTransaction)
                |> ignore

            entity.Property(fun e -> e.Quantity)
                |> ignore

            entity.Property(fun e -> e.TradeRateId)
                .ValueGeneratedOnAdd()
                .HasColumnName("TradeRateID")
                |> ignore

            entity.Property(fun e -> e.TypeTransaction)
                |> ignore

            entity.HasOne(fun d -> d.TradeRate)
                .WithMany("TradeRecords")
                .HasForeignKey([| "TradeRateId" |])
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TradeRate_ID")
                |> ignore
        ) |> ignore

        
        modelBuilder.RegisterOptionTypes()
