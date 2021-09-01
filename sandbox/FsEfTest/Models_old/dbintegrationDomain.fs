namespace FsEfTest.Models

open System
open System.Collections.Generic

module rec dbintegrationDomain = 

    [<CLIMutable>]
    type Contact = {
        Id: Int64
        Email: string
        Firstname: string
        Lastname: string
    }

    [<CLIMutable>]
    type User = {
        UserId: int
        Active: bool option
        Email: string
        FirstName: string
        LastName: string
        Username: string
    }

