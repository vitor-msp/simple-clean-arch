#!/bin/bash

command=$1

error(){
    echo "Invalid argument! Options: up, down."
    exit 1
}

if [ -z "$command" ]; then
    error
elif [ $command == "up" ] || [ $command == "down" ]; then
    sqlite3 products.db < Repository/Database/Migrations/$command.sql
else
    error
fi

exit 0