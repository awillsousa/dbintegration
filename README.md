### Database Integration

An abstraction layer in data access provided by F# library. 

The purpose of this application is to serve as an abstraction layer to data access, 
through an API that can bring to endpoints independence of database and without 
the need to write complex SQL queries. 

#### Application Structure

The application structure has 3 parts:

<ul>
<li>Library: contains the database layer manipulation with high level functions</li>
<li>App: a CLI application to load some data into database using the Library functions and also show some data manipulation examples into the various tables</li>
<li>WebApp: an API that receive requests and using the Library manipulate data and retrive information from database</li>
</ul>


#### Installation

To use install the application infrastructure, you will need:

<ol>
  <li>Docker
  <li>Docker Compose</li>
  <li>This code</li>
</ol>

To start the database, you can use docker-compose `docker-compose up -d`

It's possible to execute the database containers and applications using separated files:
- `docker-compose -f docker-compose-db.yml up -d` for database and PgAdmin interface
- `docker-compose -f docker-compose-app.yml up -d` for F# API and application

There is a easy option, that using scripts:

`./scripts/start_db.sh` for database and PgAdmin interface
`./scripts/start_app.sh` for F# API and application

run_cli_load_data.sh
run_cli_show_data.sh
start_app.sh
start_db.sh


#### Execution
To execute the CLI application tests, you can run:
- `./scripts/run_cli_load_data.sh` to load data into Postgres database
- `./scripts/run_cli_show_data.sh` to show some data manipulation examples 

To test the API, just use curl or some application like Insomnia or Postman.

#### Examples of Use

<TODO>

