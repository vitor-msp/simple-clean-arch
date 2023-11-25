#!/bin/bash

error(){
    echo "Invalid argument! Options: up, down."
    exit 1
}

if [ -z "$1" ]; then
    error
elif [ $1 == "up" ]; then
    sqlite3 products.db < Repository/Database/Migrations/up.sql
elif [ $1 == "down" ]; then
    sqlite3 products.db < Repository/Database/Migrations/down.sql
else
    error
fi

exit 0