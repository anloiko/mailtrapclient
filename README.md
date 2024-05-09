# mailtrapclient
Solution:
- MailtrapClient - project on netstandard2.1 that incapsulates logic of working with Mailtrap, can be packaged to nuget
- MailtrapClient.Tests - tests project that covered a few workflows with MailtrapClient
- WebMailtrapApi - web api project with swagger ui that can be started and where we can send emails with different subjects and texts 

For run tests: 
- pls provide your recipient email in const RecipientEmail and your api token in const ApiToken in the class MailtrapSendTests

For check API:
- pls provide your recipient email in const RecipientEmail and your api token in const ApiToken in the class EmailController

Also
- you can change account for Mailtrap in appsettings.Development.json for Api project and in consts in MailtrapSendTests class for Test project
