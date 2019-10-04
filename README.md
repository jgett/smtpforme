# SmtpForMe

Creates a local smtp server that can receive emails. Received emails are displayed in a web-based GUI. By default this is hosted at https://jgett.github.io/smtpforme/inbox/.

This is mainly (only?) useful for debugging an application that sends emails, so that actual emails are not sent.

The application runs in the system tray. Clicking the icon will open a small window where you can open the web interface or quit.

#### Running SmtpForMe with default App.config settings
1. Download the latest release and run SmtpForMe.exe on your local computer. This will create an SMTP server listening on port 4855 and a web service listing on port 4856 (i.e. http://127.0.0.1:4856).
1. Configure your application to send emails to 127.0.0.1 port 4855.
1. View received emails at https://jgett.github.io/smtpforme/inbox.

#### Changing settings in App.config
The following settings are available:
* SmtpServiceHost: The host for the smtp server (default: 127.0.0.1)
* SmtpServicePort: The port for the smtp server (default: 4855)
* WebServiceHost: The uri for the web service. The gui will use this uri to interact with the application (default: http://127.0.0.1:4856)
* UserInterfaceUri: The uri for the user interface where emails are displayed and can be deleted (default: https://jgett.github.io/smtpforme/inbox)

If WebServiceHost is changed then the querystring ?host=<UrlEncodedValueOfWebServiceHost> will be appended to UserInterfaceUri.

UserInterfaceUri can be changed if you want to host this page on your own web server.
