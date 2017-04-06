# MobileApi
Simple .Net REST API for RSF Mobile Applications

## Status
4/6/17 - Added OData support, such that mobile devices can easily query the database using simple and flexible OData syntax. See this resource for examples: [Using OData query syntax with Web API](https://blogs.msdn.microsoft.com/martinkearn/2015/03/10/using-odata-query-syntax-with-web-api/)

3/21/17 - Basic functionality, controllers still hard-coded. Need to abstract out, at all levels of the MVC framework, any static values and make it such that any data set can be requested at a generic URL and the data will be dynamically pulled from the DB. 
Ex. going to "api/plants" will get all plants from the database and "api/wetlands" will get all wetlands.

## Technology Stack
The MobileApi app utilizes the following tools:
* .Net Core, Web API
* Entity Framework 6
* NPGSQL connection to PostgreSQL database
* OData package to handle translating query params into SQL

## Local Server Settings
For testing the API, use a tool such as Fiddler, and make sure to configure your IIS Express settings properly. See this StackOverflow post for reference: http://stackoverflow.com/questions/5433786/configure-iis-express-for-external-access-to-vs2010-project.
