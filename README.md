# Orders Service 

## Description
This service displays the schedule for x amount of orders placed by x amount of customers

## Technologies
* C# 
* ASP.NET Core

## Endpoints
`/api/v1/schedule`- generates a schedule from the supplied request body(samples provided via swagger). NOTE: this is called via HTTP POST.  
Example of valid body is shown below:  
```
{
  "orders": [
    {
      "customerName": "john"
    },
    {
      "customerName": "phil"
    }
  ]
}
```

## Run
Note: You would need [.NET 5.0 SDK](https://dotnet.microsoft.com/download) to be installed on machine.  

Start service using the following commands:
```
cd src
dotnet run -p Orders.csproj
```

## Test
```
cd tests
dotnet test
```