Before running the application, please follow the below steps:

1. Please run InitDb.sql script to create the database and table

2. In appsettings.Development.json file, please change the following values to your own values
"AzureAd": {
    "Instance": "https://login.microsoftonline.com/",
    "Domain": "[Enter your Domain Name]",
    "TenantId": "[Enter your Tenant ID]",
    "ClientId": "[Enter your Client ID]",
    "CallbackPath": "/signin-oidc"
  }