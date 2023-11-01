using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SwishProxy.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SwishController : ControllerBase
    {
        private string _log = "\n\nInstalled certs:\n----------------\n";

        [HttpGet]
        public async Task<string> Get()
        {
            return await PostToSwishAsync();
        }

        private async Task<string> PostToSwishAsync()
        {
            try
            {
                using (HttpClient client = GetClient())
                {
                    client.BaseAddress = new Uri("https://cpc.getswish.net/swish-cpcapi/api/");

                    client.DefaultRequestHeaders.Clear();

                    var content = new StringContent(GetPayload(), Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync("v1/paymentrequests", content);

                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsStringAsync();
                    }

                    return $"Error: {response.StatusCode}\n{_log}";
                }
            }
            catch (Exception e)
            {
                var convertedException = JsonConvert.SerializeObject(e);

                return $"{convertedException}\n{_log}";
            }
        }

        private string GetPayload()
        {
            return @"{
                ""payeePaymentReference"": ""0123456789"",
                ""callbackUrl"": ""https://example.com/api/swishcb/paymentrequests"",
                ""payerAlias"": ""4671234768"",
                ""payeeAlias"": ""1231181189"",
                ""amount"": ""100"",
                ""currency"": ""SEK"",
                ""message"": ""Kingston USB Flash Drive 8 GB""
            }";
        }

        private HttpClient GetClient()
        {
            var handler = new HttpClientHandler();

            //handler.SslProtocols = System.Security.Authentication.SslProtocols.Tls12;
            //handler.ServerCertificateCustomValidationCallback = (_, _, _, _) => true;

            var clientCerts = GetCertificates(new[]{
                "A8985D3A65E5E5C4B2D7D66D40C6DD2FB19C5436", // DigiCert Global Root CA cert
                "5b30c3baeaef70b6a52a4826e60001a79d629e05", // 1232349751 (A client cert)
                "443ff123f1b24cb87037a804b554e0e90d89d8f4", // SEB Customer CA1 v2 for Swish cert
                "72b0d6c45893f53578767cd4d5213a0f01274013", // SEB Root CA v2 for Swish cert
                "49F2334991C4A48DFC6862095E965229B7CEE457", // Swish Root CA v2 cert
            });

            foreach (var cert in clientCerts)
            {
                _log += cert.Subject + "\n";
                handler.ClientCertificates.Add(cert);
            }

            return new HttpClient(handler);
        }

        private X509Certificate2Collection GetCertificates(string[] thumbprints)
        {
            var certStore = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            certStore.Open(OpenFlags.ReadOnly);

            var certificates = new X509Certificate2Collection();

            foreach (var thumbprint in thumbprints)
            {
                var certs = certStore.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, false);
                certificates.AddRange(certs);
            }

            return certificates;

        }
    }
}