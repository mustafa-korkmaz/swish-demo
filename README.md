# Swish demo app
This repo contains the published code on an Azure App Service served from https://swishproxy.azurewebsites.net/swish address. When I install the required certificates on my local machine it works as expected.
But when I install them on Azure it gets `HandshakeFailure` error even though I can see all the required certificates are installed properly.  
`Installed certs:` trace shows the installed certificates that are successfully attached to the Http request.  
Everything you need can be found in [SwishController.cs](/SwishProxy/Controllers/SwishController.cs) file

### Expected result
Error: Forbidden (HTTP 403)  

Installed certs:  
 
CN=DigiCert Global Root CA, OU=www.digicert.com, O=DigiCert Inc, C=US  
C=SE, O=5590934534, CN=1232349751  
CN=SEB Customer CA1 v2 for Swish, SERIALNUMBER=ESSESESS, O=Skandinaviska Enskilda Banken AB (publ), C=SE  
CN=SEB Root CA v2 for Swish, SERIALNUMBER=ESSESESS, O=Skandinaviska Enskilda Banken AB (publ), C=SE  
CN=Swish Root CA v2, OU=Swish Member CA, O=Getswish AB  

### Actual result
{"StatusCode":null,"Message":"The SSL connection could not be established, see inner exception.","Data":{},"InnerException":{"ClassName":"System.Security.Authentication.AuthenticationException","Message":"Authentication failed because the remote party sent a TLS alert: 'HandshakeFailure'.","Data":null,"InnerException":{"ClassName":"System.ComponentModel.Win32Exception","Message":"The message received was unexpected or badly formatted.","Data":null,"InnerException":null,"HelpURL":null,"StackTraceString":null,"RemoteStackTraceString":null,"RemoteStackIndex":0,"ExceptionMethod":null,"HResult":-2147467259,"Source":null,"WatsonBuckets":null,"NativeErrorCode":-2146893018},"HelpURL":null,"StackTraceString":"   at System.Net.Security.SslStream.ForceAuthenticationAsync[TIOAdapter](Boolean receiveFirst, Byte[] reAuthenticationData, CancellationToken cancellationToken)\r\n   at System.Net.Http.ConnectHelper.EstablishSslConnectionAsync(SslClientAuthenticationOptions sslOptions, HttpRequestMessage request, Boolean async, Stream stream, CancellationToken cancellationToken)","RemoteStackTraceString":null,"RemoteStackIndex":0,"ExceptionMethod":null,"HResult":-2146233087,"Source":"System.Net.Security","WatsonBuckets":null},"HelpLink":null,"Source":"System.Net.Http","HResult":-2146233087,"StackTrace":"   at System.Net.Http.ConnectHelper.EstablishSslConnectionAsync(SslClientAuthenticationOptions sslOptions, HttpRequestMessage request, Boolean async, Stream stream, CancellationToken cancellationToken)\r\n   at System.Net.Http.HttpConnectionPool.ConnectAsync(HttpRequestMessage request, Boolean async, CancellationToken cancellationToken)\r\n   at System.Net.Http.HttpConnectionPool.CreateHttp11ConnectionAsync(HttpRequestMessage request, Boolean async, CancellationToken cancellationToken)\r\n   at System.Net.Http.HttpConnectionPool.AddHttp11ConnectionAsync(QueueItem queueItem)\r\n   at System.Threading.Tasks.TaskCompletionSourceWithCancellation`1.WaitWithCancellationAsync(CancellationToken cancellationToken)\r\n   at System.Net.Http.HttpConnectionPool.HttpConnectionWaiter`1.WaitForConnectionAsync(Boolean async, CancellationToken requestCancellationToken)\r\n   at System.Net.Http.HttpConnectionPool.SendWithVersionDetectionAndRetryAsync(HttpRequestMessage request, Boolean async, Boolean doRequestAuth, CancellationToken cancellationToken)\r\n   at System.Net.Http.DiagnosticsHandler.SendAsyncCore(HttpRequestMessage request, Boolean async, CancellationToken cancellationToken)\r\n   at System.Net.Http.RedirectHandler.SendAsync(HttpRequestMessage request, Boolean async, CancellationToken cancellationToken)\r\n   at System.Net.Http.HttpClient.<SendAsync>g__Core|83_0(HttpRequestMessage request, HttpCompletionOption completionOption, CancellationTokenSource cts, Boolean disposeCts, CancellationTokenSource pendingRequestsCts, CancellationToken originalCancellationToken)\r\n   at SwishProxy.Controllers.SwishController.PostToSwishAsync() in C:\\Users\\mustafa.korkmaz\\Desktop\\SwishProxy\\SwishProxy\\Controllers\\SwishController.cs:line 35"}

Installed certs:

CN=DigiCert Global Root CA, OU=www.digicert.com, O=DigiCert Inc, C=US  
C=SE, O=5590934534, CN=1232349751  
CN=SEB Customer CA1 v2 for Swish, SERIALNUMBER=ESSESESS, O=Skandinaviska Enskilda Banken AB (publ), C=SE  
CN=SEB Root CA v2 for Swish, SERIALNUMBER=ESSESESS, O=Skandinaviska Enskilda Banken AB (publ), C=SE  
CN=Swish Root CA v2, OU=Swish Member CA, O=Getswish AB  
