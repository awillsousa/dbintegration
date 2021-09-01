// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open Npgsql.FSharp
open Npgsql

// Define a function to construct a message to print
let from whom =
    sprintf "from %s" whom

let connectionString : string =
    Sql.host "localhost"
    |> Sql.database "dbintegration"
    |> Sql.username "dbintegration"
    |> Sql.password "fD$#d143da"
    |> Sql.port 15432
    |> Sql.formatConnectionString

//let dbConnection = new NpgsqlConnection(connectionString)


type User = { Id: int; 
              Username: string; 
              Email: string;
              FirstName: string option;
              LastName: string option }

let readUsers (connectionString: string) : User list =
    connectionString
    |> Sql.connect
    |> Sql.query "SELECT * FROM users"
    |> Sql.execute (fun read ->
        {
            Id = read.int "user_id"
            Username = read.text "username"
            Email = read.text "email"
            FirstName = read.textOrNone "first_name"
            LastName = read.textOrNone "last_name"
        })

let readUser (connectionString: string, username: string, email: string) : User =
    connectionString
    |> Sql.connect
    |> Sql.query "SELECT * FROM users WHERE username = @username OR email = @email"
    |> Sql.parameters [ "@username", Sql.text username; "@email", Sql.text email ]
    |> Sql.executeRow (fun read ->
        {
            Id = read.int "user_id"
            Username = read.text "username"
            Email = read.text "email"
            FirstName = read.textOrNone "first_name"
            LastName = read.textOrNone "last_name"
        })

let activeUsers (connectionString: string) : User list =
    connectionString
    |> Sql.connect
    |> Sql.query "SELECT * FROM users WHERE active = @active"
    |> Sql.parameters [ "@active", Sql.bool true ]
    |> Sql.execute (fun read ->
        {
            Id = read.int "user_id"
            Username = read.text "username"
            Email = read.text "email"
            FirstName = read.textOrNone "first_name"
            LastName = read.textOrNone "last_name"
        })

let activeUsersTryFinally (connectionString: string) : User list =    
    let dbConnection = new NpgsqlConnection(connectionString)
    try        
        dbConnection.Open()
        dbConnection
        |> Sql.existingConnection
        |> Sql.query "SELECT * FROM users WHERE active = @active"
        |> Sql.parameters [ "@active", Sql.bool true ]
        |> Sql.execute (fun read ->
            {
                Id = read.int "user_id"
                Username = read.text "username"
                Email = read.text "email"
                FirstName = read.textOrNone "first_name"
                LastName = read.textOrNone "last_name"
            })
    finally
        dbConnection.Dispose()

// Insert one row in users table
let addUser (connectionString: string, user_id: int, username:string, email:string) : int =
    connectionString
    |> Sql.connect
    |> Sql.query "INSERT INTO users (user_id, username, email) VALUES (@user_id, @username, @email)"
    |> Sql.parameters ["@user_id", Sql.int user_id; "@username", Sql.text username; "@email", Sql.text email ]
    |> Sql.executeNonQuery

// Insert one row an return the last inserted line
let addUserAndShow (connectionString: string, username:string, email:string) : User =
    connectionString
    |> Sql.connect
    |> Sql.query "INSERT INTO users (username, email) VALUES (@username, @email) RETURNING *"
    |> Sql.parameters [ "@username", Sql.text username; "@email", Sql.text email ]
    |> Sql.executeRow (fun read ->
        {
            Id = read.int "user_id" // the generated user id
            Username = read.text "username"
            Email = read.text "email"
            FirstName = read.textOrNone "first_name"
            LastName = read.textOrNone "last_name"
        })
   

// Count all users in table
let numberOfUsers (connectionString: string) : int64 =
    connectionString
    |> Sql.connect
    |> Sql.query "SELECT COUNT(*) AS user_count FROM users"
    |> Sql.executeRow (fun read -> read.int64 "user_count")

// Batch operations
let executeBatch(connectionString: string) =
    connectionString
    |> Sql.connect
    |> Sql.executeTransaction
        [
            // This query is executed 3 times
            // using three different set of parameters
            "INSERT INTO users (username, email, first_name, last_name) VALUES (@username, @email, @first_name, @last_name)", [
                [ "@username", Sql.text "diamond"; "@email", Sql.text "diamond@hotmail.com"; 
                  "@first_name", Sql.text "Diamond"; "@last_name", Sql.text "Brute"]
                [ "@username", Sql.text "cristalbaby"; "@email", Sql.text "cristalbaby@yahoo.com"; 
                  "@first_name", Sql.text "Marble"; "@last_name", Sql.text "Cristal"]
                [ "@username", Sql.text "littlerock"; "@email", Sql.text "littlerock@gmail.com"; 
                  "@first_name", Sql.text "Brandon"; "@last_name", Sql.text "Banner"]
            ]            
        ]

// Iterating result set
let readUsersIter (connectionString: string) : ResizeArray<User> = 
    let users = ResizeArray<User>()
    connectionString
    |> Sql.connect
    |> Sql.query "SELECT * FROM users"
    |> Sql.iter (fun read ->
        users.Add {
            Id = read.int "user_id"
            Username = read.text "username"
            Email = read.text "email"
            FirstName = read.textOrNone "first_name"
            LastName = read.textOrNone "last_name"
        })

    users

// Check if username of email already exists
let [<Literal>] usernameOrEmailExistQuery = """
    SELECT EXISTS (
        SELECT 1 
          FROM users
         WHERE username = @username OR email = @email    
    ) AS username_or_email_already_exist
"""

let usernameOrEmailAlreadyExist (connectionString: string, username: string, email: string) : bool =     
    let dbConnection = new NpgsqlConnection(connectionString)
    try        
        dbConnection.Open()
        dbConnection
        |> Sql.existingConnection
        |> Sql.query usernameOrEmailExistQuery
        |> Sql.parameters [ "@username", Sql.text username; "@email", Sql.text email ]
        |> Sql.executeRow (fun read -> read.bool "username_or_email_already_exist")
    
    finally
        dbConnection.Dispose()

let users = readUsers connectionString

[<EntryPoint>]
let main argv =
    //let message = from "F#" // Call the function
    //printfn "Hello world %s" message
    for user in users do
        printfn "User(%d) -> {%s}" user.Id user.Email
    
    let username = "campbilt"
    let email = "campbilt@hotmail.com"    
    
    let userExists = usernameOrEmailAlreadyExist (connectionString, username, email)  
    if not userExists then 
        let r = addUserAndShow(connectionString, username, email)
        printfn "\nInserted:"
        printfn "User(%d) - {%A},{%A} -> {%s}" r.Id r.LastName r.FirstName r.Email
    else
        let r = readUser(connectionString, username, email)
        printfn "\nUser already exists!"
        printfn "User(%d) - {%A},{%A} -> {%s}" r.Id r.LastName r.FirstName r.Email

    //let r = addUserAndShow(connectionString, username, email, userExists) 
    
    let activesAfter = activeUsersTryFinally connectionString

    printfn "\nActive Users: "
    for user in activesAfter do
        printfn "User(%d) - {%A},{%A} -> {%s}" user.Id user.LastName user.FirstName user.Email
    
    let totalUsers = numberOfUsers connectionString
    printfn "\nTotal of Users (%d)" totalUsers

    0 // return an integer exit code


