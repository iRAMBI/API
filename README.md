# API

DO NOT USE VS's INTEGRATED GIT. BE A CONSOLE WIZARD.

Ben's Dev API base URL:
api.thunderchicken.ca/

-note if this route is not working try using api.thunderchicken.ca:8080

#Setup
 1. Download the repo. Load it into VS. Build the project. You will need to configure your connections to your database and generate a Web.config file
 2. In App_Data is a build script. run it in MSSQL to load your database. The script needs a user named ben and alan to work correctly. Add them as users with read / write / update / delete access to the DB.
 3. You may need to create a user that is both a windows authenticated user in MSSQL and a ApplicationPool user in IIS depending on your configuration setup. The project was last setup with the DB being localhost so assume any set settings in the project are expecting that



#Routes Reference
See oneNote For Full Documentation

##Authentication
####POST Authenticate
Route: /api/auth <br>
Body: <br>
````json
{ 
  "email": String,
  "password": String
}
````
####POST Submit Apple Toke
Route: /api/auth/:userid/appletoken/:appletoken/:token <br>
Body: There is no body content to send

##Newsfeed
####GET Standard Newsfeed
Route: GET /api/newsfeed/:userid/standard/:token <br>
Body: There is no body content to send
####GET Critical Newsfeed
Route: GET /api/newsfeed/critical <br>
Body: There is no body content to send
####GET Course Section Newsfeed
Route: GET /api/newsfeed/:userid/coursesection/:coursesectionid/:token
Body: There is no body content to send
####GET Single News Article
Route: GET /api/newsfeed/:userid/article/:newsid/:token <br>
Body: There is no body content to send
####POST News Article
Route: POST /api/newsfeed/:userid/article/:token <br>
Body: <br>
````json
{
  "coursesectionid": Number,
  "title": String,
  "content": String,
  "priority": String,
  "expirydate": DateTime
}
````
####POST Comment
Route: POST /api/newsfeed/:userid/article/:newsid/comment/:token <br>
Body: <br>
````json
{
  "content" : String
}
````
##MyCourses
####GET All Courses Belonging to Logged In User
Route: GET /api/mycourses/:userid/:token <br>
Body: There is no body content to send

##Contacts
####Get All Contacts
Route: GET /api/contacts/:userid/:token <br>
Body: There is no body content to send
####Get Specific Contact
Route: GET /api/contacts/:userid/single/:contactid/:token <br>
Body: There is no body content to send
####Get Postable Contacts
Route: GET /api/contacts/:userid/postable/:token <br>
Body: There is no body content to send
####Post Search for Contacts
Route: POST /api/contacts/:userid/search/:token
Body: <br>
````json
{
  "searchContent": String
}
````
