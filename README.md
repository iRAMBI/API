# API

DO NOT USE VS's INTEGRATED GIT. BE A CONSOLE WIZARD.

Ben's Dev API base URL:
api.thunderchicken.ca/

-note if this route is not working try using api.thunderchicken.ca:8080

#Routes

##Authentication
####POST /api/auth
Body:
````json
{ 
  "email":String,
  "password":String
}
````
