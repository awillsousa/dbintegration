#!/bin/bash
#
# Stop the container of F# application
# Stop the database Postgres and PgAdmin
#
# Autor: A. Willian
# Created: 02/09/2021
# Modified: 

docker-compose -f docker-compose-app.yml down

exit 0
