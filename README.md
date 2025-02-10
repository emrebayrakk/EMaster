# Clean Architecture (EMaster)
Clean Architecture Simple Income Expense project for ASP.NET Core. Clean Architecture is the latest in a series of architectures aiming towards a loosely-coupled, dependency-inverted architecture. You may also hear it referred to as Hexagonal, ports-and-adapters, or onion architecture.

## Give a start! ⭐
If you are using or like this project, you can support it by giving a star. Thank you!

## Versions
The project currently uses .NET version 9.

## Getting Started

It is a simple user and company based income and expense project. An API is designed to easily manage income and expense data in a categorised way.
Scaler page view is as follows.

![api](https://github.com/emrebayrakk/EMaster/blob/master/backend/EMaster.Infrastructure/Ekran%20g%C3%B6r%C3%BCnt%C3%BCs%C3%BC%202025-01-06%20205601.PNG)

## Project Content

### Architectural Structure
- Architectural Pattern: Clean Architecture
- Design Patterns:
  - Result Pattern
  - Repository Pattern
  - REPR(Request-Endpoint-Response)

### Libraries Used in the Project

- EntityFrameworkCore
- Mapster
- JWT
- Scalar

## Installation and Use

1. Clone the repo:
   ``` 
   git clone https://github.com/emrebayrakk/EMaster.git
   cd EMaster
   ```

The project is initially configured with MSSQL. If you want to continue with MSSQL, adjust the ConnectionStrings section in the `appsetting.json` file according to your settings.
![sql](https://github.com/emrebayrakk/EMaster/blob/master/backend/EMaster.Infrastructure/sql.PNG)

