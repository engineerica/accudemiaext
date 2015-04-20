Table of Contents:


**DOCUMENTATION STILL UNDER DEVELOPMENT**


# Introduction #

It explains the options of the public HTTP API exposed by Accudemia. This service is still under development and is subject to change at any moment.

The service exposed can be called using simple HTTP requests, which enables all kind of applications to access this service.

# Service Specification #

## Protocol and Location ##

You can reach the service issuing a HTTP Secure (HTTPS) request to the Accudemia server.
HTTPS is a combination of the Hypertext Transfer Protocol with the SSL/TLS protocol to provide encrypted communication and secure identification of a network web server.

In order to access the service, you have to make a request to the following address:

```
https://your-college.accudemia.net/Services/PublicHttp.ashx
```

where `your-college.accudemia.net` must be replace by the address of your college.

## Your Developer Key ##

In order to learn how to get your developer key, please read the related article: YourDeveloperKey.

Once you have your key, you have to append it to the service URL, for example:

```
.../PublicHttp.ashx?devkey=f2d99640-0712-4ba5-a82f-d84e368295ac
```


## Actions ##

The command you want to execute must be specified using a parameter called _action_ in the URL.

### `getlogintoken` ###

> This command returns a text (token) which can be used to log-in any user to Accudemia. You just have to specify the user ID using the _user_ parameter.
For example, in order to get a log-in access token for the user 123-12-1234, issue a HTTP GET request to the address:


```
.../PublicHttp.ashx?devkey=f2d99640-0712-4ba5-a82f-d84e368295ac&action=getlogintoken&user=123-12-1234
```