# .NET Core & Heroku Coding Case

The solution contains 2 projects of ASP.NET Core apps:
1. RegistrationService accepts requests for license registrations,
2. SignatureGenerator provides signing capabilities.

## Request
To check the functionality, a POST request should be sent to the following URL:  
https://sv-registration-service.herokuapp.com/api/Registration

containing the following body:
```json
{  
  "companyName": "Company",  
  "contactPerson": "mcDuck",  
  "email": "ff@gg.com",  
  "address": "my address",  
  "licenseKey": "Cmfm-11"   
}  
```

## Response

The result should have one of the following formats:

- Success.
```json
{
  "licenseKey": "Cmfm-11",
  "signature": "40fcf3662ab4f8e95f2aeb36255b0f1afe089f8e89dcd1c916bd1f59d21ebb2e"
}  
```

- Failed validation. Status 400.
- Wrong license key. Status 403.
- Signature generator error. Status 503.

## Logging
A standard logging via ILogger abstraction is used for RegistrationService. The default processing is logged with a Trace verbosity.

## RPC
As Heroku doesn't support gRPC, to avoid custom WebSocket-based implementation, SignalR solution was used with only WebSocket transport allowed.  

## Limitations

There are 3 groups of limitations related to PoC nature of the solution:

### Absence of DB

As there is no DB for RegistrationService it's hard to implement a realistic model of license key checks. So the application uses the following approach: the valid license key 
- starts with 1st letters of company name, contact person, email, address, and license key,
- ends with 1.

Also, there is no tracking of license registrations except the tracing log messages. But it would be more logical to keep this info in DB also. 

### Signing
As there were no requests for the signature generator, the 'signature' process is very naive. In real world it should be based on asymmetric encryption mechanism. 

### Signature Generator Protection. 
One of the requirements was to keep Signature Generator safe from external connections. This requirement is kinda infrastructural, and it looks like Heroku doesn't provide it for free (https://devcenter.heroku.com/articles/internal-routing). 

Custom check based on the sender address might be not a brilliant idea.