<<<<<<< HEAD
﻿# MobileApi

## Status
3/21/17 - Basic functionality, controllers still hard-coded. Need to abstract out, at all levels of the MVC framework, any static values and make it such that any data set can be requested at a generic URL and the data will be dynamically pulled from the DB. 
Ex. going to "api/plants" will get all plants from the database and "api/wetlands" will get all wetlands.

## Introduction
This app is designed as a simple API from which to pull data down to mobile devices. 

## Technology Stack
The MobileApi app utilizes the following tools:
* .Net Core, Web API
* Entity Framework 6
* NPGSQL connection to PostgreSQL database

## Local Server Settings
For testing the API, use a tool such as Fiddler, and make sure to configure your IIS Express settings properly. See this StackOverflow post for reference: http://stackoverflow.com/questions/5433786/configure-iis-express-for-external-access-to-vs2010-project.
=======
# MobileApi
Simple .Net API for RSF Mobile Applications
>>>>>>> 9b2e6d88f701177e3c5f03fc957ae5267bbf06a6
