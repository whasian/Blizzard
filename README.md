Blizzard
========

blizzard mvc test

The project is split into 5 parts. 
The Core project which handles the objects themselves and any selfcontained logic. 
The MVC project which contains the website.
The Service project contains the logic that the MVC project calls to do all functionality.
The Repository project contains the logic to persist the data. (currently in a text file login.json held in the MVC project for simplicity)
The Unit test project is exactly what you think it is.

There are also some admin functionality in the site. In order to make your user an admin, after registering open the login.json file and change the "isAdmin" property from "false" to "true" and then log off and log back in.
