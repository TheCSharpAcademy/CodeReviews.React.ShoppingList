# React Shopping List with SQL backend For The C# Academy

This application is built in two parts:
* a .Net WebAPI backend interacting with a SQL server (using EntityFramework Core)
* a React frontend served with Vite

## Requirements

* This is a CRUD Shopping List App with React and .NET Web API .
* Users should be able to cross items from the shopping-list without deleting them. You can use a IsPickedUp boolean for that.
* You should create two projects: A .NET WebApi and a React app.
* You can choose whatever database solution you want: Sqlite, SQL server or whatever you're comfortable with.
* You can choose whatever ORM you want: Dapper, EF, ADO.NET.
* To keep it simple, don't create a project that allows multiple shopping lists.
* Your database should have a single "ShoppingListItems" table. The objective is to focus on React, so we should avoid the complexities of relational data. 
* You CANNOT use Axios or Redux.

## Features

* SQL Database connection
* React front-end Vite
* Ability toggle checked off functionality

## Instructions

1. Update the connection string in `appsettings.json`
2. Run the backend WebAPI
3. Run the frontend WebUI using `vite`