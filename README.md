# ReservationApp
ISU Corp test.

## DB
I use Postgress sql for database.
## API
I do an API in asp.net core follow design patterns as ID, Repository, Dto and pagination. I define (Code first) 3 models Contact Type, Contact and reservation, with the fields explained in the document. 

## FrontEND
I do a frontend using Angular 8 with bootstrap css framework. I define models and service accoreding to backend models. The design is responsive design for mobiles device.

## Instruction
Dev code is in master dev

Clone the project:
```
git clone https://github.com/jr638091/ReservationApp.git
git checkout dev
```

Initial setup:
```
dotnet build
dotnet ef database update
```

Run site:
```
dotnet run
```

Its listening at port https://localhost:5001