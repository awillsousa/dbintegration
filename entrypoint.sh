#!/bin/sh

if [ "$DATABASE" = "dbintegration" ]
then
    echo "Waiting for Database Server..."

    while ! nc -z $POSTGRES_HOST $POSTGRES_PORT; do
      sleep 1
    done

    echo "Database Server running OK"
fi

exec "$@"
