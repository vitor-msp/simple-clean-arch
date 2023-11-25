# Simple Clean Arch

#### Simple project template using clean architecture developed in C#

## Execution

1. Clone this repository
```
git clone https://github.com/vitor-msp/simple-clean-arch.git
```

2. Access the downloaded folder
```
cd simple-clean-arch
```

3. Restore the .NET dependencies
```
dotnet restore
```

4. Create database
```
./create_database.sh
```

5. Run migrations
```
./migrations.sh up
```

6. Run API
```
dotnet run --project Api/
```

7. Use [VSCode Rest Client](tests-api.http) to test

## Notes

1. The ports 7043 and 5142 in your machine must be free