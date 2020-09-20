using System;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace Azureference.Web.Pages
{
    public class KeyVault : PageModel
    {
        private readonly IConfiguration _config;

        public KeyVault(IConfiguration config)
        {
            _config = config;
            SecretName = _config.GetValue<string>("KeyVault:DemoSecretName");
        }

        public string SecretName { get; }
        public string SecretValue { get; private set; }
        public string ErrorMessage { get; private set; }

        public async Task OnGetAsync()
        {
            try
            {
                var keyVaultUri = _config.GetValue<string>("KeyVault:Uri");
                var client = new SecretClient(new Uri(keyVaultUri), GetClientCredential());
                var secret = await client.GetSecretAsync(SecretName);

                SecretValue = secret.Value.Value;
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
            }
        }


        private TokenCredential GetClientCredential()
        {
            var tenantId = _config.GetValue<string>("AzureAD:TenantId");
            var clientId = _config.GetValue<string>("AzureAD:ClientId");
            var clientSecret = _config.GetValue<string>("AzureAD:ClientSecret");

            return new ClientSecretCredential(tenantId, clientId, clientSecret);
        }
    }
}
