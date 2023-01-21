
# Adopt Pet

This is a portal for animal adoption ads.

## Tech Stack

**Client:** Razor Pages, Bootstrap, JQuery

**Server:** ASP.NET, SQL Server 

## Features

- user registration and login
- four user roles:
    - non-logged-in user can search for ads and display them
    - logged in user can do what above and add new ads, write messages and add interesting ads to private watch list
    - a moderator can change the status of announcements, i.e. make them visible to other users or delete them
    - administrator can add new species and breeds to the application, give and take away moderator rights from other users
- validation of forms for invalid data
- unique link to the ad generated based on the title and a few random characters
- autocomplete of town names
- message statuses and information about not being read


## Run Locally

Clone the project

```bash
  git clone https://github.com/asdros/AdoptPet_Razor.git
```

Run the solution file

Apply defined database migrations

```bash
  Update-database
```

Run the sql_script.sql with the database data

Start the application

```bash
  dotnet run --project AdoptPet
```

Go to localhost

```bash
https://localhost:5001/
```


## Demo

[Deployed on Azure](https://adoptpet.azurewebsites.net/)

## Screenshots

1. Home page

<img src="https://i.imgur.com/GRnynjo.png" width="60%" height="75%">
<br/>
2. Search engine

<img src="https://i.imgur.com/nNzHoRA.png" width="60%" height="75%">
<br/>
3. Form for adding a new ad with visible validation

<img src="https://i.imgur.com/9wrWwgv.png" width="60%" height="75%">
<br/>
4. View list of observed ads

<img src="https://i.imgur.com/cjE4WdP.png" width="60%" height="75%">
<br/>
5. Preview of the added ad

<img src="https://i.imgur.com/DpCUalq.png" width="60%" height="60%">
<br/>
6. Admin panel

<img src="https://i.imgur.com/pATsJV7.png" width="60%" height="60%">

