# API

DO NOT USE VS's INTEGRATED GIT. BE A CONSOLE WIZARD.

Ben's Dev API base URL:
api.thunderchicken.ca/

-note if this route is not working try using api.thunderchicken.ca:8080

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
####GET Single News Article
Route: GET /api/newsfeed/:userid/article/:newsid/:token <br>
Body: There is no body content to send
####POST News Article
Route: POST /api/newsfeed/:userid/article/:token <br>
Body: <br>
````json
{
  "programid": Number,
  "coursesectionid": Number,
  "title": String,
  "content": String,
  "priority": String,
  "expirydate": DateTime
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
