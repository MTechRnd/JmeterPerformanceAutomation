#!/bin/bash

/opt/mssql/bin/sqlservr &

/opt/mssql-tools/bin/sqlcmd -S localhost,1433 -U sa -P Password1234 -Q "CREATE DATABASE ODataAPIDatabase" -d master 
/opt/mssql-tools/bin/sqlcmd -S localhost,1433 -U sa -P Password1234 -d ODataAPIDatabase -i /app/script/script.sql 

tail -f /dev/null