Database is local in App_Data folder, you need to have Sql Server 2014 localdb.
Wihthout internet connection icons may not be displayed.

Token based authorisation was implemented using Asp.NET Identity.

In order to use api you need to get token (to login) and then put that token into Authorization header in each request.

Example how to login:

POST
http://localhost:54509/Token
grant_type=password&username=a@a.a&password=123456 (type should be text not application/json)

Examples how to register:

POST
http://localhost:54509/api/account/register
{Email: "b@b.b",
Password: "123456",
ConfirmPassword:"123456"
}

Examples how to use api:

GET
Headers: Authorization - "Bearer [token]" (where [token] is your token)
http://localhost:54509/api/links

GET
Headers: Authorization - "Bearer [token]" (where [token] is your token)
http://localhost:54509/api/links/8

POST
Headers: Authorization - "Bearer [token]" (where [token] is your token)
http://localhost:54509/api/links
{
Url: "http://google.com",
Name:"Google",
Category:"Work"
} (type shoud be application/json)

PUT
Headers: Authorization - "Bearer [token]" (where [token] is your token)
http://localhost:54509/api/links/8
{
Url: "http://google.com",
Name:"Google",
Category:"Word"
}

DELETE
Headers: Authorization - "Bearer [token]" (where [token] is your token)
http://localhost:54509/api/links/8

GET
Headers: Authorization - "Bearer [token]" (where [token] is your token)
http://localhost:54509/api/links/filter/Work
